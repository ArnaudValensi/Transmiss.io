using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameManager gameManager;
    public float force = 1f;
    public GameObject shooter;
    public float lifeSpan;
	TeamsManager teamsManager;

    void Start()
    {
        gameManager = GameManager.Instance;
		teamsManager = GameObject.Find("/Managers/TeamsManager").GetComponent<TeamsManager>();
        GetComponent<MeshRenderer>().material.color = shooter.GetComponent<MeshRenderer>().material.color;
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
        StartCoroutine("DestroyBullet");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.color = shooter.GetComponent<MeshRenderer>().material.color;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(lifeSpan);
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.GetComponent<Enemy>() || collider.GetComponent<Player>()) &&
            collider.GetComponent<MeshRenderer>().material.color != GetComponent<MeshRenderer>().material.color)
        {
			Color shooterColor = GetComponent<MeshRenderer>().material.color;
			teamsManager.SwitchToTeam(collider.gameObject, shooterColor);
            Destroy(this.gameObject);
        }
    }
}