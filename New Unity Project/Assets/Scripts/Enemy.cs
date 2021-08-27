using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    [Header("Enemy Stats")]
    public Player player;
    public float chaseRange;
    public float playerDist;
    public bool isTan;
    public int atkCount;
    public int numToHeavy;
    public int heavyDmg;
    public bool isRed;
    public Sprite hpBar;
    public int blueDie;
    public int blueWin;
    public int tanDie;
    public int tanWin;
    public bool firstAtk;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        ui = FindObjectOfType<UI>();
        firstAtk = true;
    }

    private void Start()
    {
        ui.amountToDefeat += 1;
    }

    private void Update()
    {
        
        playerDist = Vector2.Distance(transform.position, player.transform.position);
        //x is direction of player relative to enemy
        x = player.transform.position.x - transform.position.x;
        Flip(x);

        if(playerDist <= atkRange)
        {
            /*if (firstAtk)
            {
                lastAtk = Time.time;
                firstAtk = false;
            }*/


            if (Time.time - lastAtk >= atkRate)
            {
                int die;

                if (!isTan)
                {

                        //roll and debug
                        die = Roll(blueDie);
                        Debug.Log(die);
                        //if the roll is a success, debug and atk
                        if (die >= blueWin)
                        {
                            Debug.Log("attack!");
                            MeleeAttack(7, dmg);
                        }
                        else
                        {
                            Debug.Log("Miss!");
                            Miss();
                        }
                }
                if (isTan){
                   //roll and debug
                    die = Roll(tanDie);
                    Debug.Log(die);
                    //if roll is a success, debug and atk
                    if (die >= tanWin)
                    {
                        Debug.Log("attack!");
                        IncrementAtk();
                        if (atkCount < numToHeavy)
                        {
                            MeleeAttack(7, dmg);
                        }
                        else
                        {
                            anim.SetBool("isMelee", true);
                        }
                    }
                    else
                    {
                        Debug.Log("Miss!");
                        Miss();
                    }
                }
            }
            StopMoving();
        }
        else if(playerDist <= chaseRange)
        {
            StopMelee();
            anim.SetFloat("speed", Mathf.Abs(x));
            Chase();
            ChangeFace();
        }
        else
        {
            StopMelee();
            StopMoving();
            //RangedAttack();
        }
    }

    public void Chase()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * speed;
    }

    public void StopMoving()
    {
        anim.SetFloat("speed", 0);
        rb.velocity = Vector2.zero;
    }

    public void IncrementAtk()
    {
        atkCount++;
        anim.SetInteger("atkCount", atkCount);
    }

    public void HeavyAttack()
    {
        MeleeAttack(7, heavyDmg);
        atkCount = 0;
        anim.SetBool("isMelee", false);
    }

    public int Roll(int sides)
    {
        return Random.Range(1,sides + 1);
    }
}
