using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ·������
/// </summary>
public static class URLManager
{
    public static string GetScenePath(int sceneId)
    {
        return "scene_" + sceneId;
    }
}
