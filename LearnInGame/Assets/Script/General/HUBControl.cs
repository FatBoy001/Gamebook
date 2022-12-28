using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBControl : MonoBehaviour
{
    public Animator animator;
    public bool pause;

    public void showMenu()
    {
        animator.SetBool("Show",!animator.GetBool("Show"));
    }

    public void pauseTime()
    {
        //Debug.Log("I am here now pause is " + p);
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

}
