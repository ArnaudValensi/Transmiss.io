using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager> {
	public Vector2 boundsMin;
	public Vector2 boundsMax;
	public List<GameObject> entityList;
	public List<Color> colors;

	TeamsManager teamsManager;
	EnemyManager enemyManager;
	GameObject leaderBoardCanvas;
	GameObject startCanvas;
	Player player;

	void Start() {
		Time.timeScale = 0;
		leaderBoardCanvas = GameObject.Find("/LeaderBoardCanvas");
		startCanvas = GameObject.Find("/StartCanvas");
		teamsManager = GameObject.Find("/Managers/TeamsManager").GetComponent<TeamsManager>();
		player = GameObject.Find("/Environment/Character <--|").GetComponent<Player>();
		enemyManager = GameObject.Find("/Managers/EnemyManager").GetComponent<EnemyManager>();

		leaderBoardCanvas.SetActive(false);
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			EndGame();
		}
	}

	void EndGame() {
		Debug.Log("End game");

		Time.timeScale = 0;
		startCanvas.SetActive(true);
	}

//	public void ResetGame() {
//		Debug.Log("ResetGame");
//
//		leaderBoardCanvas.SetActive(false);
//	}

	public void StartGame() {
		Debug.Log("StartGame");

		startCanvas.SetActive(false);
		leaderBoardCanvas.SetActive(true);

		teamsManager.Init(colors);
		enemyManager.Init();
		player.Init();

		Time.timeScale = 1;
	}

	public void setTeam(GameObject enemy) {
		teamsManager.AddToNewTeam(enemy);
	}

}
