﻿using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameManager gameManager;
    public float force = 1f;
    public GameObject shooter;

    void Start()
    {
        gameManager = GameManager.Instance;
        GetComponent<MeshRenderer>().material.color = shooter.GetComponent<MeshRenderer>().material.color;
        GetComponent<Rigidbody>().AddForce(transform.forward * force);
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<MeshRenderer>().material.color = shooter.GetComponent<MeshRenderer>().material.color;
    }

    void OnTriggerEnter(Collider collider)
    {
        if ((collider.GetComponent<Enemy>() || collider.GetComponent<Player>()) &&
            collider.GetComponent<MeshRenderer>().material.color != GetComponent<MeshRenderer>().material.color)
        {
            int i = 0;
            bool incrementDone = false;
            bool decrementDone = false;
            while (i < gameManager.colors.Count && !decrementDone && !incrementDone)
            {
                if (collider.GetComponent<MeshRenderer>().material.color == gameManager.colors[i])
                    gameManager.entitiesOfColors[i] -= 1;
                else if (GetComponent<MeshRenderer>().material.color == gameManager.colors[i])
                    gameManager.entitiesOfColors[i] += 1;
                i++;
            }
            collider.GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color;
            Destroy(this.gameObject);
        }
    }

}