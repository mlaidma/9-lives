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
    [SerializeField] float walkVolume = 0.8f;
    [SerializeField] float smackVolume = 1f;
    [SerializeField] AudioClip smackClip;


    [SerializeField] GameObject deadCat;
    [SerializeField] GameObject spawnCatAt;

    [SerializeField] SceneLoader sceneLoader;
    

    bool canSmack = false;
    bool canClimb = false;

    bool inAir = false;
    bool isWalking = false;

    SpriteRenderer mySpriteRenderer;
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    AudioSource myAudio;


    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = walkVolume;
        livesRemainingText.text = livesRemaining.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Smack();
    }

    private void Die()
    {
        Debug.Log("Player dead");
        if(sceneLoader != null)
        {
            sceneLoader.PlayerDied();
        }


    }

    private void Smack()
    {
        if(Input.GetButtonDown("Fire1") && canSmack)
        {
            myAnimator.SetTrigger("smack");
            myAudio.PlayOneShot(smackClip, smackVolume);
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
                SetPlayerMoving(true);
                transform.localScale= new Vector2(-1, transform.localScale.y);
            }
            else if(deltaX > 0f)
            {
                SetPlayerMoving(true);
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else
            {
                SetPlayerMoving(false);
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

            myAudio.Pause();
        }
    }

    private void SetPlayerMoving(bool state)
    {
        isWalking = state;
        myAnimator.SetBool("isWalking", state);

        if(state)
        {
            myAudio.UnPause();
        }
        else
        {
            myAudio.Pause();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor")
        {
            inAir = false;
            myRigidBody.gravityScale = 1;
        }

        if(collision.collider.tag == "PoolDeath")
        {
            Debug.Log("Pool Death");
            var curPos = transform.position;
            var newPos = new Vector3(curPos.x, curPos.y - 0.3f, curPos.z);
            Instantiate(deadCat, newPos, Quaternion.identity);
            LoseLives(1);

            myRigidBody.velocity = new Vector3(0f, 0f, 0f);
            if(spawnCatAt != null)
            {
                transform.position = spawnCatAt.transform.position;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "Floor")
        {
            inAir = true;
        }
    }

    public void LoseLives(int lives)
    {
        livesRemaining -= lives;
        livesRemainingText.text = livesRemaining.ToString();

        if(livesRemaining <= 0)
        {
            Die();
        }
    }
    
    public void EnableSmacking()
    {
        canSmack = true;
    }

    public void EnableClimbing()
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
