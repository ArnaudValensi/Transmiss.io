using UnityEngine;

public class SpriteHolder : MonoBehaviour {

	SpriteRenderer spriteRenderer;

	public void Init() {
		Debug.Log("Start SpriteHolder");
		spriteRenderer = transform.Find("Sprite").GetComponent<SpriteRenderer>();
	}

	public SpriteRenderer GetSpriteRenderer() {
		return spriteRenderer;
	}
}
