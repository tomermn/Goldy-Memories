
using UnityEngine;

/// <summary>
/// Extends the functionality of AnimatedSprite to handle sprite animation specifically during climbing.
/// </summary>
public class ClimbingSprites : AnimatedSprite
{
    private PlayerMovement playerMovement;
    private const float ClimbingFrameRate = (1f / 9f);

    protected override void Awake()
    {
        framerate = ClimbingFrameRate;
        base.Awake();
        playerMovement = GetComponent<PlayerMovement>();
        enabled = false;
    }

    /// <summary>
    /// Monitors the player's climbing state and enables/disables the component accordingly.
    /// </summary>
    private void Update()
    {
        enabled = playerMovement.OnLadder;
    }
}
