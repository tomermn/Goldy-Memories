using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles triggering the transition to the next level when the player enters the trigger zone, namely the end of the current level.
/// </summary>
public class NextLevel : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.Player) == true)
        {
            GameManager.Instance.NextLevel(0);
        }
    }
}
