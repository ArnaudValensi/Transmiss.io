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

	public void Init(List<Color> colors) {
		teamsInfo = new Dictionary<Color, List<GameObject>>(colors.Count, new ColorComparer());

		for (int i = 0; i < colors.Count; i++) {
			teamsInfo.Add(colors[i], new List<GameObject>());
		}
	}

	public void AddToNewTeam(GameObject player) {
		foreach (var team in teamsInfo) {
			if (team.Value.Count == 0) {
				player.GetComponent<MeshRenderer>().material.color = team.Key;
				team.Value.Add(player);
				break;
			}
		}
	}

	public void AddToTeam(Color teamColor, GameObject player) {
		player.GetComponent<MeshRenderer>().material.color = teamColor;
		teamsInfo[teamColor].Add(player);
	}

	public void SwitchToTeam(GameObject player, Color toTeamColor) {
		Color teamColor = player.GetComponent<MeshRenderer>().material.color;

		teamsInfo[teamColor].Remove(player);
		AddToTeam(toTeamColor, player);
	}
}