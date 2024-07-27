using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct TilemapSize
{
	public Vector3Int minPos;
	public Vector3Int maxPos;
}

public class TileTest : MonoBehaviour
{
	[SerializeField]
	private Tilemap tilemap;

	private Camera mainCam;

	[SerializeField][Header("선택한 Tile")]
	private TileBase clickedTile;

	[Header("Table Tile")]
	[SerializeField]
	private TileBase tableTile;

	public TilemapSize tilemapSize;

	private void Start()
	{
		mainCam = Camera.main;
	}

	private void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Input.mousePosition;
			Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);

			Vector3Int tilePos = tilemap.WorldToCell(worldPos);
			tilePos.z = 0;

			TileBase selectedTile = tilemap.GetTile(tilePos);

			if(selectedTile != null)
			{
				Debug.Log("Clicked Tile: " + selectedTile.name + " at Position: " + tilePos);

				clickedTile = selectedTile;

				tilemap.SetColor(tilePos, Color.red);
			}
			else
			{
				Debug.Log($"No Tile at Position: {tilePos}, {worldPos}");
			}

			BoundsInt bounds = tilemap.cellBounds;

			Debug.Log($"Bounds.MinX : {bounds.xMin}, Bounds.MaxX : {bounds.xMax}");
			Debug.Log($"Bounds MinX MinY WorldToCell : {tilemap.WorldToCell(new Vector3(bounds.xMin, bounds.yMin))}");

			UpdateTilemapSize();
		}
	}

	private const string FloorName = "floor";

	private void UpdateTilemapSize()
	{
		BoundsInt bounds = tilemap.cellBounds;
		int i, j;
		Vector3Int tilePos;
		TileBase tile;

		for (i = bounds.xMin; i < bounds.xMax; i++)
		{
			if (tilemapSize.minPos != Vector3Int.zero) break;

			for(j = bounds.yMin; j < bounds.yMax; j++)
			{
				tilePos = new Vector3Int(i, j);
				tile = tilemap.GetTile(tilePos);

				if (tile != null && tile.name.Contains(FloorName))
				{
					tilemapSize.minPos = tilePos;

					break;
				}
			}
		}

		for(i = bounds.xMax; i > bounds.xMin; i--)
		{
			if (tilemapSize.maxPos != Vector3Int.zero) break;

			for(j = bounds.yMax; j > bounds.yMin; j--)
			{
				tilePos = new Vector3Int(i, j);
				tile = tilemap.GetTile(tilePos);

				if (tile != null && tile.name.Contains(FloorName))
				{
					tilemapSize.maxPos = tilePos;

					break;
				}
			}
		}
	}
}