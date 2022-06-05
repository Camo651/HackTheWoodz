using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ElementPrefab : ScriptableObject
{
	public Element.ElementType elementType;
	public int audioIndex;
	[Space(10)]
	public GameObject[] StandaloneModel;
	public GameObject[] HasTopModel;
}
