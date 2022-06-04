using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public Tilemap tileMap;
	public List<Element> childedElements = new List<Element>();
	public Vector3 tilePosition;
	public TileType tileType;
	public enum TileType
	{
		None,
		Anchor,
		Ghost,
		Basic
	}
}
