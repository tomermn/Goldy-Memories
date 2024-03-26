using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;


/// <summary>
/// Represents a collectible item in the game that can be picked up by the player.
/// </summary>
public class Item : MonoBehaviour, ICollectable
{
    private static readonly List<Transform> UsedSpawnPoints = new List<Transform>(); // List of spawn points already used for item placement
    private bool isCollected = false;                   // Flag indicating whether the item has been collected



    
    /// <summary>
    /// Collects the item, adds it to the player's inventory, and destroys the item object.
    /// </summary>
    public void OnCollect()
    {
        Inventory inventory = FindAnyObjectByType<Inventory>();
        if (inventory != null )
        {
            inventory.AddToInvetory(this);
            Destroy(gameObject);   
        }
    }

    /// <summary>
    /// Handles the item being triggered by the player.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerMovement.PlayerTag) && !isCollected)
        {
            
            isCollected = true;
            OnCollect();

        }
    }

    private void Start()
    {
        SpawnRandomly();
    }
    
    /// <summary>
    /// Spawns the item at a random available spawn point.
    /// </summary>
    private void SpawnRandomly()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag(Constants.ItemSpawnPointTag);

        List<Transform> availableSpawnPoints = new List<Transform>();

        foreach (var spawnPoint in spawnPoints)
        {
            if (!UsedSpawnPoints.Contains(spawnPoint.transform))
            {
                availableSpawnPoints.Add(spawnPoint.transform);
            }
        }

        if (availableSpawnPoints.Count > 0)
        {
            // Choose a random available spawn point index.
            int randomIndex = Random.Range(0, availableSpawnPoints.Count);
            Transform chosenSpawnPoint = availableSpawnPoints[randomIndex];

            // Set the position of the collectible to the chosen spawn point's position.
            transform.position = chosenSpawnPoint.position;

            // Mark the chosen spawn point as used.
            UsedSpawnPoints.Add(chosenSpawnPoint);
        }
    }
}
