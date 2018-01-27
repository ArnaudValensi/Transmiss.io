using UnityEngine;

public class Bullet : MonoBehaviour {

	public float force = 1f;

	void Start() {
		GetComponent<Rigidbody>().AddForce(transform.forward * force);
	}

}
