using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private static List<Transform> usedSpawnPoints = new List<Transform>();


    private void Start()
    {
        SpawnRandomally();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Collect(other.gameObject);

        }
    }

    private void Collect(GameObject player)
    {
        Inventory inventory = FindAnyObjectByType<Inventory>();
        if (inventory != null)
        {
            inventory.AddToInvetory(this);
            Destroy(gameObject);
        }
    }

    private void SpawnRandomally()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawnPoint");

        List<Transform> availableSpawnPoints = new List<Transform>();

        foreach (var spawnPoint in spawnPoints)
        {
            if (!usedSpawnPoints.Contains(spawnPoint.transform))
            {
                availableSpawnPoints.Add(spawnPoint.transform);
            }
        }

        if (availableSpawnPoints.Count > 0)
        {
            // Choose a random available spawn point index.
            int randomIndex = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
            Transform chosenSpawnPoint = availableSpawnPoints[randomIndex];

            // Set the position of the collectible to the chosen spawn point's position.
            transform.position = chosenSpawnPoint.position;

            // Mark the chosen spawn point as used.
            usedSpawnPoints.Add(chosenSpawnPoint);
        }
    }
}
