using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Fighter
{

    public int sp;
    public KeyCode atkKey;
    private Vector2 face;
    public float atkRange;

    /*TODO:
     * hit a key to attack (idk which yet)
     * hit a key to jump
     * hit a key for sp atk
    */

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        if (Input.GetKeyDown(atkKey))
        {
            Attack();
        }
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal");

        Vector2 vel = new Vector2(x, rig.velocity.y);

        //if player's velocity is changing, in other words moving,
        //update face in direction of movement
            face = vel;

        rig.velocity = new Vector2(x * speed, rig.velocity.y);

    }

    public void Jump()
    {

    }

    public void Attack()
    {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, face, atkRange, 1 << 8);

            if(hit.collider != null)
            {
                hit.collider.GetComponent<Enemy>().TakeDmg(atk);
                Debug.Log("enemy: Ouch!");
            }
    }

}
