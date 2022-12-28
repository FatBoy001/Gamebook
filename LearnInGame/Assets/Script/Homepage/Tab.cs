using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tab : MonoBehaviour
{
    public void setTab()
    {
        ChangePage.instance.changePage(this.name);   
    }
}
