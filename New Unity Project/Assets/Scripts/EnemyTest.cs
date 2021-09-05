
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : Character
{
    [Header("Enemy Objects")]

    [Header("Stats")]
    public int speed;

    [Header("Player")]
    public Player player;
    public float playerDistance;
    public float chaseRange;

    public Sprite hpBar;


//UNITY FUNCTIONS
    //WHEN ENEMY IS FIRST CREATED
    private void Awake()
    {
        //init animator, rb, and sprite renderer variables with scripts from enemy
        //an = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        //find player and ui in the scene
        player = FindObjectOfType<Player>();
        ui = FindObjectOfType<UI>();
    }

    //WHEN SCENE BEGINS
    private void Start()
    {
        //increments amount of enemies required to defeat in order to win the lvl
        ui.amountToDefeat += 1;
    }

    private void Update()
    {
        playerDistance = Vector2.Distance(transform.position, player.transform.position);
        //x is direction of player relative to enemy
        x = player.transform.position.x - transform.position.x;
        Flip(x);
    }



//CUSTOM FUNCTIONS
    //
    public void Attack()
    {

    }
}
