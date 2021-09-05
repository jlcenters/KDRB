using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour 
{
    [Header("Character Objects")]

    [Header("Stats")]
    public int currentHP;
    public int maxHP;
    public int damage;

    [Header("Attacking")]
    public float attackRange;
    public float attackRate;
    public float lastAttack;

    [Header("Sprite")]
    public SpriteRenderer sprite;
    public Vector2 face;
    public float x;
    public float scaleUp;
    public Animator an;

    public Rigidbody2D rb;

    public UI ui;



    //FLIPS SPRITE BASED ON DIRECTION
    public void Flip(float x)
    {
        //grabs scale of object
        Vector3 scale = transform.localScale;

        //inverts x axis using scale of sprite
        if (x < 0) { scale.x = -scaleUp; }
        else if (x > 0) { scale.x = scaleUp; }

        //applies new scale to object
        transform.localScale = scale;
    }

    //CHANGE SIDE THAT WILL OUTPUT ATTACKS
    public void ChangeFace()
    {
        //grabs velocity of object
        Vector2 vel = new Vector2(x, rb.velocity.y);

        //if character is moving, update face in that direction
        if (vel.magnitude != 0) { face = vel; }
    }

    //START MELEE ATTACK
    public RaycastHit2D MeleeAttack(int targetLayer)
    {
        //begin melee animation
        an.SetBool("isMelee", true);
        
        //output melee attack attempt
        return Physics2D.Raycast(transform.position, face, attackRange, 1 << targetLayer);
    }

    //STOP MELEE ATTACK
    public void StopMelee()
    {
        //stop melee animation
        an.SetBool("isMelee", false);
    }

    //RECEIVE DAMAGE UPON HIT
    public void TakeDmg(int dmg)
    {
        //take damage
        currentHP -= dmg;

        //update ui
        if (ui != null) { ui.AdjustHP(); }
    }
    
}
