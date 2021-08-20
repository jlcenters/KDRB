using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Fighter
{
    public Player player;
    public float chaseRange;
    public float atkRange;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Chase();
    }

    public void Chase()
    {
        rb.velocity = (player.transform.position - transform.position).normalized * speed;

    }
}
