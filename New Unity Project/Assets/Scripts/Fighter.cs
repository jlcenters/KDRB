using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hp;
    public int speed;
    public int atk;
    public Rigidbody2D rig;
    public Animator anim;

    public void Attack(int dmg)
    {

    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
    }

   /* public void Walk(float xAxis)
    {
        anim.SetFloat("speed", Mathf.Abs(xAxis != 0 ? xAxis : 0f));
    }*/
}
