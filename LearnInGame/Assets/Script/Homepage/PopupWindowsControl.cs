using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PopupWindowsControl : MonoBehaviour
{
    public Animator animator;
    public bool pause;

    public void Start()
    {
        Time.timeScale = 1;
    }
    public void showPopupWindows()
    {
        animator.SetBool("Show", !animator.GetBool("Show"));
    }
    public void startGame()
    {
        animator.SetBool("Show", !animator.GetBool("Show"));
        SceneManager.LoadScene(RecordData.game + "Tutorial");
    }
}
