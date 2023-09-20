
using UnityEngine;

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


    private void Update()
    {
        this.enabled = playerMovement.onLadder;
    }
}
