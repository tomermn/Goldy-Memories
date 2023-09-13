using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingSprites : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = (1 / 6f);

    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
        this.enabled = false;
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        frame++;
        Debug.Log("climbing frame number: " + frame);

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        spriteRenderer.sprite = sprites[frame];
    }

    private void Update()
    {
        this.enabled = playerMovement.onLadder;
    }
}
