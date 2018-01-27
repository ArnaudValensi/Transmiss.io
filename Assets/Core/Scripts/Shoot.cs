using UnityEngine;
using System;

public class Shoot : MonoBehaviour {

	public GameObject bulletPrefab;

	Transform bulletHolder;
	ShootLoad shootLoad;

	void Start() {
		bulletHolder = GameObject.Find("/Environment/BulletsHolder").transform;
		shootLoad = transform.Find("IndicatorHolder/ShootLoad").GetComponent<ShootLoad>();
	}

	[Obsolete("DoShoot is deprecated :P, use LoadShoot and ReleaseShoot.")]
	public void DoShoot(Vector3 direction) {
		Quaternion rotation = Quaternion.LookRotation(direction);

		Instantiate(bulletPrefab, transform.position, rotation, bulletHolder);
	}

	public void LoadShoot() {
		shootLoad.StartLoading();
	}

	public void ReleaseShoot(Vector3 direction) {
		Quaternion rotation = Quaternion.LookRotation(direction);

		shootLoad.Reset();
		Instantiate(bulletPrefab, transform.position, rotation, bulletHolder);
	}

}
