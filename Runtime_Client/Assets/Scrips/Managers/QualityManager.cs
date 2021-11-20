using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QualityManager : Singleton<QualityManager>
{
    private enum QualityLable
    {
        
    }


    public string GetQualityLableKey()
    {
        return "default";
    }
}
