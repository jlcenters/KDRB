using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hp;
    public int speed;
    public int atk;
    public Rigidbody2D rb;
    public Animator anim;
    public bool isGrounded;
    public bool isBlocking;

    public void Attack(int dmg)
    {

    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
    }

    /* public bool IsGrounded()
     {
         RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, -0.1f, 0), Vector2.down, 0.2f);

         return hit.collider != null;
     }

     public void Walk(float xAxis)
     {
         anim.SetFloat("speed", Mathf.Abs(xAxis != 0 ? xAxis : 0f));
     }*/
}
