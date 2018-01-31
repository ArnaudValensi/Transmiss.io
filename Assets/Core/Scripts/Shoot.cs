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

	public void ReleaseShoot(Vector3 direction, float castTime) {
		Quaternion rotation = Quaternion.LookRotation(direction);

		shootLoad.Reset();
		GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation, bulletHolder);
		bullet.GetComponent<Bullet>().shooter = gameObject;
        if (castTime > 2)
            castTime = 2;
        bullet.GetComponent<Bullet>().force = 600f + castTime * 500f;
        bullet.GetComponent<Bullet>().lifeSpan = 0.3f + castTime * 0.3f;
        bullet.transform.localScale = new Vector3(0.5f + castTime * 0.25f, 0.5f + castTime * 0.25f, 0.5f + castTime * 0.25f);
	}
}
