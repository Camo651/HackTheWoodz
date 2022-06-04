using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowControl : MonoBehaviour
{
	public Tilemap tileMap;
	public CameraControl cameraControl;
	private void Start()
	{
		tileMap.CreateTile(Tile.TileType.Ghost, Vector3.zero, true, null);
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
					Vector3 pos = t.tilePosition;
					tileMap.DelteTile(t);
					tileMap.CreateTile(Tile.TileType.Basic, pos, true, null);
				}
			}
		}
	}

	public List<List<Element.ElementType>> GenerateElements(int seed)
	{
		List<List<Element.ElementType>> elms = new List<List<Element.ElementType>>();
		for (int x = 0; x < 5; x++)
		{
			elms.Add(new List<Element.ElementType>());
			for (int z = 0; z < 5; z++)
			{
				elms[x].Add(Element.ElementType.None);
				if (Mathf.PerlinNoise((seed * 5.01f) + x, (seed * 5.01f) + z) * 10f > 7f)
				{
					elms[x][z] = Element.ElementType.Field;
				}
			}
		}
		return elms;
	}
}
