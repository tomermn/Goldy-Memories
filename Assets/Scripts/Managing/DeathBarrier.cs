using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            other.gameObject.SetActive(false);
            GameManager.Instance.Respawn(2f);
            //other.gameObject.SetActive(true);
            //GameManager.Instance.ResetLevel(3f);
        }
        //else
        //{
        //    Destroy(other.gameObject);
        //}
    }
}
