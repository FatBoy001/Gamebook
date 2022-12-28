using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBox : Collidable
{
    public SpriteRenderer spriteRenderer;
    public string boxType;
    //{"operator","number"}
    public int boxValue;
    public bool placeIn;
    public bool inHand;
    public float boxLength;

    public override void Start()
    {    
        inHand = false;
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        
    }

    public override void Update()
    {
        base.Update();

        if (inHand)
            boxCollider.enabled = false;
        else
            boxCollider.enabled = true;

        if (inHand == true)
            placeIn=false;

    }

    public override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Place_Number" && boxType == "number")
        {
            placeIn = true;
            return;
        } 
        else if (coll.tag == "Place_Operator_Start" && boxType == "operator" && (boxValue%2==0) )
        {
            placeIn = true;
            return;
        }
        else if (coll.tag == "Place_Operator_End" && boxType == "operator" && (boxValue % 2 != 0) )
        {
            placeIn = true;
            return;
        }
    }
    
}
