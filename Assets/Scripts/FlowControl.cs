using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowControl : MonoBehaviour
{
	public Tilemap tileMap;
	public CameraControl cameraControl;
	private void Start()
	{
		tileMap.CreateTile(Tile.TileType.Anchor, Vector3.zero, true);
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//check that the player is able to place tiles
			Tile t = cameraControl.RaycastTile();
			if (t)
			{
				if(t.tileType == Tile.TileType.Ghost)
				{

					//check that it can be placed there here
					tileMap.DelteTile(t);

				}
			}
		}
	}
}
