
using UnityEngine;

/// <summary>
/// Controls the side-scrolling behavior of the camera to follow the player horizontally and vertically.
/// </summary>
public class SiideScrolling : MonoBehaviour
{
    private Transform player;
    private void Awake()
    {
        player = GameObject.FindWithTag(Constants.PlayerTag).transform;
    }

    /// <summary>
    /// Called every frame after all Update functions have been called.
    /// </summary>
    private void LateUpdate() // gonna call last, right before everything render to the screen.
    {
        Vector3 cameraPosition = transform.position;

        // Follow the player horizontally if the player's x position is greater than or equal to 0.
        if (player.position.x >= 0)
        {
            cameraPosition.x = player.position.x;
            
        }

        /*OPTIONAL - prevent the player to move to the left.
            else
            {
                cameraPosition.x = cameraPosition.x;
            }
            cameraPosition.x = player.position.x;
            cameraPosition.x = Mathf.Max(player.position.x, cameraPosition.x); // there is an option to 
         */



        // Follow the player vertically if the player's y position is greater than or equal to 0.
        if (player.position.y >= 0)
        {
            cameraPosition.y = player.position.y;
            
        }

        transform.position = cameraPosition;

    }
}
