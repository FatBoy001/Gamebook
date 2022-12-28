using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Test : MonoBehaviour
{
    private Quiz quiz;
    private System.Random random = new System.Random();
    private int answerIndex;
    public Transform[] formulaTransfom;
    public Transform operatorTransfom;
    public GameObject numberBox;   //NumberBox
    public GameObject operatorBox;
    private GameObject[] testBox = new GameObject[4];
    public bool testLockUp = false;

    public int answer { get; set; }


    public bool checkAnswer()
    {
        if (formulaTransfom[answerIndex].GetComponent<PlaceItem>().numberBox != null && formulaTransfom[answerIndex].GetComponent<PlaceItem>().numberBox.boxValue == answer)
            return true;
        else
            return false;
    }

    public void createTest()
    {
        quiz = new Quiz();

        Debug.Log(quiz.formula[0] + " " + quiz.formula[1] + " " + quiz.formula[2]);

        operatorBox.GetComponent<NumberBox>().boxType = "operator";
        if (quiz.formula[1] >= 0)
            operatorBox.GetComponent<NumberBox>().boxValue = -2;
        else
            operatorBox.GetComponent<NumberBox>().boxValue = -4;


        answerIndex = random.Next(0, 2);
        answer = Mathf.Abs(quiz.formula[answerIndex]);
        testBox[answerIndex] =Instantiate(operatorBox, operatorTransfom.transform.position, operatorTransfom.transform.rotation);

        for (int i = 0; i < 3; i++)
        {
            if (i == answerIndex)
                continue;
            
            numberBox.GetComponent<NumberBox>().boxValue = Math.Abs(quiz.formula[i]);
            numberBox.GetComponent<NumberBox>().boxType = "number";
            testBox[i] = Instantiate(numberBox, formulaTransfom[i].transform.position, formulaTransfom[i].transform.rotation);
        }

    }

    public void distroyTest()
    {
        for (int i = 0; i < testBox.Length; i++)
        {
            Destroy(testBox[i], 0f);
        }
    }
    
    public void lockDownAnswer()
    {
        if (formulaTransfom[answerIndex].GetComponent<PlaceItem>().numberBox == null) return;
        //if (!checkAnswer()) return;

        numberBox.GetComponent<NumberBox>().boxValue = formulaTransfom[answerIndex].GetComponent<PlaceItem>().numberBox.boxValue;
        numberBox.GetComponent<NumberBox>().boxType = formulaTransfom[answerIndex].GetComponent<PlaceItem>().numberBox.boxType;

        Destroy(formulaTransfom[answerIndex].GetComponent<PlaceItem>().numberBox.gameObject);
        testBox[3] = Instantiate(numberBox, formulaTransfom[answerIndex].transform.position, formulaTransfom[answerIndex].transform.rotation);
        testLockUp = true;

        /*
                Destroy();
                Instantiate(,formulaTransfom[answerIndex].transform.position, formulaTransfom[answerIndex].transform.rotation);
          */
        //換他的layer
        //int LayerBlock = LayerMask.NameToLayer("Block");
        //formulaTransfom[answerIndex].GetComponent<PlaceItem>().numberBox.gameObject.layer = LayerBlock;
    }

    public Transform getAnsPlaceItem()
    {
        return formulaTransfom[answerIndex].GetComponent<Transform>();
    }

}
