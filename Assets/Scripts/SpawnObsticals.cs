using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObsticals : MonoBehaviour
{
    [SerializeField] private Transform[] spawnerLocations;

    [SerializeField] private float spawnTime = 2f;

    private float currentTimer;

    [SerializeField] private GameObject obstaclePrefab;

    private int currentSpawnerID = 0;
    private void Start()
    {
        currentTimer = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (currentTimer <= 0)
        {
            Instantiate(obstaclePrefab, spawnerLocations[currentSpawnerID].transform.position, Quaternion.identity);
            currentSpawnerID += 1;
            if (currentSpawnerID == spawnerLocations.Length)
            {
                currentSpawnerID = 0;
            }

            currentTimer = spawnTime;
        }
    }
}
