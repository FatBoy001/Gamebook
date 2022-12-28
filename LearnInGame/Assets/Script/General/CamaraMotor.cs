using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMotor : MonoBehaviour
{
    //可以使用[HideInInspector]
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;

    private void Start()
    {
        //若是沒有加.transform他只會回傳gameobject
        lookAt = GameObject.Find("Player").transform;
    }

    //LateUpdate 被稱作 afterUpdate 或者 afterFixedUpdate 因為當玩家移動時 我們必須確保攝影機移動時是玩家已經先按移動
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        //判斷是否在範圍中 以x範圍為例 deltaX的值為 玩家x位置與攝影機x位置的距離 若超過0.15那麼就要移動
        //x範圍
        float deltaX = lookAt.position.x - transform.position.x;

        if (Mathf.Abs(deltaX) > boundX)
        {
            //判斷攝影機在玩家的左邊還是右邊
            if (transform.position.x < lookAt.position.x)
            {
                //若鏡頭在右玩家在左需要向右邊移動else則反之
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }
        //y範圍
        float deltaY = lookAt.position.y - transform.position.y;
        if (Mathf.Abs(deltaY) > boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
