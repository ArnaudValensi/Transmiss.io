using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;

	Transform bulletHolder;
	ShootLoad shootLoad;

	void Start() {
		bulletHolder = GameObject.Find("/Environment/BulletsHolder").transform;
		shootLoad = transform.Find("IndicatorHolder/ShootLoad").GetComponent<ShootLoad>();
	}

	public void LoadShoot() {
        shootLoad.StartLoading();
	}

	public void ReleaseShoot(Vector3 direction) {
		Quaternion rotation = Quaternion.LookRotation(direction);

		shootLoad.Reset();
		GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation, bulletHolder);
		bullet.GetComponent<Bullet>().shooter = gameObject;
	}

}
