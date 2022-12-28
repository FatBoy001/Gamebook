using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Linq;
using System;

public class Quiz
{
    public int[] formula { get; set; }
    private System.Random randomNumber = new System.Random();
    public Quiz()
    {
        formula = new int[3];
        formula[0] = randomNumber.Next(1, 9);
        formula[1] = randomNumber.Next(-1* Math.Abs(formula[0]),9);
        formula[2] = formula[0] + formula[1];
    }
    public string getSign(int index)
    {
        if (formula[index] >= 0)
        {
           return "Plus";
        }
        else
            return "Minus";
    }


}