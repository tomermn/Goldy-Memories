
using UnityEngine;

/// <summary>
/// Extends the functionality of AnimatedSprite to handle sprite animation specifically during climbing.
/// </summary>
public class ClimbingSprites : AnimatedSprite
{
    private PlayerMovement playerMovement;


    protected override void Awake()
    {
        framerate = (1f / 9f);
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
        this.enabled = false;
    }

    /// <summary>
    /// Monitors the player's climbing state and enables/disables the component accordingly.
    /// </summary>
    private void Update()
    {
        this.enabled = playerMovement.onLadder;
    }
}
