using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public GameObject enemyPrefab;
    public GameObject enemiesHolder;
    public int nbEnnemies;

    GameManager gameManager;

	void Start () {
        gameManager = GameManager.Instance;
    }

	public void Init() {
		Debug.Log("Init enemy manager");
		instantiateEnnemies();
	}

    void instantiateEnnemies()
    {
        gameManager.entityList = new List<GameObject>();
        for (int i = 0; i < nbEnnemies; i++)
        {
            Vector3 randomPos = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            GameObject enemy = Instantiate(enemyPrefab, randomPos, Quaternion.AngleAxis(45, Vector3.up), enemiesHolder.transform);
			enemy.name = string.Format("Enemy {0}", i);
            GameManager.Instance.setTeam(enemy);
            gameManager.entityList.Add(enemy);
        }
    }

    Vector3 randomInMap(Vector2 mins, Vector2 maxes)
    {
        float x = Random.Range(mins.x, maxes.x);
        float y = Random.Range(mins.y, maxes.y);

		return new Vector3(x, 0f, y);
    }
}
