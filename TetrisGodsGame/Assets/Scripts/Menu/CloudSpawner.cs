using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudsToSpawn;
    public float spawnRate = 15f;
    private float spawnRateMax;

    private void Start()
    {
        spawnRateMax = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        spawnRate -= Time.deltaTime;

        if (spawnRate < 0)
        {
            Vector3 spawnLocation = transform.position;
            spawnLocation.y += Random.Range(-1.5f,1.5f);

            Instantiate(cloudsToSpawn[Random.Range(0,cloudsToSpawn.Length-1)],spawnLocation,Quaternion.identity);

            spawnRate = Random.Range(spawnRateMax/2,spawnRateMax);
        }



    }
}
