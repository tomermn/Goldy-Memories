using System;
using UnityEngine;


public class SiideScrolling : MonoBehaviour
{
    private Transform player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate() // gonna call last, right before everything render to the screen.
    {


        Vector3 cameraPosition = transform.position;

        if (player.position.x < 0)
        {
            cameraPosition.x = cameraPosition.x;
        }

     
        else
        {
            cameraPosition.x = player.position.x;
        }
        //cameraPosition.x = player.position.x;
        //cameraPosition.x = Mathf.Max(player.position.x, cameraPosition.x); // there is an option to prevent the player to move to the left.


        if (player.position.y < 0)
        {
            cameraPosition.y = cameraPosition.y;
        }

        else
        {
            cameraPosition.y = player.position.y;
            
        }
        transform.position = cameraPosition;

    }
}
