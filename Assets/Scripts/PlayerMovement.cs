using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Rigidbody2D rigidBody;
	public float speed = 10;
	public float torqueSpeed = 400;

	public Sprite frontImage;
	public Sprite leftImage;
	public Sprite rightImage;
	public Sprite backImage;

	private SocketClient client;
	public Camera camera;
	private Vector3 offset;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	void Start() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator>();
		rigidBody = GetComponent<Rigidbody2D> ();
		client = new SocketClient ();
		client.SetupSocket ();
		offset = camera.transform.position - transform.position;
	}

	void Update() {
		//Rotate ();
		//Move();
		JerkyWalking();
		MoveCamera ();
		WriteToSocket ();
	}

	void Rotate() {
		var rotationDelta = 0f;
		var targetDirection = new Vector2(Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (targetDirection.magnitude == 0) {
			rigidBody.angularVelocity = 0f;
			return;
		}
		var currentDirection = transform.up;
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

	void JerkyWalking() {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.position += Vector3.left * speed * Time.deltaTime;
			animator.SetBool ("left", true);
			animator.SetBool ("front", false);
			animator.SetBool ("back", false);
			animator.SetBool ("right", false);
			animator.SetFloat ("speed", speed);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.position += Vector3.right * speed * Time.deltaTime;
			animator.SetBool ("right", true);
			animator.SetBool ("front", false);
			animator.SetBool ("back", false);
			animator.SetBool ("left", false);
			animator.SetFloat ("speed", speed);
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			transform.position += Vector3.up * speed * Time.deltaTime;
			animator.SetBool ("back", true);
			animator.SetBool ("front", false);
			animator.SetBool ("right", false);
			animator.SetBool ("left", false);
			animator.SetFloat ("speed", speed);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			transform.position += Vector3.down * speed * Time.deltaTime;
			animator.SetBool ("front", true);
			animator.SetBool ("right", false);
			animator.SetBool ("back", false);
			animator.SetBool ("left", false);
			animator.SetFloat ("speed", speed);
		}
		if(Input.anyKey == false)
		{
			animator.SetFloat ("speed", 0);
		}
		Debug.Log (animator.GetBool ("left"));
	}

	private void MoveCamera() {
		camera.transform.position = transform.position + offset;
	}

	private void WriteToSocket () {
		string message = transform.position.x + "," + transform.position.y + "\n";
		client.WriteSocket (message);
	}
}
