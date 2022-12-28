using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Linq;
using System;

public class BoxSpawner : MonoBehaviour
{
    public GameObject[] spawnpoint;
    public Sprite[] spriteArray;
    public GameObject numberBox;
    private int[] ramdomIndex = {0,1,2,3,4,5,6,7,8,9};
    private GameObject[] spawnObject = new GameObject[10];
    /*
    -1 = >
    -2 = +
    -3 = <
    -4 = -
     */

    public void spawnBox()
    {
        randomBoxValue(ref ramdomIndex);
        for (int i = 0; i < ramdomIndex.Length; i++)
        {
            numberBox.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = spriteArray[ramdomIndex[i]];
            numberBox.transform.GetComponent<NumberBox>().boxValue = ramdomIndex[i];
            numberBox.transform.GetComponent<NumberBox>().boxType = "number";

            spawnObject[i] = Instantiate(numberBox, spawnpoint[i].transform.position, spawnpoint[i].transform.rotation);
        }
    }

    public void destroyBox()
    {
        for (int i=0; i< spawnObject.Length;i++)
        {
            Destroy(spawnObject[i]);
        }
    }
    private void randomBoxValue(ref int[] arr)
    {
        RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        arr = arr.OrderBy(x => Next(random)).ToArray();
        
        //return arr;
    }

    private int Next(RNGCryptoServiceProvider random)
    {
        byte[] randomInt = new byte[4];
        random.GetBytes(randomInt);
        return Convert.ToInt32(randomInt[0]);
    }
}
