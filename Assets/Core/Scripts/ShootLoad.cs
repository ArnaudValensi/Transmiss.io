using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Events;

public class ShootLoad : MonoBehaviour {

	public int nbIndicators = 5;
	public float indicatorsOffset = 0.25f;
	public GameObject indicatorPrefab;
	public float increaseTime = 0.5f;
	public UnityEvent OnShootMaxLoad;
	public UnityEvent OnShootStop;

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

		OnShootMaxLoad.Invoke();
	}

	public void Reset() {
		StopCoroutine("Load");

		OnShootStop.Invoke();

		for (int i = 0; i < indicators.Count; i++) {
			indicators[i].SetActive(false);
		}
	}

}
