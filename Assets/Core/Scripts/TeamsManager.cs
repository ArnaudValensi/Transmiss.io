using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
	public Sprite[] sprites;

	Dictionary<Color, List<GameObject>> teamsInfo;
	LeaderBoard leaderBoard;
	bool isAllPlayersLoaded;

	public void Init(List<Color> colors) {
		leaderBoard = GameObject.Find("LeaderBoardCanvas").GetComponent<LeaderBoard>();
		teamsInfo = new Dictionary<Color, List<GameObject>>(colors.Count, new ColorComparer());

		isAllPlayersLoaded = false;

		for (int i = 0; i < colors.Count; i++) {
			teamsInfo.Add(colors[i], new List<GameObject>());
		}

		leaderBoard.Init();
	}

	public void SetAllPlayersLoaded() {
		isAllPlayersLoaded = true;
	}

	public void AddToNewTeam(GameObject player) {
		foreach (var team in teamsInfo) {
			if (team.Value.Count == 0) {
				player.GetComponent<MeshRenderer>().material.color = team.Key;
				team.Value.Add(player);
				break;
			}
		}

		UpdatePlayerInfo();
	}

	public void AddToTeam(Color teamColor, GameObject player) {
		player.GetComponent<MeshRenderer>().material.color = teamColor;
		teamsInfo[teamColor].Add(player);
		UpdatePlayerInfo();
	}

	public void SwitchToTeam(GameObject player, Color toTeamColor) {
		Color teamColor = player.GetComponent<MeshRenderer>().material.color;

		teamsInfo[teamColor].Remove(player);
		AddToTeam(toTeamColor, player);
		UpdatePlayerInfo();
	}

	void UpdatePlayerInfo() {
		leaderBoard.DisplayTeams(teamsInfo);
		UpdateRankSpriteAndColor();
		CheckEndGame();
	}

	void UpdateRankSpriteAndColor() {
		if (!isAllPlayersLoaded) {
			return;
		}

		foreach (var team in teamsInfo) {
			Color teamColor = team.Key;
			List<GameObject> players = team.Value;
			int playersCount = players.Count;

			int i = 0;
			foreach (var player in players) {
				SpriteHolder spriteHolder = player.GetComponent<SpriteHolder>();
				SpriteRenderer playerSprite = spriteHolder.GetSpriteRenderer();

				playerSprite.sprite = sprites[playersCount - 1];
				playerSprite.color = teamColor;
				i++;
			}
		}
	}

	void CheckEndGame() {
		if (!isAllPlayersLoaded) {
			return;
		}

		var playersByScore = teamsInfo
			.OrderByDescending(pair => pair.Value.Count)
			.Select(pair => pair.Value.Count)
			.ToList();

		if (playersByScore[1] == 0) {
			GameManager.Instance.EndGame();
		}
	}
}