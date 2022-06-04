using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour
{
	public Dictionary<Vector3, Tile> coordinateMap = new Dictionary<Vector3, Tile>();
	public GameObject tilePrefab, ghostTilePrefab;

	public Vector3 tileDimensions;

	public List<GameObject> elementPrefabs;

	public Tile GetTileInMap(Vector3 pos)
	{
		if (pos == null)
			return null;
		if (coordinateMap.ContainsKey(pos))
		{
			return coordinateMap[pos];
		}
		else
		{
			return null;
		}
	}

	/// <summary>
	/// Make sure that the position is in worldspace coords
	/// </summary>
	/// <param name="_type"></param>
	/// <param name="_pos"></param>
	/// <returns></returns>
	public Tile CreateTile(Tile.TileType _type, Vector3 _pos, bool createGhosts, List<List<Element.ElementType>> elements)
	{
		if (GetTileInMap(_pos) != null)
			return null;
		Tile tile = Instantiate(_type==Tile.TileType.Ghost?ghostTilePrefab:tilePrefab, _pos, Quaternion.identity).GetComponent<Tile>();
		tile.tileMap = this;
		tile.tileType = _type;
		tile.tilePosition = _pos;
		coordinateMap.Add(_pos, tile);

		if (createGhosts)
		{
			foreach (Vector3 offset in offsets)
			{
				CreateTile(Tile.TileType.Ghost, (tile.tilePosition + offset*tileDimensions.x), false, null);
			}
		}

		return tile;
	}
	public void DelteTile(Tile tile)
	{
		coordinateMap.Remove(tile.tilePosition);
		Destroy(tile.gameObject);
	}
	public static Vector3[] offsets =
	{
		Vector3.forward,
		Vector3.right,
		Vector3.back,
		Vector3.left,
	};
	public List<Tile> GetLateralNeighboringTiles(Tile tile)
	{
		List<Tile> tiles = new List<Tile>();
		foreach (Vector3 offset in offsets)
		{
			Tile t = GetTileInMap(tile.tilePosition + offset*tileDimensions.x);
			if (t)
			{
				tiles.Add(t);
			}
		}
		return tiles;
	}
	public Tile GetUpperNeighbor(Tile tile)
	{
		return GetTileInMap(tile.tilePosition + (Vector3.up * tileDimensions.y));
	}
}
