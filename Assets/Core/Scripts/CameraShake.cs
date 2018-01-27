using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public bool isShaking;

	public float linearIntensity = 0.25f;
	public bool angularShaking = true;
	public float angularIntensity = 5f;

	public bool perlinShake;
	public float perlinMagnitude = 2f;
	public float perlinFrequency = 10f;

	public bool linearShaking;

	void Update() {
		if (isShaking) {
			if (perlinShake) {
				PerlinShake();
			} else if (linearShaking) {
				LinearShaking();
			} else if (angularShaking) {
				AngularShaking();
			}
		}
	}

	public void ApplyShake (Vector2 noise) {
		Vector3 newPosition = transform.localPosition;
		newPosition.x = noise.x;
		newPosition.z = noise.y;
		transform.localPosition = newPosition;
	}

	void LinearShaking() {
		Vector2 shake = Random.insideUnitCircle * linearIntensity;
		ApplyShake(shake);
	}

	void AngularShaking() {
		float shake = Random.Range(-angularIntensity, angularIntensity);
		transform.localRotation = Quaternion.Euler(0f, 0f, shake);
	}

	void PerlinShake() {
		Vector2 result;
		float seed = Time.time * perlinFrequency;
		result.x = Mathf.Clamp01(Mathf.PerlinNoise(seed, 0f)) - 0.5f;
		result.y = Mathf.Clamp01(Mathf.PerlinNoise(0f, seed)) - 0.5f; 
		result = result * perlinMagnitude;

		ApplyShake(result);
	}

	public void EnableShake() {
		isShaking = true;
	}

	public void DisableShake() {
		isShaking = false;
		transform.localPosition = Vector3.zero;
		transform.localRotation = Quaternion.identity;
	}

	int nbShakeCalls = 0;

	public void Shake(float duration) {
		if (nbShakeCalls == 0) {
			EnableShake();
		}

		nbShakeCalls++;

		StartCoroutine(StopShakeCoroutine(duration));
	}

	IEnumerator StopShakeCoroutine(float duration) {
		yield return new WaitForSeconds(duration);

		nbShakeCalls--;

		if (nbShakeCalls == 0) {
			DisableShake();
		}
	}

}