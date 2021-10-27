using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Monoµ¥Àý
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton_Mono <T>: MonoBehaviour where T : Singleton_Mono<T>
{
    protected static T _instance = null;
    public static T Instance
    {
        get
        {
            if (null == _instance)
            {
                GameObject go = GameObject.Find("GameApp");
                if (null == go)
                {
                    go = new GameObject("GameApp");
                    DontDestroyOnLoad(go);
                }
                _instance = go.GetComponent<T>();
                if (null == _instance)
                {
                    _instance = go.AddComponent<T>();
                }
            }
            return _instance;
        }
    }
}
