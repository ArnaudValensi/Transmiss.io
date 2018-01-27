using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public GameObject player;
    public EnemyManager enemyManager;
    public GameManager gameManager;
    public float rotationSpeed;

    Shoot shoot;
    NavMeshAgent agent;

    float time = 0;
    float previousTime = 0;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
		shoot = GetComponent<Shoot>();
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (Mathf.Ceil(time) != Mathf.Ceil(previousTime))
        {
            Vector3 targetPoint = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            agent.SetDestination(targetPoint);
        }

        if (Input.GetButtonDown("Fire1"))
        {
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
        for (int i = 0; i < enemyManager.characterList.Length; i++)
        {
            float dist2 = Vector3.Distance(transform.position, enemyManager.characterList[i].GetComponent<Transform>().position);
            if(dist2 < dist && dist2 != 0)
            {
                targetPos = enemyManager.characterList[i].transform.position;
                dist = dist2;
            }
        }
        return (targetPos);
    }

    IEnumerator Shoot()
    {
        Vector3 targetPos = new Vector3();


        agent.isStopped = true;
        float randomCastTime = Random.value * 5;
        // Shoot casting
        for (float f = 0f; f <= randomCastTime; f += Time.deltaTime)
        {
            targetPos = findClosestTarget();
            //targetPos = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
            float strength = Mathf.Min(rotationSpeed * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);
            yield return null;
        }
        print(transform.rotation);
        // Cast done, shoot
        Vector3 shootVector = targetPos - transform.position;
        shootVector.Normalize();
        shoot.DoShoot(shootVector);
        agent.isStopped = false;
    }
}
