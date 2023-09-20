using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    public Sprite idle;
    public Sprite jump;
    //public Sprite onLadder; an idle state on ladder - option
    public Sprite climbingState;

    public AnimatedSprite run;
    public AnimatedSprite climb;

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

    private void LateUpdate()
    {
        run.enabled = playerMovement.running && !playerMovement.onLadder;

        if (playerMovement.isJumping && !playerMovement.ladderFlag)
        {
            
            spriteRenderer.sprite = jump;
        }

        else if (playerMovement.onLadder && playerMovement.isMovingVertical)
        {
            
            run.enabled = false;
            climb.enabled = true;
            climbingState = climb.getCurrentSprite();
        }

        else if (playerMovement.onLadder) 
        {
            spriteRenderer.sprite = climbingState;
            //spriteRenderer.sprite = onLadder; //an option
        }

        else if (!playerMovement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }
}
