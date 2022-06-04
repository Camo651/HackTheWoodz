using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Tilemap tileMap;
	public List<List<Element>> childedElements = new List<List<Element>>();
	public Vector3 tilePosition;
	public TileType tileType;
	public enum TileType
	{
		None,
		Anchor,
		Ghost,
		Basic
	}

	public void GenerateElementsOnTile(List<List<Element.ElementType>> e, bool addToMap)
	{
		for (int x = 0; x < tileMap.tileDimensions.x/tileMap.elementDimensions.x; x++)
		{
			childedElements.Add(new List<Element>());
			for (int z = 0; z < tileMap.tileDimensions.z / tileMap.elementDimensions.z; z++)
			{
				if(e[x][z] != Element.ElementType.None)
				{
					ElementPrefab model = GetCorrectModelForElement(e[x][z]);
					if(model != null)
					{
						Element elm = Instantiate(model.StandaloneModel, transform.GetChild(0)).GetComponent<Element>();
						elm.transform.localPosition = new Vector3(x * tileMap.elementDimensions.x, 0, -z * tileMap.elementDimensions.z);
						elm.transform.name = elm.transform.position + "";
						elm.parentTile = this;
						childedElements[x].Add(elm);
						if (addToMap)
							tileMap.elementMap.Add(elm.transform.position, elm);

						if(tilePosition.y > 0)
						{
							Element lower = tileMap.elementMap[new Vector3(x, tilePosition.y - tileMap.tileDimensions.y, z)];
							Vector3 lowPos = lower.transform.position;
							Tile lowTile = lower.parentTile;
							tileMap.elementMap.Remove(lowPos);
							Destroy(lower.gameObject);
							Element newLow = Instantiate(model.HasTopModel, lowTile.transform.GetChild(0)).GetComponent<Element>();
							elm.transform.localPosition = new Vector3(x * tileMap.elementDimensions.x, 0, -z * tileMap.elementDimensions.z);
							elm.transform.name = elm.transform.position + "";
							elm.parentTile = lowTile;
							lowTile.childedElements[x][z] = newLow;
							tileMap.elementMap[elm.transform.position] = newLow;

						}
					}
				}
				else
				{
					childedElements[x].Add(null);
				}
			}
		}
	}

	public ElementPrefab GetCorrectModelForElement(Element.ElementType e)
	{
		foreach (ElementPrefab elementPrefab in tileMap.elementPrefabs)
		{
			if(elementPrefab.elementType == e)
			{
				return elementPrefab;
			}
		}
		return null;
	}

	public void ClearElementsOnTile()
	{
		for (int i = 0; i < transform.GetChild(0).childCount; i++)
		{
			Destroy(transform.GetChild(0).GetChild(i).gameObject);
		}
	}
}
