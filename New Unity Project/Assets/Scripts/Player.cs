using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Fighter
{
    [Header("Player Stats")]
    public int sp;
    public KeyCode atkKey;
    public KeyCode jumpKey;
    public KeyCode blockKey;
    public float jumpForce;
    public bool isStopped;
    public int wallet;
    public int walkSpeed;
    public int runSpeed;
    public bool isRunning;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ui = FindObjectOfType<UI>();
    }

    private void Start()
    {
        ui.AdjustHP();
    }

    //handles input for physics
    void Update()
    {
        //store user input into x
        x = Input.GetAxis("Horizontal");
        if (!isStopped)
        {
            Jump();
            Move();
        }
        Punch();
        Block();
    }

    //runs physics
    private void FixedUpdate()
    {
        Flip(x);
    }

    public void Move()
    {
        if(x != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isRunning = true;
                anim.SetBool("isRunning", true);
                speed = runSpeed;
            }
            else
            {
                isRunning = false;
                speed = walkSpeed;
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isRunning = false;
            anim.SetBool("isRunning", false);
        }

        //move horizontally based on speed and real time
        transform.Translate(x * speed * Time.deltaTime, 0f, 0f);
        ChangeFace();

        //set speed in animator using absolute value of x
        anim.SetFloat("speed", Mathf.Abs(x));
    }

    public void Run()
    {

        transform.Translate(x * runSpeed * Time.deltaTime, 0f, 0f);
    }

    public void Jump()
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

    public void Punch()
    {
        if (Input.GetKeyDown(atkKey) && Time.time - lastAtk >= atkRate)
        {
            isStopped = true;
            MeleeAttack(8, dmg);
        }
        if (Input.GetKeyUp(atkKey))
        {
            anim.SetBool("isMelee", false);
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

    public void GetPaid(int value)
    {
        wallet += value;
    }


}
