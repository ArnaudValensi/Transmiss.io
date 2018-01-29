using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager> {
	public Vector2 boundsMin;
	public Vector2 boundsMax;
	public List<GameObject> entityList;
	public List<Color> colors;
    public Timer timer;
	public bool isGameStarted;

	TeamsManager teamsManager;
    EnemyManager enemyManager;
    SoundManager soundManager;
    GameObject leaderBoardCanvas;
	GameObject startCanvas;
	Player player;
	public GameObject enemiesHolder;
	public GameObject bulletsHolder;


	void Start() {
		Time.timeScale = 0;
		leaderBoardCanvas = GameObject.Find("/LeaderBoardCanvas");
		startCanvas = GameObject.Find("/StartCanvas");
		teamsManager = GameObject.Find("/Managers/TeamsManager").GetComponent<TeamsManager>();
		player = GameObject.Find("/Environment/Character <--|").GetComponent<Player>();
		enemyManager = GameObject.Find("/Managers/EnemyManager").GetComponent<EnemyManager>();
		soundManager = GameObject.Find("/Managers/SoundManager").GetComponent<SoundManager>();
		enemiesHolder = GameObject.Find("Environment/EnemiesHolder");
		bulletsHolder = GameObject.Find("Environment/BulletHolder");

		leaderBoardCanvas.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			EndGame();
		}
    }

    public void EndGame() {
		Time.timeScale = 0;
		isGameStarted = false;
		startCanvas.SetActive(true);
	}


	void Reset() {
		// Remove bullets
		foreach (Transform child in enemiesHolder.transform) {
			Object.DestroyImmediate(child.gameObject);
		}

		// Remove enemies
		foreach (Transform child in enemiesHolder.transform) {
			Object.Destroy(child.gameObject);
		}
	}

	public void StartGame() {
		Reset();
        timer.SetTimer(100);

        startCanvas.SetActive(false);
		leaderBoardCanvas.SetActive(true);

        soundManager.Init();
		teamsManager.Init(colors);
		enemyManager.Init();
		player.Init();


        isGameStarted = true;
		Time.timeScale = 1;
	}

	public void setTeam(GameObject enemy) {
		teamsManager.AddToNewTeam(enemy);
	}

}
