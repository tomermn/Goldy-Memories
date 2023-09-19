using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private new Camera camera;
    private Vector2 velocity;

    private new Rigidbody2D rigidbody;
    public float MoveSpeed = 1f;
    public float LadderClimbSpeed = 5f;
    public float JumpForce = 60f;

    public bool isJumping = false;
    public bool ladderFlag = false;
    public bool onLadder = false;

    public float moveHorizontal;
    public float moveVertical;

    public bool running => Mathf.Abs(moveHorizontal) > 0f;
    public bool isMovingVertical => Mathf.Abs(moveVertical) > 0f;



    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        camera = Camera.main;
    }

    private void Update()
    {

        moveHorizontal = Input.GetAxisRaw("Horizontal"); //left = a = -1, right = 'd' = 1, 0 = standing
        moveVertical = Input.GetAxisRaw("Vertical"); 

    }

    private void FixedUpdate()
    {
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
            rigidbody.velocity = new Vector2(moveHorizontal * LadderClimbSpeed, moveVertical * LadderClimbSpeed);
            if (moveVertical != 0f)
            {
                onLadder = true;
                
            }
        }


        //velocity.x = Mathf.MoveTowards(velocity.x, inputAxis* movement_speed, movement_speed * Time.deltaTime);
        velocity.x = Mathf.MoveTowards(velocity.x, moveHorizontal * MoveSpeed, MoveSpeed);
        
        if (velocity.x > 0)
        {
            transform.eulerAngles = Vector3.zero; // dont want any rotation
        }
        else if (velocity.x < 0f) //moving to the left
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }

        else if (collision.gameObject.tag == "Ladder")
        {
            ladderFlag = true;
            rigidbody.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true; ;
        }
        else if (collision.gameObject.CompareTag("Ladder"))
        {
            ladderFlag = false;
            onLadder = false;
            rigidbody.gravityScale = 8f; // Re-enable gravity when leaving the ladder
            
            
        }
    }


}
