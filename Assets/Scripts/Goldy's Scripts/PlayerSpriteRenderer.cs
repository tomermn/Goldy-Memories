using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    public Sprite idle;
    public Sprite jump;
    public AnimatedSprite run;

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
        run.enabled = playerMovement.running;
        if (playerMovement.isJumping)
        {
           spriteRenderer.sprite = jump;
        }
        else if (!playerMovement.running)
        {
            spriteRenderer.sprite = idle;
        }

    }
}
