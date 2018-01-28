using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager> {
	public Vector2 boundsMin;
	public Vector2 boundsMax;
	public List<GameObject> entityList;
	public List<Color> colors;

	TeamsManager teamsManager;

	void Start() {
		teamsManager = GameObject.Find("/Managers/TeamsManager").GetComponent<TeamsManager>();
		teamsManager.Init(colors);
	}

	public void setTeam(GameObject enemy) {
		teamsManager.AddToNewTeam(enemy);
	}

}
