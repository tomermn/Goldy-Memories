using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    //we will use this functoin only if we want to kill the player. in the current situation, we want that the player will respawn, and the playerMovement script is handle it.
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
        //{
            
    //        other.gameObject.SetActive(false);
     //       GameManager.Instance.ResetLevel(3f);
    //    }
        //else
        //{
        //    Destroy(other.gameObject);
        //}
    //}
}
