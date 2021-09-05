using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : Character
{
    [Header("Player Objects")]

    [Header("Stats")]
    public int sp;
    public int wallet;

    [Header("Controls")]
    public KeyCode atkKey;
    public KeyCode jumpKey;
    public KeyCode blockKey;

    [Header("Physics")]
    public int walkSpeed;
    public int runSpeed;
    public float jumpForce;

    [Header("Checks")]
    public bool isStopped;
    public bool isRunning;
    public bool isGrounded;
    public bool isBlocking;


//UNITY FUNCTIONS
    //WHEN PLAYER IS FIRST CREATED
    private void Awake()
    {
        //init rigid body and animation variables with player scripts
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        //find ui script in the scene
        ui = FindObjectOfType<UI>();
    }

    //WHEN SCENE BEGINS
    private void Start()
    {
        //adjust ui elements
        ui.AdjustHP();
        ui.AdjustWallet();
    }

    //ALL PHYSICS UPDATES
    void Update()
    {
        //store user input into x
        x = Input.GetAxis("Horizontal");

        //flip sprite based on dir of input
        Flip(x);

        //if player is not stopped check for movement or jumping calls
        if (!isStopped)
        {
            Jump();
            Move();
        }

        //always check for punch and block calls
        Punch();
        //Block();
    }

    //runs physics
    private void FixedUpdate()
    {
    }

    //CHECKS IF PLAYER HAS LANDED ON GROUND
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal == Vector2.up)
        {
            isGrounded = true;
            an.SetBool("isJumping", false);
        }
    }



    //CUSTOM FUNCTIONS    
    //MOVE PLAYER ON COMMAND
    public void Move()
    {
        //move if x is being pressed
        if (x != 0)
        {
            //adjust speed and animation based on whether or not shift key is entered
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { Run(); }
            else { Walk(); }
        }

        //if shift key released, stop running behavior and animation
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)) { isRunning = false; }

        //start or stop run animation if applicable
        an.SetBool("isRunning", isRunning);

        //update dir of player output
        ChangeFace();

        //set speed in animator using absolute value of x
        an.SetFloat("speed", Mathf.Abs(x));
    }

    //UPDATE ANIMATION CONDITION AND MOVE AT WALK SPEED
    public void Walk()
    {
        isRunning = false;
        transform.Translate(x * walkSpeed * Time.deltaTime, 0f, 0f);
    }

    //UPDATE ANIMATION CONDITION AND MOVE AT RUN SPEED
    public void Run()
    {
        isRunning = true;
        transform.Translate(x * runSpeed * Time.deltaTime, 0f, 0f);
    }

    //JUMP ON COMMAND
    public void Jump()
    {
        //if jump key is pressed and is on the ground
        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            //begin jump animation and jump
            an.SetTrigger("jump");
            an.SetBool("isJumping", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            //cannot jump again until isGrounded
            isGrounded = false;
        }
    }

    //PUNCH
    public void Punch()
    {
        //if attack key is pressed and it's time to attack, punch
        if (Input.GetKeyDown(atkKey) && Time.time - lastAttack >= attackRate)
        {
            //stop moving
            isStopped = true;
            
            //attempt to punch an enemy
            RaycastHit2D punch = MeleeAttack(8);
            //if punch is successful, inflict damage
            if(punch.collider != null) 
            { 
                punch.collider.GetComponent<Enemy>().TakeDmg(damage); 
                lastAttack = Time.time; 
            }
            //otherwise, continue being able to move
            else { isStopped = false; }
            
        }
    }

    //TO BE CALLED AFTER PUNCH ANIMATION IS FINISHED
    public void StopPunch()
    {
        isStopped = false;
    }

   /* public void Block()
    {
        if (Input.GetKeyDown(blockKey))
        {
            an.SetBool("isBlocking", true);
            isStopped = true;
            isBlocking = true;
        }
        if (Input.GetKeyUp(blockKey) && isBlocking)
        {
            an.SetBool("isBlocking", false);
            isStopped = false;
            isBlocking = false;
        }
    }*/

    //INCREASE WALLET WHEN COLLIDING WITH COINS
    public void GetPaid(int value)
    {
        //increment wallet and adjust ui
        wallet += value;
        ui.AdjustWallet();
    }

    //WHEN COLLIDING WITH END FLAG
    public void WinLvl()
    {
        //change scene
        Debug.Log("you win!");
        ui.NextLevel();
    }
}
