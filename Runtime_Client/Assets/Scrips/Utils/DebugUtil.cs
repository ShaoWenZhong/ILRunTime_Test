using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��־����
/// </summary>
public static class DebugUtil
{
    public static void Log(string msg)
    {
        Debug.Log(msg);
    }

    public static void LogError(string msg)
    {
        Debug.LogError(msg);
    }
}
