using UnityEngine;

public class Player : MonoBehaviour {
	public float movementSpeed = 0.2f;
	public float rotationSpeed = 1f;
	public Transform cameraTransform;

	CharacterController controller;

	void Start() {
		controller = GetComponent<CharacterController>();
	}

	void Update() {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		// Movement
		Vector3 movement = new Vector3(
			x * movementSpeed * Time.deltaTime,
			0f,
			y * movementSpeed * Time.deltaTime
		);

        

        controller.Move(movement);
	}
}
