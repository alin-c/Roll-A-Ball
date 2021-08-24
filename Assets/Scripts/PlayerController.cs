using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour {
	public float speed = 0;
	public TextMeshProUGUI countText;
	public GameObject winTextObject;

	private Rigidbody playerRigidbody;

	[SerializeField]
	private GameObject pickupParent;

	private int collectibleCount;
	private int hitCounter;

	private float movementX;
	private float movementY;

	void Start() {
		playerRigidbody = GetComponent<Rigidbody>();
		collectibleCount = pickupParent.transform.childCount;
		hitCounter = 0;
		SetCountText();

		winTextObject.gameObject.SetActive(false);
	}

	void OnMove(InputValue movementValue) {
		Vector2 movementVector = movementValue.Get<Vector2>();
		movementX = movementVector.x;
		movementY = movementVector.y;
	}

	void SetCountText() {
		countText.text = "Count: " + hitCounter.ToString();

		if (hitCounter >= collectibleCount) {
			winTextObject.gameObject.SetActive(true);
		}
	}

	void FixedUpdate() {
		Vector3 movement = new Vector3(movementX, 0.0f, movementY);
		playerRigidbody.AddForce(movement * speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("PickUp")) {
			other.gameObject.SetActive(false);
			hitCounter++;

			SetCountText();
		}
	}
}
