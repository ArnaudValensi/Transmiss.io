using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource mainSound;
    public AudioSource endSound;

    // Use this for initialization
    void Start () {
    }


    public void Init()
    {
        mainSound.Stop();
        endSound.Stop();
        mainSound.Play();
        endSound.Play();
        mainSound.volume = 1f;
        endSound.volume = 0f;
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
