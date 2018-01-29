using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;

public class Timer : MonoBehaviour {

    public SoundManager soundManager;
    GameManager gameManager;
    float timeCount;
    float previousTime;

    // Use this for initialization
    void Start () {
        gameManager = GameManager.Instance;
        previousTime = timeCount;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
            timeCount = timeCount - Time.deltaTime;

        if (timeCount < 20 && previousTime >= 20)
            soundManager.StartCoroutine("FadeEndSound");

        if (timeCount < 0 && previousTime >= 0)
        {
            timeCount = 0;
            gameManager.EndGame();
        }

        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
		text.SetText(String.Format("{0}", Math.Round(timeCount, 0)));
        previousTime = timeCount;
    }

    public void SetTimer(float f)
    {
        timeCount = f;
    }
}
