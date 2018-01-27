using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject enemiesHolder;
    public int nbEnnemies;

    GameManager gameManager;

	public void Start () {
        gameManager = GameManager.Instance;
        instantiateEnnemies();
    }

    void instantiateEnnemies()
    {
        gameManager.entityList = new List<GameObject>();
        for (int i = 0; i < nbEnnemies; i++)
        {
            Vector3 randomPos = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.AngleAxis(45, Vector3.up), enemiesHolder.transform);
            setTeam(enemy);
            gameManager.entityList.Add(enemy);
        }
    }

    void setTeam(GameObject enemy)
    {
        int j = 0;
        while (gameManager.entitiesOfColors[j] != 0)
            j++;
        enemy.GetComponent<MeshRenderer>().material.color = gameManager.colors[j];
        gameManager.entitiesOfColors[j] += 1;
    }

    Vector3 randomInMap(Vector2 mins, Vector2 maxes)
    {
        float x = Random.Range(mins.x, maxes.x);
        float y = Random.Range(mins.y, maxes.y);

		return new Vector3(x, 0f, y);
    }
}
