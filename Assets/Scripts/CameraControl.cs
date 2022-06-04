using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
	{


	public Camera mainCam;
	float lookSensitivity = 2;

	void Awake()
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
	void FixedUpdate() {
		float shift = Input.GetKey(KeyCode.LeftShift);
		float movementSpeed = 1;
		if(Input.GetKeyDown(KeyCode.LeftShift))
        {
			movementSpeed = 2;
        }
		if (Input.GetKeyDown(KeyCode.W))
        {
			transform.position += transform.forward * Time.fixedDeltaTime * movementSpeed;
        }
		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.position += transform.right * Time.fixedDeltaTime * -movementSpeed;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.position += transform.forward * Time.fixedDeltaTime * -movementSpeed;
		}
		if (Input.GetKeyDown(KeyCode.D))
		{
			transform.position += transform.right * Time.fixedDeltaTime * movementSpeed;
		}
		if (Input.GetKeyDown(KeyCode.Q))
        {
			transform.position += Vector3.up * Time.fixedDeltaTime * movementSpeed;
		}
        if (Input.GetKeyDown(KeyCode.E))
        {
			transform.position += Vector3.up * Time.fixedDeltaTime * -movementSpeed;
		}

		//rotation
		if (Input.GetKeyDown(Input.GetMouseButtonDown(1)) {
			float rotX = transform.localEulerAngles.y + Input.GetAxisRaw("Mouse X") * lookSensitivity;
			float rotY = transform.localEulerAngles.x - Input.GetAxisRaw("Mouse Y") * lookSensitivity;
			transform.localEulerAngles += Vector3(rotY, rotX, 0f);
		}
    }
}
