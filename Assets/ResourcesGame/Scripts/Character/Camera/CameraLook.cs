using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
	//public float acceleration = 50; // how fast you accelerate
	//public float accSprintMultiplier = 4; // how much faster you go when "sprinting"
	public float lookSensitivity = 1; // mouse look sensitivity
	//public float dampingCoefficient = 5; // how quickly you break to a halt after you stop your input
	public bool focusOnEnable = true; // whether or not to focus and lock cursor immediately on enable

	public Transform head;
	//Vector3 velocity; // current velocity

	static bool Focused
	{
		get => Cursor.lockState == CursorLockMode.Locked;
		set
		{
			Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
			Cursor.visible = value == false;
		}
	}

	void OnEnable()
	{
		if (focusOnEnable) Focused = true;
	}

	void OnDisable() => Focused = false;

	void Update()
	{
		// Input
		if (Focused)
			UpdateInput();
		else if (Input.GetMouseButtonDown(0))
			Focused = true;

		transform.position = Vector3.Lerp(transform.position, head.position,Time.deltaTime*10);
	}

	void UpdateInput()
	{
		// Position
	

		// Rotation
		Vector2 mouseDelta = lookSensitivity * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
		Quaternion rotation = transform.rotation;
		Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
		Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
		transform.rotation = horiz * rotation * vert;

		// Leave cursor lock
		if (Input.GetKeyDown(KeyCode.Escape))
			Focused = false;
	}

	
}
