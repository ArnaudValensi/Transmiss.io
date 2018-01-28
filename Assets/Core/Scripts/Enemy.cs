using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float rotationSpeed;
    GameManager gameManager;
    Shoot shoot;
    NavMeshAgent agent;
    Transform indicator;
    Color ownColor;
	TeamsManager teamsManager;

    int behaviour = 0;

    void Start () {
		teamsManager = GameObject.Find("/Managers/TeamsManager").GetComponent<TeamsManager>();
        agent = GetComponent<NavMeshAgent>();
		shoot = GetComponent<Shoot>();
		gameManager = GameManager.Instance;
		indicator = transform.Find("IndicatorHolder/Indicator");
        ownColor = GetComponent<MeshRenderer>().material.color;
        StartCoroutine("setBehaviour");
    }

    IEnumerator setBehaviour()
    {
		yield return null;
        while (true)
        {
            behaviour = Random.Range(0, 3);
            if (behaviour == 0)
                StartCoroutine("roam");
            if (behaviour == 1)
                StartCoroutine("flee");
            if (behaviour == 2)
                StartCoroutine("rush");
            yield return new WaitForSeconds(Random.Range(4000, 10000) / 1000);
        }
    }

    // BEHAVIOURS
    IEnumerator rush()
    {
        while (behaviour == 2)
        {
            Vector3 targetPoint = findClosestTarget();
            agent.SetDestination(targetPoint);
            if (Random.Range(0, 100) > 95 && agent.isStopped == false)
                StartCoroutine("Shoot");
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator flee()
    {
        while (behaviour == 1)
        {
            Vector3 threatPoint = findClosestTarget();
            Vector3 targetPoint = new Vector3(
                transform.position.x + (transform.position.x - threatPoint.x),
                transform.position.y,
                transform.position.z + (transform.position.z - threatPoint.z));
            agent.SetDestination(targetPoint);
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    IEnumerator roam()
    {
        while (behaviour == 0)
        {
            Vector3 targetPoint = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            agent.SetDestination(targetPoint);
            if (Random.Range(0, 100) > 25 && agent.isStopped == false)
                StartCoroutine("Shoot");
            yield return new WaitForSeconds(Random.Range(2000, 5000) / 1000);
        }
    }

    // Return a random point in the map
    Vector3 randomInMap(Vector2 mins, Vector2 maxes)
    {
        float x = Random.Range(mins.x, maxes.x);
        float y = Random.Range(mins.y, maxes.y);

        Vector3 targetPoint = new Vector3(x, 0f, y);
        return (targetPoint);
    }

    // Return a character to target
    Vector3 findClosestTarget()
    {
        Vector3 targetPos = new Vector3(100, 100, 100);
        float dist = Vector3.Distance(transform.position, targetPos);
        for (int i = 0; i < gameManager.entityList.Count; i++)
        {
            GameObject entity = gameManager.entityList[i];
            Color targetColor = entity.GetComponent<MeshRenderer>().material.color;
            float dist2 = Vector3.Distance(transform.position, entity.GetComponent<Transform>().position);
            if(dist2 < dist && dist2 != 0 && ownColor != targetColor)
            {
                targetPos = gameManager.entityList[i].transform.position;
                dist = dist2;
            }
        }
        return (targetPos);
    }

    IEnumerator Shoot()
    {
        agent.isStopped = true;
		shoot.LoadShoot();

		Vector3 targetPos;

        float randomCastTime = Random.Range(0, 3000) / 1000;
        // Shoot casting
        for (float f = 0f; f <= randomCastTime; f += Time.deltaTime)
        {
            targetPos = findClosestTarget();
            //targetPos = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            float strength = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
			Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);
			transform.SetRotationY(rotation.eulerAngles.y);
            yield return null;
        }
        // Cast done, shoot
        Vector3 shootVector = (indicator.position - transform.position);
        shootVector.Normalize();
		shoot.ReleaseShoot(shootVector);
        agent.isStopped = false;
    }

}
