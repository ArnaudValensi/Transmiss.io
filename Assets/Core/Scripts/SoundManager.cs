using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource mainSound;
    public AudioSource endSound;

    float timeCount;
    float previousTime;
    GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameManager.Instance;
        timeCount = 100f;
        previousTime = timeCount;
    }

    // Update is called once per frame
    void Update () {
        timeCount = timeCount - Time.deltaTime;
        print("Time count " + timeCount + " et prev " + previousTime);
        if (timeCount < 20 && previousTime >= 20)
        {
            StartCoroutine("FadeEndSound");
        }
        if (timeCount < 0 && previousTime >= 0)
        {
            timeCount = 0;
            gameManager.EndGame();
        }
        previousTime = timeCount;
    }

    IEnumerator FadeEndSound()
    {
        while (endSound.volume != 1)
        {
            endSound.volume += 0.08f;
            mainSound.volume -= 0.08f;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
