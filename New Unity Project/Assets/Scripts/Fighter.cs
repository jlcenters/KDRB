using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hp;
    public int speed;
    public int atk;
    public Rigidbody2D rig;

    public void Attack(int dmg)
    {

    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
    }
}
