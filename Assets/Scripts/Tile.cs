using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Tilemap tileMap;
	public List<List<Element>> childedElements = new List<List<Element>>();
	public Vector3 tilePosition;
	public TileType tileType;
	public bool isBuildable;
	public enum TileType
	{
		None,
		Anchor,
		Ghost,
		Basic
	}

	public void GenerateElementsOnTile(List<List<Element.ElementType>> e, bool addToMap)
	{
		int totalPointsToAdd = 10;
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
						if(addToMap)
							tileMap.fc.am.Play(model.audioIndex);
						int typeIdex = Random.Range(0, model.StandaloneModel.Length);
						Element elm = Instantiate(model.StandaloneModel[typeIdex], transform.GetChild(0)).GetComponent<Element>();
						elm.transform.localPosition = new Vector3(x * tileMap.elementDimensions.x, 0, -z * tileMap.elementDimensions.z);
						elm.transform.name = elm.transform.position + "";
						elm.parentTile = this;
						elm.typeIndex = typeIdex;
						childedElements[x].Add(elm);
						if (addToMap)
							tileMap.elementMap.Add(elm.transform.position, elm);
						totalPointsToAdd += ((int)tilePosition.y+1) * 10;

						if (tilePosition.y > 0 && tileMap.elementMap.ContainsKey(new Vector3(elm.transform.position.x, tilePosition.y - tileMap.tileDimensions.y, elm.transform.position.z)))
						{
							if (addToMap)
								tileMap.fc.am.Play(2);
							Element lower = tileMap.elementMap[new Vector3(elm.transform.position.x, tilePosition.y - tileMap.tileDimensions.y, elm.transform.position.z)];
							Vector3 lowPos = lower.transform.position;
							Tile lowTile = lower.parentTile;
							tileMap.elementMap.Remove(lowPos);
							int ti = lower.typeIndex;
							Destroy(lower.gameObject);
							Element newLow = Instantiate(model.HasTopModel[ti], lowTile.transform.GetChild(0)).GetComponent<Element>();
							newLow.transform.localPosition = new Vector3(x * tileMap.elementDimensions.x, 0, -z * tileMap.elementDimensions.z);
							newLow.transform.name = newLow.transform.position + "";
							newLow.parentTile = lowTile;
							lowTile.childedElements[x][z] = newLow;
							tileMap.elementMap[newLow.transform.position] = newLow;
						}
					}
				}
				else
				{
					childedElements[x].Add(null);
				}
			}
		}
		tileMap.fc.playerScore += totalPointsToAdd;
		tileMap.fc.scoreText.text = tileMap.fc.playerScore + "";
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
