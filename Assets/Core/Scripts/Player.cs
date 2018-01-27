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
	Transform indicator;

	void Start() {
		controller = GetComponent<CharacterController>();
		shoot = GetComponent<Shoot>();
		indicator = transform.Find("IndicatorHolder");
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

		UpdateIndicator();
	}

	void UpdateIndicator() {
		Quaternion rotation = Quaternion.LookRotation(direction);

		indicator.rotation = rotation;
	}

	IEnumerator LoadShoot() {
		shoot.LoadShoot();

		while (Input.GetMouseButton(0)) {
			yield return null;
		}

		isShooting = false;
		shoot.ReleaseShoot(direction);
		// shoot.DoShoot(direction, this.gameObject);
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
