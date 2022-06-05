using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{


	public Camera mainCam;
	float lookSensitivity = 250;
	float movementSpeed = .5f;

	void Awake()
	{
		mainCam = Camera.main;
		Time.timeScale = 1f;
	}

    private void FixedUpdate()
    {
		if (Input.GetKey(KeyCode.LeftShift))
		{
			movementSpeed = 1;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.parent.position += Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * movementSpeed;
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.parent.position += transform.right * -movementSpeed;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.parent.position += Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * -movementSpeed;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.parent.position += transform.right * movementSpeed;
		}
		if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Space))
		{
			transform.parent.position += Vector3.up * movementSpeed;
		}
		if (Input.GetKey(KeyCode.Q))
		{
			transform.parent.position += Vector3.up * -movementSpeed;
		}

		//rotation
		if (Input.GetMouseButton(1))
		{
			transform.parent.localEulerAngles = (Vector3.up * (transform.parent.localEulerAngles.y + Input.GetAxis("Mouse X") * lookSensitivity * Time.fixedDeltaTime));
			transform.localEulerAngles = (Vector3.right * (transform.localEulerAngles.x + Input.GetAxis("Mouse Y") * -lookSensitivity * Time.fixedDeltaTime));
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
