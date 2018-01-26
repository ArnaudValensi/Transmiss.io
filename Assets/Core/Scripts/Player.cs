using UnityEngine;

public class Player : MonoBehaviour {
	public float movementSpeed = 0.2f;
	public float rotationSpeed = 1f;
	public Transform cameraTransform;
	public float mouseConfinementRadius;

	CharacterController controller;
	Vector3 direction = Vector3.zero;

	void Start() {
		controller = GetComponent<CharacterController>();
	}

	void Update() {
		Vector3 targetPosition;

		if (GetMousePositionInWorld(out targetPosition)) {
			direction = (targetPosition - transform.position).normalized;
			direction.y = 0f;
		}

		Vector3 movement = direction * (movementSpeed * Time.deltaTime);
		controller.Move(movement);
	}

	bool GetMousePositionInWorld(out Vector3 targetPosition) {
		Vector3 mouse = Input.mousePosition;
		Ray castPoint = Camera.main.ScreenPointToRay(mouse);
		RaycastHit hit;

		if (Physics.Raycast(castPoint, out hit, Mathf.Infinity)) {
			targetPosition = hit.point;
			return true;
		}

		targetPosition = Vector3.zero;
		return false;
	}
}
