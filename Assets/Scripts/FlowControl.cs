using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowControl : MonoBehaviour
{
	public Tilemap tileMap;
	public CameraControl cameraControl;
	public Tile previewTile;
	public List<List<Element.ElementType>> previewElements;
	private void Start()
	{
		tileMap.CreateTile(Tile.TileType.Ghost, Vector3.zero, false, null);
		SetPreviewTile(GenerateElements(Random.Range(0,100000)));
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			//check that the player is able to place tiles
			Tile t = cameraControl.RaycastTile();
			if (t)
			{
				if(t.tileType == Tile.TileType.Ghost && TileCanBePlaced(t))
				{

					//check that it can be placed there here
					Vector3 pos = t.tilePosition;
					tileMap.DelteTile(t);
					tileMap.CreateTile(Tile.TileType.Basic, pos, true, previewElements);
					SetPreviewTile(GenerateElements(Random.Range(0, 100000)));
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.R))
		{
			RotatePreviewTile();
		}
	}

	public bool TileCanBePlaced(Tile ghost)
	{
		if (ghost.tilePosition.y == 0)
			return true;

		bool allElementsMatch = true, containsElements = false;
		List<List<Element>> baseElements = tileMap.GetTileInMap(ghost.tilePosition - (Vector3.up * tileMap.tileDimensions.y)).childedElements;
		for (int x = 0; x < tileMap.tileDimensions.x / tileMap.elementDimensions.x; x++)
		{
			for (int z = 0; z < tileMap.tileDimensions.z / tileMap.elementDimensions.z; z++)
			{
				if (previewElements[x][z] != Element.ElementType.None)
				{
					containsElements = true;
					if(baseElements[x][z] == null || baseElements[x][z].elementType != Element.ElementType.Building)
					{
						allElementsMatch = false;
					}
				}
			}
		}
		return allElementsMatch && containsElements;
	}

	public void SetPreviewTile(List<List<Element.ElementType>> e)
	{
		previewTile.ClearElementsOnTile();
		previewTile.GenerateElementsOnTile(e, false);
		previewElements = e;
	}

	public List<List<Element.ElementType>> GenerateElements(int seed)
	{
		bool hasElements = false;
		List<List<Element.ElementType>> elms = new List<List<Element.ElementType>>();
		while (!hasElements)
		{
			elms = new List<List<Element.ElementType>>();
			for (int x = 0; x < tileMap.tileDimensions.x / tileMap.elementDimensions.x; x++)
			{
				elms.Add(new List<Element.ElementType>());
				for (int z = 0; z < tileMap.tileDimensions.z / tileMap.elementDimensions.z; z++)
				{
					elms[x].Add(Element.ElementType.None);
					if (Mathf.PerlinNoise((seed * 5.01f) + x, (seed * 5.01f) + z) * 10f > 6f)
					{
						hasElements = true;
						elms[x][z] = Random.value < .2f ? Element.ElementType.Field : Element.ElementType.Building;
					}
				}
			}
		}
		return elms;
	}

	public void RotatePreviewTile()
	{
		List<List<Element.ElementType>> remap = new List<List<Element.ElementType>>();
		remap.Add(new List<Element.ElementType>() { previewElements[2][0], previewElements[1][0], previewElements[0][0] });
		remap.Add(new List<Element.ElementType>() { previewElements[2][1], previewElements[1][1], previewElements[0][1] });
		remap.Add(new List<Element.ElementType>() { previewElements[2][2], previewElements[1][2], previewElements[0][2] });
		previewElements = remap;
		SetPreviewTile(previewElements);
	}
}
