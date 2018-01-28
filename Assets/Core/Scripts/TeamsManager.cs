using System.Collections.Generic;
using UnityEngine;

public class TeamsManager : MonoBehaviour  {
	Dictionary<Color, List<GameObject>> teamsInfo;

	public void Init(List<Color> colors) {
		teamsInfo = new Dictionary<Color, List<GameObject>>(colors.Count);

		for (int i = 0; i < colors.Count; i++) {
			teamsInfo.Add(colors[i], new List<GameObject>());
		}
	}

	public void AddToNewTeam(GameObject player) {
		Debug.Log("AddToNewTeam");
		foreach (var team in teamsInfo) {
			Debug.Log("team.Value.Count: " + team.Value.Count);
			if (team.Value.Count == 0) {
				Debug.Log("add to team color: " + team.Key);
				player.GetComponent<MeshRenderer>().material.color = team.Key;
				team.Value.Add(player);
				break;
			}
		}
	}

	public void AddToTeam(Color teamColor, GameObject player) {
		Debug.Log("AddToTeam");
		player.GetComponent<MeshRenderer>().material.color = teamColor;
		teamsInfo[teamColor].Add(player);
	}

	public void SwitchToTeam(GameObject player, Color toTeamColor) {
		Debug.Log("SwitchToTeam");
		Color teamColor = player.GetComponent<MeshRenderer>().material.color;

		teamsInfo[teamColor].Remove(player);
		AddToTeam(toTeamColor, player);
	}
}