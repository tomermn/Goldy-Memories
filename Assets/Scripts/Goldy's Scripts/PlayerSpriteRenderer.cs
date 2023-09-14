using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    public Sprite idle;
    public Sprite jump;
    //public Sprite onLadder; need a sprite from Nika

    public AnimatedSprite run;
    public ClimbingSprites climb;

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
            Debug.Log("isJumping!");
            spriteRenderer.sprite = jump;
        }

        else if (playerMovement.onLadder && playerMovement.isMovingVertical)
        {
            
            run.enabled = false;
            climb.enabled = true;
        }

        //else if (playerMovement.onLadder) need this sprite form Nika
        //{
        //    spriteRenderer.sprite = onLadder;
        //}

        else if (!playerMovement.running)
        {
            spriteRenderer.sprite = idle;
        }
    }
}
