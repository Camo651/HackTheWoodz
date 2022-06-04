using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
	public Tile parentTile;
	public ElementType elementType;
	public enum ElementType
	{
		None,
		Building,
		Field,
		Path,
	};
}
