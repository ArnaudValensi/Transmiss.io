using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;

public class LeaderBoard : MonoBehaviour {

	[SerializeField] GameObject playerTextPrefab;
	List<GameObject> playerTexts;
	GameObject playersLayout;

	public void Init() {
		playerTexts = new List<GameObject>();
		playersLayout = transform.Find("Panel/PlayersLayout").gameObject;

		Reset();

		for (int i = 0; i < 26; i++) {
			GameObject newPlayerText = Instantiate(playerTextPrefab, playersLayout.transform);
			playerTexts.Add(newPlayerText);
		}
	}

	void Reset() {
		// Remove enemies
		foreach (Transform child in playersLayout.transform) {
			UnityEngine.Object.Destroy(child.gameObject);
		}
	}

	public void DisplayTeams(Dictionary<Color, List<GameObject>> teamsInfo) {
		var playersByScore = teamsInfo
			.OrderByDescending(pair => pair.Value.Count)
			.ToDictionary(pair => pair.Key, pair => pair.Value);

		int i = 0;
		foreach (var player in playersByScore) {
			Color teamColor = player.Key;

			foreach (var playerGameObject in player.Value) {
				TextMeshProUGUI text = playerTexts[i].GetComponent<TextMeshProUGUI>();
				text.SetText(String.Format("{0}. {1}", i + 1, playerGameObject.name));
				text.color = teamColor;

				i++;
			}
		}
	}

}
