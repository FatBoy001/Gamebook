using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberBoxCustom : NumberBox
{
    public GameObject[] childObject = new GameObject[2];
    public Sprite[] numberSprite = new Sprite[10];
    public Sprite[] operatorSprite = new Sprite[3];


    public override void Start()
    {
        inHand = false;
        placeIn = true;
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        childObject[0] = gameObject.transform.GetChild(0).gameObject;
        childObject[1] = gameObject.transform.GetChild(1).gameObject;
        if (boxType == "operator")
        {
            childObject[1].GetComponent<SpriteRenderer>().enabled = false;
            if (boxValue == -5)
            {
                childObject[0].GetComponent<SpriteRenderer>().sprite = operatorSprite[2];
            }

            if (boxValue % 2 == 0)
            {
                
                if (boxValue == -2)
                {
                    childObject[0].GetComponent<SpriteRenderer>().sprite = operatorSprite[0];
                }
                else if (boxValue == -4)
                {
                    childObject[0].GetComponent<SpriteRenderer>().sprite = operatorSprite[1];
                }
            }

            childObject[0].transform.localPosition = new Vector3(0, 0, 0);
            childObject[0].transform.localScale = new Vector3(0.8f, 0.8f, 0);
            childObject[1].GetComponent<SpriteRenderer>().sprite = numberSprite[boxValue / 10];
        }
        else if(boxType == "number")
        {
            if (boxValue >= 10)
            {
                //位置調整
                childObject[1].GetComponent<SpriteRenderer>().enabled = true;
                childObject[0].transform.localPosition = new Vector3(0.025f, 0, 0);
                childObject[1].transform.localPosition = new Vector3(-0.025f, 0, 0);
                childObject[0].transform.localScale = new Vector3(0.5f, 0.5f, 0);
                childObject[1].transform.localScale = new Vector3(0.5f, 0.5f, 0);

                childObject[0].GetComponent<SpriteRenderer>().sprite = numberSprite[boxValue % 10];
                childObject[1].GetComponent<SpriteRenderer>().sprite = numberSprite[boxValue / 10];
            }
            else if (0 <= Mathf.Abs(boxValue) && Mathf.Abs(boxValue) < 10)
            {
                childObject[0].GetComponent<SpriteRenderer>().sprite = numberSprite[Mathf.Abs(boxValue)];
                childObject[0].transform.localPosition = new Vector3(0, 0, 0);
                childObject[0].transform.localScale = new Vector3(0.8f, 0.8f, 0);
                childObject[1].GetComponent<SpriteRenderer>().enabled = false;
            }
        }
       
    }

    public override void Update()
    {
        //base.Update();
    }

    public override void OnCollide(Collider2D coll)
    {
        base.Update();
    }
    
}