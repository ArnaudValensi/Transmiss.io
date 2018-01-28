using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using System;
using UnityEngine;

public class Timer : MonoBehaviour {

    float timeCount;
    float previousTime;

    // Use this for initialization
    void Start () {
        timeCount = 100f;
        previousTime = timeCount;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale != 0)
            timeCount = timeCount - Time.deltaTime;
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
		text.SetText(String.Format("{0}", Math.Round(timeCount, 0)));
        previousTime = timeCount;
    }
}
