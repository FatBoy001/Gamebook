using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyObject : MonoBehaviour
{
    public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}
