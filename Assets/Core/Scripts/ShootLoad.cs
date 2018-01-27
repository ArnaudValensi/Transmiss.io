using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ShootLoad : MonoBehaviour {

	public int nbIndicators = 5;
	public float indicatorsOffset = 0.25f;
	public GameObject indicatorPrefab;
	public float increaseTime = 0.5f;

	List<GameObject> indicators = new List<GameObject>();

	void Start() {
		for (int i = 0; i < nbIndicators; i++) {
			GameObject indicator = Instantiate(indicatorPrefab, transform);

			indicator.transform.SetZ(indicator.transform.position.z + i * indicatorsOffset);
			indicator.SetActive(false);
			indicators.Add(indicator);
		}
	}

	public void StartLoading() {
		StartCoroutine("Load");
	}

	IEnumerator Load() {
		indicators[0].SetActive(true);

		for (int i = 1; i < nbIndicators; i++) {
			yield return new WaitForSeconds(increaseTime);
			indicators[i].SetActive(true);
		}
	}

	public void Reset() {
		StopCoroutine("Load");

		for (int i = 0; i < indicators.Count; i++) {
			indicators[i].SetActive(false);
		}
	}

}
