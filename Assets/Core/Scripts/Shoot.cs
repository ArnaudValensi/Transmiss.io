using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;

	Transform bulletHolder;

	void Start() {
		bulletHolder = GameObject.Find("/Environment/BulletsHolder").transform;
	}

	public void DoShoot(Vector3 direction, GameObject shooter) {
		Quaternion rotation = Quaternion.LookRotation(direction);
		GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation, bulletHolder);
        bullet.GetComponent<Bullet>().shooter = shooter;
    }

}
