using UnityEngine;

public class PlayerShake : MonoBehaviour {

	CameraShake cameraShake;

	void Start () {
		cameraShake = GameObject.Find("/CameraHolder/Camera").GetComponent<CameraShake>();
	}
	
	public void StartScreenShake() {
		cameraShake.EnableShake();
	}

	public void StopScreenShake() {
		cameraShake.DisableShake();
	}

}
