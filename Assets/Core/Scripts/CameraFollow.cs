using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float verticalOffset;
	public bool lerp = true;
	public float smoothSpeed = 5.0f;
	public bool smoothDamp = false;

	Vector3 speed = Vector3.zero;

	// Update is called once per frame
	void Update () {
		if (!target) {
			return;
		}

		if (lerp) {
			transform.position = Vector3.Lerp(
				transform.position, 
				new Vector3(target.position.x, verticalOffset, target.position.z), 
				Time.deltaTime * smoothSpeed
			);
		} else if (smoothDamp) {
			transform.position = Vector3.SmoothDamp(
				transform.position, 
				new Vector3(target.position.x, verticalOffset, target.position.z), 
				ref speed, 
				Time.deltaTime * smoothSpeed
			);
		} else {
			transform.position = new Vector3(target.position.x, verticalOffset, target.position.z);
		}
	}

}
