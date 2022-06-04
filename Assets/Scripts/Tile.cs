using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Tilemap tileMap;
	public List<List<Element>> childedElements = new List<List<Element>>(); //5x5 matrix (elms 2x2 grid scale)
	public Vector3 tilePosition;
	public TileType tileType;
	public enum TileType
	{
		None,
		Anchor,
		Ghost,
		Basic
	}

	public void GenerateElements()
	{
		for (int x = 0; x < 5; x++)
		{
			childedElements.Add(new List<Element>());
			for (int z = 0; z < 5; z++)
			{
				childedElements[x].Add()
				if(Mathf.PerlinNoise((tilePosition.x*5) + x, (tilePosition.z*5) + z) > .4f)
				{

				}
			}
		}
	}
}
