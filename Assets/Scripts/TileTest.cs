using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileTest : MonoBehaviour
{
	[SerializeField]
	private Tilemap tilemap;

	private Camera mainCam;

	[SerializeField][Header("º±≈√«— Tile")]
	private TileBase clickedTile;

	[Header("Table Tile")]
	[SerializeField]
	private TileBase tableTile;

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
				Debug.Log("No Tile at Position: " + tilePos);
			}

			BoundsInt bounds = tilemap.cellBounds;

			Debug.Log($"Bounds.MinX : {bounds.xMin}, Bounds.MaxX : {bounds.xMax}");
			Debug.Log($"Bounds MinX MinY WorldToCell : {tilemap.WorldToCell(new Vector3(bounds.xMin, bounds.yMin))}");
		}
	}
}