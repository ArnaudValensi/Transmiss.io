using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    public Light[] beatLights;

	// Use this for initialization
	void Start () {
        StartCoroutine("LightBeat");
	}

    IEnumerator LightBeat()
    {
        while (true)
        {
            StartCoroutine("SingleBeat");
            yield return new WaitForSeconds(0.4f);
        }
    }

    IEnumerator SingleBeat()
    {
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < beatLights.Length; j++)
            {
                beatLights[j].intensity += 0.8f;
            }
            yield return null;
        }
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < beatLights.Length; j++)
            {
                beatLights[j].intensity -= 0.8f;
            }
            yield return null;
        }
    }
}
