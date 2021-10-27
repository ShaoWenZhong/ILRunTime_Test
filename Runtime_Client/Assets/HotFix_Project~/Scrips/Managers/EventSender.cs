namespace HotFix_Project.Scrips.Managers
{
    using System;
    using System.Collections.Generic;
    using Scrips.Utils;

    public delegate void Callback();
    public delegate void Callback<T>(T arg1);
    public delegate void Callback<T, U>(T arg1, U arg2);
    public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
    public delegate void Callback<T, U, V, W>(T arg1, U arg2, V arg3, W arg4);
    public delegate void Callback<T, U, V, W, X>(T arg1, U arg2, V arg3, W arg4, X arg5);
    public delegate void Callback<T, U, V, W, X, Y>(T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6);

    /// <summary>
    /// 事件发送器
    /// </summary>
    public class EventSender<TKey, TValue>
    {
        /// <summary> 事件表 </summary>
        private Dictionary<EventType, Delegate> dict = new Dictionary<EventType, Delegate>();

        /// <summary>
        /// 当订阅事件时调用
        /// </summary>
        /// <param name="string"></param>
        /// <param name="callback"></param>
        private void OnListenerAdding(EventType eventType, Delegate callback)
        {
            //判断字典里面是否包含该事件码
            if (!dict.ContainsKey(eventType))
            {
                dict.Add(eventType, null);
            }
            Delegate d = dict[eventType];
            if (d != null && d.GetType() != callback.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托是{2}", eventType, d.GetType(), callback.GetType()));
            }
        }


        /// <summary>
        /// 当取消订阅事件时调用
        /// </summary>
        /// <param name="string"></param>
        /// <param name="callBack"></param>
        private void OnListenerRemoving(EventType eventType, Delegate callBack)
        {
            if (dict.ContainsKey(eventType))
            {
                Delegate d = dict[eventType];
                if (d == null)
                {
                    throw new Exception(string.Format("移除监听事件错误：事件{0}没有对应的委托", eventType));
                }
                else if (d.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听事件错误：尝试为事件{0}移除不同类型的委托，当前事件所对应的委托为{1}，要移除的委托是{2}", eventType, d.GetType(), callBack.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听事件错误：没有事件码{0}", eventType));
            }
        }


        /// <summary>
        /// 无参的监听事件（即订阅事件）的方法
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        public void AddListener(EventType eventType, Callback callBack)
        {
            OnListenerAdding(eventType, callBack);
            dict[eventType] = (Callback)dict[eventType] + callBack;
        }

        /// <summary>
        /// 无参的监听事件（即订阅事件）的方法
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        public void AddListener<T>(EventType eventType, Callback<T> callBack)
        {
            OnListenerAdding(eventType, callBack);
            dict[eventType] = (Callback<T>)dict[eventType] + callBack;
        }
        public void AddListener<T,U>(EventType eventType, Callback<T,U> callBack)
        {
            OnListenerAdding(eventType, callBack);
            dict[eventType] = (Callback<T,U>)dict[eventType] + callBack;
        }
        public void AddListener<T, U,V>(EventType eventType, Callback<T, U,V> callBack)
        {
            OnListenerAdding(eventType, callBack);
            dict[eventType] = (Callback<T, U,V>)dict[eventType] + callBack;
        }
        public void AddListener<T, U, V,W>(EventType eventType, Callback<T, U, V,W> callBack)
        {
            OnListenerAdding(eventType, callBack);
            dict[eventType] = (Callback<T, U, V,W>)dict[eventType] + callBack;
        }
        public void AddListener<T, U, V, W,X>(EventType eventType, Callback<T, U, V, W,X> callBack)
        {
            OnListenerAdding(eventType, callBack);
            dict[eventType] = (Callback<T, U, V, W,X>)dict[eventType] + callBack;
        }
        public void AddListener<T, U, V, W, X,Y>(EventType eventType, Callback<T, U, V, W, X,Y> callBack)
        {
            OnListenerAdding(eventType, callBack);
            dict[eventType] = (Callback<T, U, V, W, X,Y>)dict[eventType] + callBack;
        }

        /// <summary>
        /// 无参的移除监听事件的方法
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="callBack"></param>
        public void RemoveListener(EventType eventType, Callback callBack)
        {
            OnListenerRemoving(eventType, callBack);
            dict[eventType] = (Callback)dict[eventType] - callBack;
        }

        public void RemoveListener<T>(EventType eventType, Callback<T> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            dict[eventType] = (Callback<T>)dict[eventType] - callBack;
        }
        public void RemoveListener<T, U>(EventType eventType, Callback<T, U> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            dict[eventType] = (Callback<T, U>)dict[eventType] - callBack;
        }
        public void RemoveListener<T, U, V>(EventType eventType, Callback<T, U, V> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            dict[eventType] = (Callback<T, U, V>)dict[eventType] - callBack;
        }
        public void RemoveListener<T, U, V, W>(EventType eventType, Callback<T, U, V, W> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            dict[eventType] = (Callback<T, U, V, W>)dict[eventType] - callBack;
        }
        public void RemoveListener<T, U, V, W, X>(EventType eventType, Callback<T, U, V, W, X> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            dict[eventType] = (Callback<T, U, V, W, X>)dict[eventType] - callBack;
        }
        public void RemoveListener<T, U, V, W, X, Y>(EventType eventType, Callback<T, U, V, W, X, Y> callBack)
        {
            OnListenerRemoving(eventType, callBack);
            dict[eventType] = (Callback<T, U, V, W, X, Y>)dict[eventType] - callBack;
        }


        /// <summary> 是否已经拥有该类型的事件监听器 </summary>
        /// <param name="eventType">事件名称</param>
        public bool HasListener(EventType eventType)
        {
            return dict.ContainsKey(eventType);
        }


        /// <summary> 发送事件 </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventArg">事件参数</param>
        public void SendMessage(EventType eventType)
        {
            Delegate d;
            if (dict.TryGetValue(eventType, out d))
            {
                Callback callBack = d as Callback;
                if (callBack != null)
                    callBack();
                else
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托有不同的类型", eventType.ToString()));
            }
        }

        public void SendMessage<T>(EventType eventType,T arg1)
        {
            Delegate d;
            if (dict.TryGetValue(eventType, out d))
            {
                Callback<T> callBack = d as Callback<T>;
                if (callBack != null)
                    callBack(arg1);
                else
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托有不同的类型", eventType.ToString()));
            }
        }

        public void SendMessage<T,U>(EventType eventType, T arg1, U arg2)
        {
            Delegate d;
            if (dict.TryGetValue(eventType, out d))
            {
                Callback<T,U> callBack = d as Callback<T,U>;
                if (callBack != null)
                    callBack(arg1, arg2);
                else
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托有不同的类型", eventType.ToString()));
            }
        }

        public void SendMessage<T, U,V>(EventType eventType, T arg1, U arg2,V arg3)
        {
            Delegate d;
            if (dict.TryGetValue(eventType, out d))
            {
                Callback<T, U,V> callBack = d as Callback<T, U,V>;
                if (callBack != null)
                    callBack(arg1, arg2,arg3);
                else
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托有不同的类型", eventType.ToString()));
            }
        }
        public void SendMessage<T, U, V,W>(EventType eventType, T arg1, U arg2, V arg3, W arg4)
        {
            Delegate d;
            if (dict.TryGetValue(eventType, out d))
            {
                Callback<T, U, V,W> callBack = d as Callback<T, U, V,W>;
                if (callBack != null)
                    callBack(arg1, arg2, arg3, arg4);
                else
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托有不同的类型", eventType.ToString()));
            }
        }


        public void SendMessage<T, U, V, W,X>(EventType eventType, T arg1, U arg2, V arg3, W arg4, X arg5)
        {
            Delegate d;
            if (dict.TryGetValue(eventType, out d))
            {
                Callback<T, U, V, W,X> callBack = d as Callback<T, U, V, W,X>;
                if (callBack != null)
                    callBack(arg1, arg2, arg3, arg4, arg5);
                else
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托有不同的类型", eventType.ToString()));
            }
        }

        public void SendMessage<T, U, V, W, X,Y>(EventType eventType, T arg1, U arg2, V arg3, W arg4, X arg5, Y arg6)
        {
            Delegate d;
            if (dict.TryGetValue(eventType, out d))
            {
                Callback<T, U, V, W, X,Y> callBack = d as Callback<T, U, V, W, X,Y>;
                if (callBack != null)
                    callBack(arg1, arg2, arg3, arg4, arg5, arg6);
                else
                    throw new Exception(string.Format("广播事件错误：事件{0}对应委托有不同的类型", eventType.ToString()));
            }
        }


        /// <summary> 清理所有事件监听器 </summary>
        public void Clear()
        {
            dict.Clear();
        }

    }
}
