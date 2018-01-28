using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;

public class LeaderBoard : MonoBehaviour {

//	[SerializeField] Dictionary<string, PlayerInfo> players;
	[SerializeField] GameObject playerTextPrefab;
	List<GameObject> playerTexts;
	GameObject playersLayout;

	public void Init() {
		Debug.Log("Start");
//		players = new Dictionary<string, PlayerInfo>();
		playerTexts = new List<GameObject>();

		playersLayout = transform.Find("Panel/PlayersLayout").gameObject;

		for (int i = 0; i < 26; i++) {
			GameObject newPlayerText = Instantiate(playerTextPrefab, playersLayout.transform);
			playerTexts.Add(newPlayerText);
		}

//		AddPlayer("Player 1", "00FF00FF", 30);
//		AddPlayer("Player 2", "0000FFFF", 10);
//		AddPlayer("Player 3", "FF0000FF", 20);
//		AddPlayer("Player 4", "00FF00FF", 50);
//		AddPlayer("Player 5", "00FF00FF", 40);
	}

	public void DisplayTeams(Dictionary<Color, List<GameObject>> teamsInfo) {
//		Debug.Log("DisplayTeams");

		var playersByScore = teamsInfo
			.OrderByDescending(pair => pair.Value.Count)
			.ToDictionary(pair => pair.Key, pair => pair.Value);

		int i = 0;
		foreach (var player in playersByScore) {
			Color teamColor = player.Key;

//			Debug.Log("i: " + i);

			foreach (var playerGameObject in player.Value) {
//				Debug.Log(String.Format("{0}. {1}", i, playerGameObject.name));
				TextMeshProUGUI text = playerTexts[i].GetComponent<TextMeshProUGUI>();
				text.SetText(String.Format("{0}. {1}", i + 1, playerGameObject.name));
				text.color = teamColor;

				i++;
			}
		}
	}

//	public void AddPlayer(string name, string color, int score) {
//		players.Add(name, new PlayerInfo(color, score));
//
//		GameObject newPlayerText = Instantiate(playerTextPrefab, playersLayout.transform);
//		playerTexts.Add(newPlayerText);
//
//		UpdateRank();
//	}
//
//	public void RemovePlayer(string name) {
//		players.Remove(name);
//		GameObject playerText = playerTexts.PopFirst();
//		Destroy(playerText);
//	}
//
//	public void SetPlayerScore(string name, int newScore) {
//		players[name].score = newScore;
//		UpdateRank();
//	}
//
//	void UpdateRank() {
//		var playersByScore = players
//			.OrderByDescending(pair => pair.Value.score)
//			.ToDictionary(pair => pair.Key, pair => pair.Value.color);
//
//		int i = 0;
//		foreach (var player in playersByScore) {
//			TextMeshProUGUI text = playerTexts[i].GetComponent<TextMeshProUGUI>();
//
//			text.SetText(String.Format("{0}. {1}", i + 1, player.Key));
//			text.color = HexToColor(player.Value);
//			i++;
//		}
//	}
//
//	Color HexToColor(string hex) {
//		byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
//		byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
//		byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
//		return new Color32(r,g,b, 255);
//	}
//
//	class PlayerInfo {
//		public PlayerInfo(string color, int score) {
//			this.color = color;
//			this.score = score;
//		}
//
//		public string color;
//		public int score;
//	}
}
