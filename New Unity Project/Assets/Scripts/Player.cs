using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    [Header("Player Stats")]
    public int sp;
    public KeyCode atkKey;
    public KeyCode jumpKey;
    public KeyCode blockKey;
    public float jumpForce;
    public bool isStopped;

    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    //handles input for physics
    void Update()
    {
        //store user input into x
        x = Input.GetAxis("Horizontal");

        Jump();
        Punch();
        Block();
    }

    //runs physics
    private void FixedUpdate()
    {
        Flip(x);
        Walk();
    }

    public void Walk()
    {
        if (!isStopped)
        {
            //move horizontally based on speed and real time
            transform.Translate(x * speed * Time.deltaTime, 0f, 0f);

            ChangeFace();

            //set speed in animator using absolute value of x
            anim.SetFloat("speed", Mathf.Abs(x));
        }
    }

    public void Jump()
    {
        if (!isStopped)
        {
            //isGrounded = Physics2D.OverlapCircle(point.position, box, ground);
            if (Input.GetKeyDown(jumpKey) && isGrounded)
            {
                anim.SetTrigger("jump");
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
                isGrounded = false;
            }
        }
    }

    public void Punch()
    {
        if (Input.GetKeyDown(atkKey))
        {
            isStopped = true;
            MeleeAttack(8, dmg);
        }
        if (Input.GetKeyUp(atkKey))
        {
            anim.SetBool("isPunching", false);
            isStopped = false;
        }

    }

    public void Block()
    {
        if (Input.GetKeyDown(blockKey))
        {
            anim.SetBool("isBlocking", true);
            isStopped = true;
            isBlocking = true;
        }
        if (Input.GetKeyUp(blockKey) && isBlocking)
        {
            anim.SetBool("isBlocking", false);
            isStopped = false;
            isBlocking = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal == Vector2.up)
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }

}
