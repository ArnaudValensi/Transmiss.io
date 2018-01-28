using System.Collections.Generic;
using UnityEngine;

public class ColorComparer : IEqualityComparer<Color> {
	public bool Equals(Color x, Color y) { 
		return ((int)(x.r * 1000) == (int)(y.r * 1000))
		&& ((int)(x.g * 1000) == (int)(y.g * 1000))
		&& ((int)(x.b * 1000) == (int)(y.b * 1000))
		&& ((int)(x.a * 1000) == (int)(y.a * 1000));
	}

	public int GetHashCode(Color x)
	{
		unchecked
		{
			int result = (int)(x.r * 1000);
			result = (result*397) ^ (int)(x.g * 1000);
			result = (result*397) ^ (int)(x.b * 1000);
			result = (result*397) ^ (int)(x.a * 1000);
			return result;
		}
	}
}

public class TeamsManager : MonoBehaviour {
	Dictionary<Color, List<GameObject>> teamsInfo;
	LeaderBoard leaderBoard;

	public void Init(List<Color> colors) {
		leaderBoard = GameObject.Find("LeaderBoardCanvas").GetComponent<LeaderBoard>();
		teamsInfo = new Dictionary<Color, List<GameObject>>(colors.Count, new ColorComparer());

		for (int i = 0; i < colors.Count; i++) {
			teamsInfo.Add(colors[i], new List<GameObject>());
		}

		leaderBoard.Init();
	}

	public void AddToNewTeam(GameObject player) {
		Debug.Log("AddToNewTeam: " + player.name);
		foreach (var team in teamsInfo) {
			if (team.Value.Count == 0) {
				player.GetComponent<MeshRenderer>().material.color = team.Key;
				team.Value.Add(player);
				break;
			}
		}

		leaderBoard.DisplayTeams(teamsInfo);
	}

	public void AddToTeam(Color teamColor, GameObject player) {
		Debug.Log("AddToTeam");
		player.GetComponent<MeshRenderer>().material.color = teamColor;
		teamsInfo[teamColor].Add(player);
		leaderBoard.DisplayTeams(teamsInfo);
	}

	public void SwitchToTeam(GameObject player, Color toTeamColor) {
		Debug.Log("SwitchToTeam");
		Color teamColor = player.GetComponent<MeshRenderer>().material.color;

		teamsInfo[teamColor].Remove(player);
		AddToTeam(toTeamColor, player);
		leaderBoard.DisplayTeams(teamsInfo);
	}
}