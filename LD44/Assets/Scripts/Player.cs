using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [SerializeField] float acceleration = 25f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float jumpForce = 25f;
    [SerializeField] float jumpGravity = 3f;

    //bool canAttack = false;
    //bool canJump = false;
    //bool canClimb = false;

    bool isJumping = false;


    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if(!isJumping)
        {
            var currentVelocity = myRigidBody.velocity;
            var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * acceleration;
      
            myRigidBody.velocity = new Vector2(
                Mathf.Clamp(currentVelocity.x + deltaX, -maxSpeed, maxSpeed), 0);
            mySpriteRenderer.flipX = deltaX <= 0 ? false : true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
        }
        else
        {
            var velocityY = myRigidBody.velocity.y;
            Debug.Log(velocityY);
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
            isJumping = false;
            myRigidBody.gravityScale = 1;
        }
    }


}
