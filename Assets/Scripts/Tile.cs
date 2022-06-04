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

	public void GenerateElementsOnTile(List<List<Element.ElementType>> e)
	{
		for (int x = 0; x < tileMap.tileDimensions.x/tileMap.elementDimensions.x; x++)
		{
			childedElements.Add(new List<Element>());
			for (int z = 0; z < tileMap.tileDimensions.z / tileMap.elementDimensions.z; z++)
			{
				if(e[x][z] != Element.ElementType.None)
				{
					Element elm = Instantiate(tileMap.elementPrefabs[0], transform.GetChild(0)).GetComponent<Element>();
					elm.transform.localPosition = new Vector3(x * tileMap.elementDimensions.x, 0, -z * tileMap.elementDimensions.z);
					childedElements[x].Add(elm);
				}
				else
				{
					childedElements[x].Add(null);
				}
			}
		}
	}

	public void ClearElementsOnTile()
	{
		for (int i = 0; i < transform.GetChild(0).childCount; i++)
		{
			Destroy(transform.GetChild(0).GetChild(i).gameObject);
		}
	}
}
