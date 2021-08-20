using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{
    public int sp;
    public KeyCode atkKey;
    public KeyCode jumpKey;
    public KeyCode blockKey;
    private Vector2 face;
    public float atkRange;
    public float x;
    public float jumpForce;
    public bool isStopped;

    [Header("If scale is updated, make changes here")]
    public float scaleUp;

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
        Attack();
        Block();
    }

    //runs physics
    private void FixedUpdate()
    {
        Flip(x);
        Walk();
    }

    private void Flip(float x)
    {
        Vector3 scale = transform.localScale;

        //inverts x axis based on scale of sprite
        if(x < 0)
        {
            scale.x = -scaleUp;
        }
        else if(x > 0)
        {
            scale.x = scaleUp;
        }

        transform.localScale = scale;
    }

    public void Walk()
    {
        if (!isStopped)
        {
            //move horizontally based on speed and real time
            transform.Translate(x * speed * Time.deltaTime, 0f, 0f);

            //if player's velocity is changing, in other words moving,
            //update face in direction of movement
            Vector2 vel = new Vector2(x, rb.velocity.y);
            face = vel;

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

    public void Attack()
    {
        // WHEN ATTACKING FREEZE POSITION
        if (Input.GetKeyDown(atkKey))
        {
            isStopped = true;
            anim.SetBool("isPunching", true);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, face, atkRange, 1 << 8);
            if (hit.collider != null)
            {
                hit.collider.GetComponent<Enemy>().TakeDmg(atk);
                Debug.Log("enemy: Ouch!");
            }
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
