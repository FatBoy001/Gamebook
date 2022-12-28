using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDataType : MonoBehaviour
{
    public void setData()
    {
        if (gameObject.tag=="Null")
        {
            RecordData.setDataType("", "");
            return;
        }
        RecordData.setDataType(gameObject.tag, gameObject.name);
    }
    
}
