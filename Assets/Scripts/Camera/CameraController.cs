using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Camera mainCam;

	private void Start()
	{
		mainCam = Camera.main;
	}

	private void Update()
	{
		MoveCamera();

        CameraZoomControl();
	}

    #region Camera Movement

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

        Vector3[] vertexWorldPos = new Vector3[Tilemap.tilemapVertex.Length];

        for(int i = 0; i < vertexWorldPos.Length; i++)
        {
            vertexWorldPos[i] = Tilemap.buildingTilemap.CellToWorld(Tilemap.tilemapVertex[i]);
        }

        Vector3 camPos = transform.position;

        camPos.x = Mathf.Clamp(camPos.x,
                               vertexWorldPos[(int)ETilemapVertexDir.LEFT].x,
                               vertexWorldPos[(int)ETilemapVertexDir.RIGHT].x);

        camPos.y = Mathf.Clamp(camPos.y,
                               vertexWorldPos[(int)ETilemapVertexDir.DOWN].y,
                               vertexWorldPos[(int)ETilemapVertexDir.UP].y);

        transform.position = camPos;
    }

    #endregion

    #region Camera Zoom

    [SerializeField] private float zoomValue;

    [SerializeField] private float minZoomValue, maxZoomValue;

    [SerializeField] private float zoomSpeed;

    public void CameraZoomControl()
    {
        zoomValue = Mathf.Lerp(zoomValue, zoomValue - Input.mouseScrollDelta.y, Time.deltaTime * zoomSpeed);

        zoomValue = Mathf.Clamp(zoomValue, minZoomValue, maxZoomValue);

        mainCam.orthographicSize = zoomValue;
    }

    #endregion
}