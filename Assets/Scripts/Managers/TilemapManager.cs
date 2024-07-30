using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum ETilemapVertexDir : byte
{
    UP = 0,
    DOWN,
    LEFT,
    RIGHT,
    COUNT
}

public class TilemapManager : MonoBehaviour
{
    [Header("Tilemap Vertex")]
    public Vector3Int[] tilemapVertex = new Vector3Int[(int)ETilemapVertexDir.COUNT];

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
        Vector3Int tilePos, min, max;
        TileBase tile;

        min = max = Vector3Int.zero;

        for (i = bounds.xMin; i < bounds.xMax; i++)
        {
            if (min != Vector3Int.zero) break;

            for (j = bounds.yMin; j < bounds.yMax; j++)
            {
                tilePos = new Vector3Int(i, j);
                tile = buildingTilemap.GetTile(tilePos);

                if (tile != null && tile.name.Contains(FloorName))
                {
                    min = tilePos;

                    break;
                }
            }
        }

        for (i = bounds.xMax; i > bounds.xMin; i--)
        {
            if (max != Vector3Int.zero) break;

            for (j = bounds.yMax; j > bounds.yMin; j--)
            {
                tilePos = new Vector3Int(i, j);
                tile = buildingTilemap.GetTile(tilePos);

                if (tile != null && tile.name.Contains(FloorName))
                {
                    max = tilePos;

                    break;
                }
            }
        }

        tilemapVertex[(int)ETilemapVertexDir.UP] = max;
        tilemapVertex[(int)ETilemapVertexDir.DOWN] = min;
        tilemapVertex[(int)ETilemapVertexDir.LEFT] = new Vector3Int(min.x, max.y);
        tilemapVertex[(int)ETilemapVertexDir.RIGHT] = new Vector3Int(max.x, min.y);
    }
}
