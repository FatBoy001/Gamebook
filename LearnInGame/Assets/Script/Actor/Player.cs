using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    public RaycastHit2D interactHit { get; set; }
    public GameObject numberBox;
    public int health;
    public bool alive { get; set; }
    public Animator animator;


    private float x;
    private float y;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        alive = true;
        health = 3;
       
    }

    private void FixedUpdate()
    {
        if (!alive) return;
        if (!GameManager.instance.boss.alive) return;
        if (health == 0) 
        {
            alive = false;
            return;
        }
        if (Time.time - lastImmune > immuneTime && animator.GetBool("getHit"))
        {
            animator.SetBool("getHit", false);
        }
        if (animator.GetBool("getHit")) 
        {
            move();
            return;
        }
        
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        runningAnimation(x,y);
        move(new Vector3(x, y, 0));
    }

    private void Update()
    {
        if (!alive) return;
        if (!GameManager.instance.boss.alive) return;

        if (Input.GetKeyDown(KeyCode.Space))
           interact();
        if (numberBox != null)
           numberBox.transform.position = transform.position + new Vector3(0,0.16f);            
    }

    private void interact()
    {
        if (interactable())
        {
            interactHit = interactLayer("Item");
            //控制數字方塊
            if(numberBox!=null)//put down
            {
                //放下
                numberBox.transform.position = transform.position + new Vector3(numberBox.GetComponent<NumberBox>().boxLength * transform.localScale.x, 0f);
                numberBox.GetComponent<NumberBox>().inHand = false;
                numberBox = null;
            }
            else if(numberBox==null && interactHit.collider!=null &&  interactHit.collider.tag=="NumberBox")
            {
                //取得
                numberBox = interactHit.collider.gameObject;
                numberBox.gameObject.transform.GetComponentInParent<NumberBox>().inHand = true;  
            }
        }
    }

    private bool interactable()
    {
        if (numberBox != null && interactLayer("Item").collider != null)
            return false;

        return interactLayer("Block").collider == null; 
    }

    private RaycastHit2D interactLayer(string layerName)
    {
        return Physics2D.BoxCast(transform.position, boxCollider2D.size, 0f, new Vector2(transform.localScale.x, 0), 0.11f, LayerMask.GetMask(layerName));
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
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            pushDirection = (transform.position - damage.origin).normalized * damage.pushForce;
            health = health - damage.health;
            GameManager.instance.UI.playerHeartUpdate();
            animator.SetBool("getHit", true);
            animator.SetBool("isRunning", false);
            
        }
    }

}
