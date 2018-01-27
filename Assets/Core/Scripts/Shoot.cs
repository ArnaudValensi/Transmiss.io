using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;

	Transform bulletHolder;

	void Start() {
		bulletHolder = GameObject.Find("/Environment/BulletsHolder").transform;
	}

	public void DoShoot(Vector3 direction) {
		Quaternion rotation = Quaternion.LookRotation(direction);

		Instantiate(bulletPrefab, transform.position, rotation, bulletHolder);
	}

}
