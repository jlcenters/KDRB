using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    [Header("Character Stats")]
    public int hp;
    public int speed;
    public int dmg;
    public Rigidbody2D rb;
    public Animator anim;
    public bool isGrounded;
    public bool isBlocking;
    public float x;
    public Vector2 face;
    public float atkRange;
    public float atkRate;
    public float lastAtk;
    //public GameObject projectile;
    //public Transform shotPoint;

    [Header("If scale is updated, make changes here")]
    public float scaleUp;

    public void MeleeAttack(int targetLayer, int dmg)
    {
        anim.SetBool("isMelee", true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, face, atkRange, 1 << targetLayer);
        if (hit.collider != null)
        {
            if(targetLayer == 7)
            {
                hit.collider.GetComponent<Player>().TakeDmg(dmg);
            }
            else if(targetLayer == 8)
            {
                hit.collider.GetComponent<Enemy>().TakeDmg(dmg);
            }

        } 
    }

    public void StopMelee()
    {
        anim.SetBool("isMelee", false);
    }

    /*public void RangedAttack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, shotPoint.position, shotPoint.rotation);
        }
    }*/

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
    }

    public void Flip(float x)
    {
        Vector3 scale = transform.localScale;

        //inverts x axis based on scale of sprite
        if (x < 0)
        {
            scale.x = -scaleUp;
        }
        else if (x > 0)
        {
            scale.x = scaleUp;
        }

        transform.localScale = scale;
    }

    public void ChangeFace()
    {
        //if character's velocity is changing, in other words moving,
        //update face in direction of movement
        Vector2 vel = new Vector2(x, rb.velocity.y);
        if (vel.magnitude != 0)
        {
            face = vel;
        }
    }
}
