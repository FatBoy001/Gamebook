using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Boss : Mover
{
    private static float WALKTIME = 3.0f;

    private int MaxHealth = 100;
    private float walkTime = WALKTIME;
    private bool walking = false;
    private System.Random random = new System.Random();
    private int rngPositionIndex = 0;
    public GameObject Fireball;
    public Animator animator;
    public Transform[] stopPoint = new Transform[3];
    public bool alive { get; set; }
    
    public int maxHealth
    {
        get => MaxHealth;
        set => MaxHealth = value;
    }

    public int health { get; set; }

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        health = maxHealth;
        alive = true;
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.player.alive) return;
        if (health == 0)
        {
            alive = false;
            return;
        }
        /*----move for the enemy (start)*/
        //在冷卻開始時選擇等等要去往的方向
        if (Mathf.FloorToInt(walkTime)==3) 
            rngPositionIndex = random.Next(0, 3);
       
        if (Mathf.FloorToInt(walkTime) > 0)
        {
            //Debug.Log("Mathf.FloorToInt(walkTime)"+ Mathf.FloorToInt(walkTime));
            walkTime -= Time.fixedDeltaTime;
        }
        else
        {
            walking = true;
        }

        if (walking)
        {
            move((stopPoint[rngPositionIndex].position - transform.position).normalized);
            runningAnimation((stopPoint[rngPositionIndex].position - transform.position).normalized.x, (stopPoint[rngPositionIndex].position - transform.position).normalized.y);
            if (transform.position == stopPoint[rngPositionIndex].position)
            {
                walkTime = WALKTIME;
                walking = false;
            }
        }
        /*----move for the enemy (end)*/
    }
    
    private void runningAnimation(float x, float y)
    {
        if (x == 0 && y == 0)
            animator.SetBool("isRunning", false);
        else
            animator.SetBool("isRunning", true);
    }
    
    public void getDamage(Damage damage)
    {
       
        health = health - damage.health;

        animator.SetBool("isRunning", false);
        animator.SetTrigger("getHit");
    }
}