using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
	[Header("Interior Tilemap")][SerializeField]
	private Tilemap interiorTilemap;

	[Header("선택된 가구")][SerializeField]
	private TileBase clickedFurniture;

	private Camera mainCam;

	private void Start()
	{
		mainCam = Camera.main;
	}

	private void Update()
	{
		CheckMousePosTile();
	}

	private void CheckMousePosTile()
	{
		if (!Input.GetMouseButtonDown(0)) return;

		Vector3 mousePos = Input.mousePosition;
		Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);

		Vector3Int tilePos = interiorTilemap.WorldToCell(worldPos);
		tilePos.z = 0;

        clickedFurniture = interiorTilemap.GetTile(tilePos);
    }
}