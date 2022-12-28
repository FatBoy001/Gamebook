using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePage : MonoBehaviour
{
    public static ChangePage instance;
    public List<GameObject> pages = new List<GameObject>();
    private void Awake()
    {
        if (ChangePage.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void changePage(string activeName)
    {
        foreach (GameObject page in pages)
        {
            if (page.name == activeName + "Page")
            {
                page.SetActive(true);
                continue;
            }
            page.SetActive(false);
        }
       
    }
}
