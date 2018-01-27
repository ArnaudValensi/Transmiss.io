using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviourSingletonPersistent<GameManager> {
	public Vector2 boundsMin;
	public Vector2 boundsMax;
	public List<GameObject> entityList;
	public List<Color> colors;
	public List<int> entitiesOfColors;

	Dictionary<Color, int> colorsEntities;

	void Start() {
		InitiateColorsEntities();
	}

	void InitiateColorsEntities() {
		colorsEntities = new Dictionary<Color, int>();

		for (int i = 0; i < colors.Count; i++) {
			colorsEntities.Add(colors[i], entitiesOfColors[i]);
		}
	}

	public void setTeam(GameObject enemy) {
		int j = 0;
		while (entitiesOfColors[j] != 0)
			j++;
		enemy.GetComponent<MeshRenderer>().material.color = colors[j];
		entitiesOfColors[j] += 1;
	}

	public void removeTeam(GameObject enemy) {
		Color tmpColor = enemy.GetComponent<MeshRenderer>().material.color;
		for (int i = 0; i < entitiesOfColors.Count; i++)
		{
			if (tmpColor == colors[i])
				entitiesOfColors[i]--;
		}
	}
}
