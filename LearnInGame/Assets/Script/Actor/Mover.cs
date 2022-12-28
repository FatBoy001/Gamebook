using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : Fighter
{

    public RaycastHit2D hit { get; set; }
    public BoxCollider2D boxCollider2D { get; set; }
    public Vector3 pushDirection { get; set; }

    protected float immuneTime = 1.0f;
    public float pushRecoverySpeed = 0.2f;
    protected float lastImmune;

    public virtual void Start()
    {
        //取得BoxCollider2D的數據
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    public virtual void move(Vector3 input) 
    {
        Vector3 moveDelta = new Vector3(input.x * Time.deltaTime, input.y * Time.deltaTime);

        //決定Sprite面向何方
        if (moveDelta.x > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        //有沒有撞到
        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position,boxCollider2D.size,0,new Vector3(0, moveDelta.y,0), Mathf.Abs(moveDelta.y), LayerMask.GetMask("Actor", "Block","Item"));
        if (hit.collider ==null)
            transform.Translate(0, moveDelta.y, 0);

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector3(moveDelta.x, 0, 0), Mathf.Abs(moveDelta.x ), LayerMask.GetMask("Actor", "Block", "Item"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x , 0, 0);
    }

    public virtual void move()
    {
        Vector3 moveDelta = new Vector3(0, 0);

        //有沒有撞到
        moveDelta += pushDirection;
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector3(0, moveDelta.y, 0), Mathf.Abs(moveDelta.y), LayerMask.GetMask("Actor", "Block", "Item"));
        if (hit.collider == null)
            transform.Translate(0, moveDelta.y, 0);

        hit = Physics2D.BoxCast(transform.position, boxCollider2D.size, 0, new Vector3(moveDelta.x, 0, 0), Mathf.Abs(moveDelta.x), LayerMask.GetMask("Actor", "Block", "Item"));
        if (hit.collider == null)
            transform.Translate(moveDelta.x, 0, 0);
    }
}
