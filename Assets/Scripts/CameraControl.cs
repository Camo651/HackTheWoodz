using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
	{


	public Camera mainCam;
	float lookSensitivity = 12;
	float movementSpeed = 1;

	void Awake()
	{
		mainCam = Camera.main;
	}

    private void FixedUpdate()
    {
		if (Input.GetKey(KeyCode.LeftShift))
		{
			movementSpeed = 2;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * movementSpeed;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.position += transform.right * -movementSpeed;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position += Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * -movementSpeed;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += transform.right * movementSpeed;
		}
		if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
		{
			transform.position += Vector3.up * movementSpeed;
		}
		if (Input.GetKey(KeyCode.Q))
		{
			transform.position += Vector3.up * -movementSpeed;
		}

		//rotation
		if (Input.GetMouseButton(1))
		{
			float rotX = Input.GetAxis("Mouse X") * lookSensitivity;
			float rotY = Input.GetAxis("Mouse Y") * -lookSensitivity;
			transform.localEulerAngles += new Vector3(rotY, rotX, 0f);
			//Debug.Log(transform.localEulerAngles.x);
			//transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x, -85f, 85f), transform.localEulerAngles.y, transform.localEulerAngles.z);
		}
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
		return hit!=null?hit.GetComponentInParent<Tile>():null;
	}
}
