using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// µ¥ÀýÄ£Ê½
/// </summary>
public abstract class Singleton<T> where T:class,new()
{
    protected static T _instance = null;

    public static T Instance
    {
        get {
            if (null == _instance)
                _instance = new T();
            return _instance;
        }
    }

    protected Singleton()
    {
        if (null != _instance)
            Debug.LogError("this" + (typeof(T)).ToString() + "Singleton is not null");
        Init();
    }

    public virtual void Init()
    { }
}