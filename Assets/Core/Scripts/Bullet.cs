using UnityEngine;

public class Bullet : MonoBehaviour {

	public float force = 1f;
    public GameObject shooter;

    void Start () {
        GetComponent<MeshRenderer>().material.color = shooter.GetComponent<MeshRenderer>().material.color;
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void Update ()
    {
        GetComponent<MeshRenderer>().material.color = shooter.GetComponent<MeshRenderer>().material.color;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Enemy>() || collider.GetComponent<Player>())
        {
            collider.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
        }
    }

}
