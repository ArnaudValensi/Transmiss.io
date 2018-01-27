using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletHolder;

	public void DoShoot(Vector3 direction) {
		Debug.Log("Shoot, direction: " + direction);

		Quaternion rotation = Quaternion.LookRotation(direction);

		Instantiate(bulletPrefab, transform.position, rotation, bulletHolder);
	}

}
