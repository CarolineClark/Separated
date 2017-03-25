using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Rigidbody rigidBody;
	public float speed = 10;
	public float torqueSpeed = 400;

	void Start() {
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Update() {
		Move();
	}

	void FluidWalking() {
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		transform.position += move * speed * Time.deltaTime;
	}

	private void Move()
	{
		var movementDelta = 1f;
		var rotationDelta = 0f;

		var targetDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
		var move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
	
		var currentDirection = transform.forward;
		var currentAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;
		var targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
		var deltaAngle = Mathf.DeltaAngle(currentAngle, targetAngle);
		Debug.Log (deltaAngle);
		if (Mathf.Abs(deltaAngle) < 4) {
			deltaAngle = 0;
		}
		rotationDelta = deltaAngle/180f;
		rigidBody.velocity = (move * speed  * movementDelta);
		rigidBody.angularVelocity = new Vector3(0, 0, rotationDelta * torqueSpeed);
	}
}
