using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    [Header("Enemy Stats")]
    public Player player;
    public float chaseRange;
    public float playerDist;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerDist = Vector2.Distance(transform.position, player.transform.position);
        //x is direction of player relative to enemy
        x = player.transform.position.x - transform.position.x;
        Flip(x);

        if(playerDist <= atkRange)
        {
            if (Time.time -lastAtk >= atkRate)
            {
                MeleeAttack(7, dmg);
                lastAtk = Time.time;
            }
            else
            {
                StopPunching();
            }
            StopMoving();
        }
        else if(playerDist <= chaseRange)
        {
            StopPunching();
            anim.SetFloat("speed", Mathf.Abs(x));
            Chase();
            ChangeFace();
        }
        else
        {
            StopPunching();
            StopMoving();
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

    public void StopPunching()
    {
        anim.SetBool("isPunching", false);
    }
}
