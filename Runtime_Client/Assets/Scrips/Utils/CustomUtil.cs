using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CustomUtil
{

    public static T GetChild<T>(GameObject parent, string path)
    {
        T _type = default;
        if (parent == null)
            return _type;
        Transform transform = parent.transform;
        Transform child = transform.Find(path);
        if (child == null)
        {
            DebugUtil.LogError("找不到子物体parent:" + parent.name + "  child :" + path);
            return _type;
        }
        _type = child.GetComponent<T>();
        return _type;
    }
}

