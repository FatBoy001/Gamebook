using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceItem : Collidable
{
    private SpriteRenderer spriteRenderer;
    public bool itemSet { get; set; }
    public NumberBox numberBox;

    public override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        itemSet = false;
    }

    public override void Update()
    {
        // Collision work
        if (itemSet==false)
        {
            base.Update();
        }
        else
        {
            //偵測是否有箱子
            boxCollider.OverlapCollider(filter, hits);
            for (int i = 0; i < hits.Length; i++)
            {
                if (collideOnNumberBox(hits[i]))
                {
                    hits[i] = null;
                    break;
                }
                itemSet = false;
                numberBox = null;
            }
        }
        

        if (itemSet == false)
            spriteRenderer.color = new Color32(255, 232, 0, 170);
            

    }
    
    public override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player"|| !collideOnNumberBox(coll)) return;
        
       
        numberBox = coll.GetComponent<NumberBox>();
        if (numberBox.placeIn)
        {
            numberBox.transform.position = transform.position;
            spriteRenderer.color = new Color32(0, 0, 0, 255);
            itemSet = true;
        }
    }

    private bool placeInNumberBox(string type, Collider2D coll)
    {
        if (typeOfNumberBox(type, coll))
        {
            if (coll.GetComponent<NumberBox>().inHand == false && coll.GetComponent<NumberBox>().placeIn)
            {
                //箱子現在在區域中且是正確的
                return true;
            }
        }
        
        return false;
    }

    private bool typeOfNumberBox(string type, Collider2D coll)
    {
        if (collideOnNumberBox(coll))
        {
            if (coll.GetComponent<NumberBox>().boxType == type)
                return true;    
        }

        return false;
    }

    private bool collideOnNumberBox(Collider2D coll)
    {
        if (coll == null)
            return false;

        if (coll.tag == "NumberBox")
            return true;

        return false;
    }


}
