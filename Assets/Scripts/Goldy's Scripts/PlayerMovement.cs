using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Handles the movement and interactions of the player character.
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    private new Camera camera;
    private Vector2 velocity;                   //Player'a current velocity
    private new Rigidbody2D rigidbody;

    // Movement parameters
    public float MoveSpeed = 1f;                //Player's movement speed parameter
    public float LadderClimbSpeed = 5f;         //Player's climbing speed parameter
    public float JumpForce = 60f;               //Player's jump force parameter

    // Flags for player state
    public bool isJumping = false;
    public bool ladderFlag = false;             // Indicates if the player is on a ladder.
    public bool onLadder = false;               // Indicates if the player is actively climbing a ladder.
    public bool isTouchingSpikes = false;

    // Input values
    public float moveHorizontal;
    public float moveVertical;

    // Properties for additional state's information
    public bool running => Mathf.Abs(moveHorizontal) > 0f;
    public bool isMovingVertical => Mathf.Abs(moveVertical) > 0f;



    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        camera = Camera.main;
        transform.position = GameManager.Instance.GetCheckpoint();
    }

    /// <summary>
    /// Handles player input for horizontal and vertical movement.
    /// </summary>
    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal"); //left = a = -1, right = 'd' = 1, 0 = standing
        moveVertical = Input.GetAxisRaw("Vertical"); 

    }


    /// <summary>
    /// Handles physics-based movement, jumping, ladder climbing, and rotation.
    /// </summary
    private void FixedUpdate()
    {
        if (!isTouchingSpikes) {
            Vector2 position = rigidbody.position;
            position += velocity * Time.fixedDeltaTime;

            if (!ladderFlag)
            {
                // Handle regular movement
                if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
                {
                    rigidbody.AddForce(new Vector2(moveHorizontal * MoveSpeed, 0f), ForceMode2D.Impulse);
                }

                if (moveVertical > 0.1f && !isJumping)
                {
                    rigidbody.AddForce(new Vector2(0f, moveVertical * JumpForce), ForceMode2D.Impulse);
                    isJumping = true;

                }
            }

            else
            {
                // Handle ladder climbing
                rigidbody.velocity = new Vector2(moveHorizontal * LadderClimbSpeed, moveVertical * LadderClimbSpeed);
                if (moveVertical != 0f)
                {
                    onLadder = true;

                }
            }

            velocity.x = Mathf.MoveTowards(velocity.x, moveHorizontal * MoveSpeed, MoveSpeed);

            // Set player rotation based on movement direction
            if (velocity.x > 0)
            {
                transform.eulerAngles = Vector3.zero; // No rotation for right movement
            }
            else if (velocity.x < 0f) //Moving to the left
            {
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
        
    }


    /// <summary>
    /// Initiates the respawn process after a delay.
    /// </summary>
    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1f);
        transform.position = GameManager.Instance.GetCheckpoint();
    }



    /// <summary>
    /// Handles interactions when the player collides with certain objects.
    /// </summary>
    /// <param name="collision">The collider with which the player has collided.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTouchingSpikes)
        {

            switch (collision.gameObject.tag)
            {
                case Tags.Platform:
                    isJumping = false;
                    break;


                case Tags.Ladder:
                    ladderFlag = true;
                    rigidbody.gravityScale = 0f;
                    break;


                case Tags.CheckPoint:
                    GameManager.Instance.SetCheckpoint(collision.transform);
                    break;


                case Tags.Spikes:
                    HandleSpikesCollision(collision);
                    break;


                default: break;

            }
        }

        if (collision.tag == Tags.DeathBarrier)
        {
            StartCoroutine(Respawn());
        }
   
    }


    /// <summary>
    /// Initiating a sequence of death and response when the player's touch spikes traps
    /// </summary>
    /// <param name="collision"></param>
    private void HandleSpikesCollision(Collider2D collision)
    {
        isTouchingSpikes = true;
        rigidbody.velocity = new Vector2(0f, JumpForce); 
        StartCoroutine(FallThroughSpikes(collision));
    }


    private IEnumerator FallThroughSpikes(Collider2D collision)
    {
        yield return new WaitForSeconds(2f);
        isTouchingSpikes = false; 
    }


    /// <summary>
    /// Handles actions when the player exits collision with certain objects.
    /// </summary>
    /// <param name="collision">The collider from which the player has exited.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.gameObject.tag == Tags.Platform)
        {
            isJumping = true; 
        }
        else if (collision.gameObject.CompareTag(Tags.Ladder))
        {
            ladderFlag = false;
            onLadder = false;
            rigidbody.gravityScale = 8f; // Re-enable gravity when leaving the ladder      
        }
    }





}
