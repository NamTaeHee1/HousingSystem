using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

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
            worldPos.z = 0;

			Vector3Int tilePos = tilemap.WorldToCell(worldPos);

			TileBase selectedTile = tilemap.GetTile(tilePos);

            if (selectedTile != null)
            {
                clickedTile = selectedTile;

                tilemap.SetColor(tilePos, Color.red);

                Debug.Log($"WorldPos : {tilemap.CellToWorld(tilePos)}");
            }

			BoundsInt bounds = tilemap.cellBounds;
		}
	}
}