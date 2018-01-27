﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour {

    public GameObject player;
    public GameManager gameManager;
    NavMeshAgent agent;
    float time = 0;
    float previousTime = 0;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update () {
        time += Time.deltaTime;
        if (Mathf.Ceil(time) != Mathf.Ceil(previousTime))
        {
            Vector3 targetPoint = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
            agent.SetDestination(targetPoint);
        }

        if (Input.GetButtonDown("Fire1"))
        {
            shoot();
        }
        previousTime = time;
    }


    


    // Return a random point in the map
    Vector3 randomInMap(Vector2 mins, Vector2 maxes)
    {
        float x = Random.Range(mins.x, maxes.x);
        float y = Random.Range(mins.y, maxes.y);

        Vector3 targetPoint = new Vector3(x, 0f, y);
        return (targetPoint);
    }

    void shoot()
    {
        Vector3 randomTarget = randomInMap(gameManager.boundsMin, gameManager.boundsMax);
        Vector3 shootVector = randomTarget - GetComponent<Transform>().position;
        float randomCastTime = Random.value * 5;
        shootVector.Normalize();
    }
}
