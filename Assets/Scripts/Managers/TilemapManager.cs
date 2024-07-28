using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct TilemapSize
{
    public Vector3Int minPos;
    public Vector3Int maxPos;

    public Vector3 ConvertToWorldPos(Tilemap tilemap, Vector3Int pos)
    {
        return tilemap.CellToWorld(pos);
    }
}

public class TilemapManager : MonoBehaviour
{
    [Header("Tilemap Size")]
    public TilemapSize tilemapSize;

    [Header("Building Tilemap")]
    public Tilemap buildingTilemap;

    public void Init()
    {
        UpdateTilemapSize();
    }

    private const string FloorName = "floor";

    private void UpdateTilemapSize()
    {
        BoundsInt bounds = buildingTilemap.cellBounds;
        int i, j;
        Vector3Int tilePos;
        TileBase tile;

        for (i = bounds.xMin; i < bounds.xMax; i++)
        {
            if (tilemapSize.minPos != Vector3Int.zero) break;

            for (j = bounds.yMin; j < bounds.yMax; j++)
            {
                tilePos = new Vector3Int(i, j);
                tile = buildingTilemap.GetTile(tilePos);

                if (tile != null && tile.name.Contains(FloorName))
                {
                    tilemapSize.minPos = tilePos;

                    break;
                }
            }
        }

        for (i = bounds.xMax; i > bounds.xMin; i--)
        {
            if (tilemapSize.maxPos != Vector3Int.zero) break;

            for (j = bounds.yMax; j > bounds.yMin; j--)
            {
                tilePos = new Vector3Int(i, j);
                tile = buildingTilemap.GetTile(tilePos);

                if (tile != null && tile.name.Contains(FloorName))
                {
                    tilemapSize.maxPos = tilePos;

                    break;
                }
            }
        }
    }
}
