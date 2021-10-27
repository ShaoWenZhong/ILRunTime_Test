using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project.Scrips.Utils
{
    /// <summary>
    /// 单例模式
    /// </summary>
    public abstract class Singleton_HotFix<T> where T : class, new()
    {
        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                if (null == _instance)
                    _instance = new T();
                return _instance;
            }
        }

        protected Singleton_HotFix()
        {
            if (null != _instance)
                Debug.LogError("this" + (typeof(T)).ToString() + "Singleton is not null");
            Init();
        }

        protected virtual void Init()
        { }
    }
}
