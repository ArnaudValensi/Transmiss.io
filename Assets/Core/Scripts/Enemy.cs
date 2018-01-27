using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public float rotationSpeed;

    EnemyManager enemyManager;
	GameManager gameManager;
    Shoot shoot;
    NavMeshAgent agent;
    Transform indicator;
    Color ownColor;

    float time = 0;
    float previousTime = 0;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
		shoot = GetComponent<Shoot>();
		enemyManager = GameObject.Find("/Managers/EnemyManager").GetComponent<EnemyManager>();
		gameManager = GameManager.Instance;
		indicator = transform.Find("IndicatorHolder/Indicator");
        ownColor = GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (Mathf.Ceil(time) != Mathf.Ceil(previousTime))
        {
            Vector3 targetPoint = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            agent.SetDestination(targetPoint);
            if (Random.Range(0, 100) > 25)
                StartCoroutine("Shoot");
        }
        
        previousTime = time;
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
		shoot.LoadShoot();

        yield return new WaitForSeconds(Random.Range(0, 2000) / 1000);
		Vector3 targetPos;

        agent.isStopped = true;
        float randomCastTime = Random.Range(0, 3000) / 1000;
        // Shoot casting
        for (float f = 0f; f <= randomCastTime; f += Time.deltaTime)
        {
            targetPos = findClosestTarget();
            //targetPos = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            float strength = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
			var rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);
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
