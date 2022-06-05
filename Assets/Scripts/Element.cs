using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
	public Tile parentTile;
	public ElementType elementType;
	public int typeIndex;
	public bool isStackable;
	public enum ElementType
	{
		None,
		Field,
		Studio,
		CastleWall,
		CastleTower,
		Hut,
	};
}
