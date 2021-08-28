using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public UI ui;
    public Player player;
    public CapsuleCollider2D collider;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        ui = FindObjectOfType<UI>();
        collider = GetComponent<CapsuleCollider2D>();
    }


    // Update is called once per frame
    void Update()
    {
       if(ui.amountToDefeat == ui.amountDefeated)
        {
            collider.isTrigger = true;
        }
        else
        {
            collider.isTrigger = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.WinLvl();
    }
}
