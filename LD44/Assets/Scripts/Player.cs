using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{   
    [SerializeField] float acceleration = 15f;
    [SerializeField] float maxSpeed = 10f;
    [SerializeField] float jumpForce = 8f;
    [SerializeField] float jumpGravity = 3f;

    [SerializeField] TextMeshProUGUI livesRemainingText;
    [SerializeField] int livesRemaining = 9;

    bool canSmack = false;
    bool canClimb = false;

    bool inAir = false;

    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidBody;
    Animator myAnimator;


    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        livesRemainingText.text = livesRemaining.ToString();

        //debug
        canSmack = true;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Smack();
    }

    private void Smack()
    {
        if(Input.GetButtonDown("Fire1") && canSmack)
        {
            myAnimator.SetTrigger("smack");
        }
    }

    private void Move()
    {
        if(!inAir)
        {
            var currentVelocity = myRigidBody.velocity;
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * acceleration;

            if(deltaX < 0f)
            {
                myAnimator.SetBool("isWalking", true);
                transform.localScale= new Vector2(-1, transform.localScale.y);
            }
            else if(deltaX > 0f)
            {
                myAnimator.SetBool("isWalking", true);
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else
            {
                myAnimator.SetBool("isWalking", false);
            }

            myRigidBody.velocity = new Vector2(
                Mathf.Clamp(currentVelocity.x + deltaX, -maxSpeed, maxSpeed), 0);


            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                inAir = true;
            }
        }
        else
        {
            var velocityY = myRigidBody.velocity.y;
            if (velocityY < 2)
            {
                myRigidBody.gravityScale = jumpGravity;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor")
        {
            inAir = false;
            myRigidBody.gravityScale = 1;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor")
        {
            inAir = true;
        }
    }
    
    public void AllowSmacking()
    {
        canSmack = true;
    }

    public void AllowClimbing()
    {
        canClimb = true;
    }

    public void IncreaseJumpForce(float boost)
    {
        jumpForce += boost;
    }

    public void IncreaseSpeed(float boost)
    {
        maxSpeed += boost;
    }
}
