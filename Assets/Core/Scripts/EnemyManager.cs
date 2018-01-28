using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject enemiesHolder;
    public int nbEnnemies;

    GameManager gameManager;
	TeamsManager teamsManager;

	void Start () {
        gameManager = GameManager.Instance;
		teamsManager = GameObject.Find("/Managers/TeamsManager").GetComponent<TeamsManager>();
    }

	public void Init() {
		instantiateEnnemies();
	}

	void instantiateEnnemies()
    {
        gameManager.entityList = new List<GameObject>();
        for (int i = 0; i < nbEnnemies; i++)
        {
            Vector3 randomPos = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.AngleAxis(45, Vector3.up), enemiesHolder.transform);
			enemy.GetComponent<Enemy>().Init();
			enemy.name = string.Format("Enemy {0}", i);
            GameManager.Instance.setTeam(enemy);
            gameManager.entityList.Add(enemy);
        }

		teamsManager.SetAllPlayersLoaded();
    }

    Vector3 randomInMap(Vector2 mins, Vector2 maxes)
    {
        float x = Random.Range(mins.x, maxes.x);
        float y = Random.Range(mins.y, maxes.y);

		return new Vector3(x, 0f, y);
    }
}
