using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the rendering of player sprites based on the player's movement state.
/// </summary>
public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    public Sprite idle;
    public Sprite jump;
    public Sprite climbingState;
    //public Sprite onLadder; an idle state on ladder - option

    public AnimatedSprite run;
    public AnimatedSprite climb;


    /// <summary>
    /// Initializes component references during the Awake phase.
    /// </summary>
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }


    /// <summary>
    /// Updates the player sprite based on the player's movement state during the LateUpdate phase.
    /// </summary>
    private void LateUpdate()
    {
        // Set running animation based on player's running state and not on a ladder.
        run.enabled = playerMovement.running && !playerMovement.onLadder;

        // Set jumping sprite when the player is jumping and not on a ladder.
        if (playerMovement.isJumping && !playerMovement.ladderFlag)
        {
            spriteRenderer.sprite = jump;
        }

        // Set climbing animation when the player is on a ladder and moving vertically.
        else if (playerMovement.onLadder && playerMovement.isMovingVertical)
        {
            run.enabled = false;
            climb.enabled = true;
            climbingState = climb.getCurrentSprite();
        }

        // Set idle Climbe sprite
        else if (playerMovement.onLadder) 
        {
            spriteRenderer.sprite = climbingState;
            //spriteRenderer.sprite = onLadder; an option
        }

        // Set idle "default" sprite
        else if (!playerMovement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }
}
