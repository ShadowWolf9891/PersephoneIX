using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(Rigidbody))]
public class MovePlayer : MonoBehaviour
{
	Rigidbody rb;
	Vector2 InputVector = Vector2.zero;
	Vector2 LookVector = Vector2.zero;

	[SerializeField]
	float MoveSpeed = 5;
	[SerializeField]
	float LookSensitivityX = 1f;
	[SerializeField]
	float LookSensitivityY = 1f;
	[SerializeField]
	float verticalClamp = 85f;

	Vector3 StartPosition = Vector3.zero;
	float StartRotation = 0f;

	public Transform cameraRoot;
	private float pitch;
	private Vector2 currentLook;
	private Vector2 smoothLookVelocity;
	public float smoothTime = 0.05f; // lower = snappier, higher = smoother

	bool InZeroG = false;


	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();

		StartPosition = transform.position;
		StartRotation = transform.rotation.eulerAngles.z;
	}

	// Update is called once per frame
	void Update()
	{
		Look();
		
	}
	private void FixedUpdate()
	{
		Move();
	}
	private void Move()
	{

		Vector3 camForward = cameraRoot.forward;
		Vector3 camRight = cameraRoot.right;
		if(!InZeroG)
		{
			//If not in 0G don't move the player up/down in the direction they are looking.
			camForward.y = 0f;
			camRight.y = 0f;
		}
		camForward.Normalize();
		camRight.Normalize();

		

		// Convert 2D input to 3D movement
		Vector3 direction = camForward * InputVector.y + camRight * InputVector.x;

		// Move the player
		rb.MovePosition(rb.position + direction * MoveSpeed * Time.fixedDeltaTime);

	}
	private void Look()
	{
		Vector2 targetLook = new Vector2(LookVector.x * LookSensitivityX, LookVector.y * LookSensitivityY);
		
		if (targetLook.magnitude < 0.05f)
			targetLook = Vector2.zero;

		currentLook = Vector2.SmoothDamp(currentLook, targetLook, ref smoothLookVelocity, smoothTime);

		transform.Rotate(currentLook.x * Vector3.up);

		pitch -= currentLook.y;
		pitch = Mathf.Clamp(pitch, -verticalClamp, verticalClamp);
		cameraRoot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
		

	}

	public void SetInputVector(Vector2 moveAmount)
	{
		InputVector = moveAmount;
	}

	public void SetLookVector(Vector2 lookAmount)
	{
		LookVector = lookAmount;
	}

	void OnGUI()
	{
		GUILayout.Label("Move: " + InputVector);
		GUILayout.Label("Look: " + LookVector);
	}
}
