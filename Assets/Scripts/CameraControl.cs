using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
	{


	public Camera mainCam;

	public void Awake()
	{
		mainCam = Camera.main;
	}

	public GameObject Raycast()
	{
		Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 10000))
		{
			return hit.transform.gameObject;
		}
		return null;
	}
	public Tile RaycastTile()
	{
		GameObject hit = Raycast();
		return hit.GetComponentInParent<Tile>();
	}
	public FixedUpdate() {
	motion = new Vector3(inp)
	}
}
