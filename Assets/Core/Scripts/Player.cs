using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float movementSpeed = 5f;
	public float rotationSpeed = 1f;
	public Transform cameraTransform;

	CharacterController controller;
	Vector3 direction = Vector3.zero;
	Shoot shoot;
	bool isShooting = false;

	void Start() {
		controller = GetComponent<CharacterController>();
		shoot = GetComponent<Shoot>();
	}

	void Update() {
		Vector3 targetPosition;

		if (GetMousePositionInWorld(out targetPosition)) {
			direction = (targetPosition - transform.position).normalized;
			direction.y = 0f;
		}

		if (!isShooting) {
			Vector3 movement = direction * (movementSpeed * Time.deltaTime);
			controller.Move(movement);
		}

		if (Input.GetMouseButtonDown(0)) {
			isShooting = true;
			StartCoroutine(LoadShoot());
		}
	}

	IEnumerator LoadShoot() {
		Debug.Log("LoadShoot");
		while (Input.GetMouseButton(0)) {
			Debug.Log("Loading shoot");
			yield return null;
		}

		Debug.Log("Stop load shoot");
		isShooting = false;
		shoot.DoShoot(direction);
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
