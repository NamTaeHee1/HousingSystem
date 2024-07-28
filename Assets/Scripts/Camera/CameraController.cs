using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Camera mainCam;

	private void Awake()
	{
		mainCam = Camera.main;
	}

	private void Update()
	{
		MoveCamera();
	}

	private Vector3 moveDir, startPos;

	private void MoveCamera()
	{
		Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

		if(Input.GetMouseButtonDown(0))
		{
			startPos = mousePos;
		}
		else if(Input.GetMouseButton(0))
		{
			moveDir = startPos - mousePos;
		}
		else
		{
			moveDir = Vector3.zero;
		}

		if (moveDir == Vector3.zero) return;

		Vector3 from = transform.position;
		Vector3 to = transform.position + moveDir;

		transform.position = Vector3.Lerp(from, to, 0.5f);

        ClampCameraPos();
	}

    private void ClampCameraPos()
    {
        TilemapManager Tilemap = Managers.Tilemap;

        TilemapSize size = Tilemap.tilemapSize;

        Vector3 min = size.ConvertToWorldPos(Tilemap.buildingTilemap, size.minPos);
        Vector3 max = size.ConvertToWorldPos(Tilemap.buildingTilemap, size.maxPos);

        Debug.Log($"min : {min}, max : {max}");
    }
}