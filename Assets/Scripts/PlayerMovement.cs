﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Rigidbody2D rigidBody;
	public float speed = 10;
	public float torqueSpeed = 400;

	void Start() {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		Rotate ();
		Move();
	}

	void Rotate() {
		var rotationDelta = 0f;
		var targetDirection = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (targetDirection.magnitude == 0) {
			rigidBody.angularVelocity = 0f;
			return;
		}
		var currentDirection = transform.forward;
		var currentAngle = Mathf.Atan2(currentDirection.x, currentDirection.y) * Mathf.Rad2Deg;
		var targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg;
		var deltaAngle = Mathf.DeltaAngle(currentAngle, targetAngle);
		rotationDelta = -deltaAngle/180f;
		rigidBody.angularVelocity = rotationDelta * torqueSpeed;
	}

	private void Move() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector2 inputDirection = new Vector2(moveHorizontal, moveVertical);
		rigidBody.velocity = (inputDirection * speed);
	}
}
