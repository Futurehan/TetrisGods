<<<<<<< HEAD:TetrisGodsGame/Assets/Scripts/ProDestroyer.cs
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProDestroyer : MonoBehaviour
{
    Rigidbody body;
    BoxCollider boxy;
    public int blocksToDestroy;
    int blocksDestroyed;
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
        boxy = GetComponent<BoxCollider>();
    }


    public void Fire(Vector3 forceDirection)
    {
        Vector3 shootForce = forceDirection.normalized * 30f;
        body.AddForce(shootForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider collision)
    {
        //Determine parent of collided object
        GameObject other = collision.gameObject;


        //Destroy collided blocks and count how many
        Destroy(collision.gameObject);
        blocksDestroyed++;

        if (blocksDestroyed >= blocksToDestroy)
        {
            boxy.enabled = false;
        }
        print(collision + " DESTROYED!!!");
    }
}