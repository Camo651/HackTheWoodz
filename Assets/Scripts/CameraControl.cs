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
		float shift = Input.GetKey(KeyCode.LeftShift) ? 1 : 0;
		float movementSpeed = 1;
		if(Input.GetKeyDown(KeyCode.LeftShift))
        {
			movementSpeed = 2;
        }
		if (Input.GetKeyDown(KeyCode.W))
        {
			transform.position += Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * Time.fixedDeltaTime * movementSpeed;
        }
		if (Input.GetKeyDown(KeyCode.A))
		{
			transform.position += transform.right * Time.fixedDeltaTime * -movementSpeed;
		}
		if (Input.GetKeyDown(KeyCode.S))
		{
			transform.position += Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * Time.fixedDeltaTime * -movementSpeed;
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
		if (Input.GetMouseButtonDown(1)) {
			float rotX = transform.localEulerAngles.y + Input.GetAxisRaw("Mouse X") * lookSensitivity * Time.FixedDeltaTime;
			float rotY = transform.localEulerAngles.x - Input.GetAxisRaw("Mouse Y") * lookSensitivity * Time.FixedDeltaTime;
			transform.localEulerAngles += new Vector3(rotY, rotX, 0f);
			transform.localEulerAngles.x = Mathf.Clamp(transform.localEulerAngles.x, -85, 85);
		}
    }
}
