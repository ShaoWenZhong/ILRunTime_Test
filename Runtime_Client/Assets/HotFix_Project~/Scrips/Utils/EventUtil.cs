using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project.Scrips.Utils
{
    using Scrips.Managers;

    /// <summary>
    /// 事件工具
    /// <para>ZhangYu 2019-12-25</para>
    /// </summary>
    public static class EventUtil
    {
        /// <summary> 事件发送器 </summary>
        private static Managers.EventSender<EventType, object> sender = new Managers.EventSender<EventType, object>();

        /// <summary> 添加事件监听器 </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventHandler">事件处理器</param>
        public static void AddListener(EventType eventType, Callback eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }

        public static void AddListener<T>(EventType eventType, Callback<T> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }

        public static void AddListener<T,U>(EventType eventType, Callback<T,U> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }
        public static void AddListener<T,U,V>(EventType eventType, Callback<T,U,V> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }

        public static void AddListener<T,U,V,W>(EventType eventType, Callback<T,U,V,W> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }

        public static void AddListener<T, U, V, W,X>(EventType eventType, Callback<T, U, V, W,X> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }
        public static void AddListener<T, U, V, W, X,Y>(EventType eventType, Callback<T, U, V, W, X,Y> eventHandler)
        {
            sender.AddListener(eventType, eventHandler);
        }


        /// <summary> 移除事件监听器 </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="eventHandler">事件处理器</param>
        public static void RemoveListener(EventType eventType, Callback eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }
        public static void RemoveListener<T>(EventType eventType, Callback<T> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }
        public static void RemoveListener<T,U>(EventType eventType, Callback<T,U> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }
        public static void RemoveListener<T, U,V>(EventType eventType, Callback<T, U,V> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }

        public static void RemoveListener<T, U, V,W>(EventType eventType, Callback<T, U, V,W> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }
        public static void RemoveListener<T, U, V, W,X>(EventType eventType, Callback<T, U, V, W,X> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }
        public static void RemoveListener<T, U, V, W, X,Y>(EventType eventType, Callback<T, U, V, W, X,Y> eventHandler)
        {
            sender.RemoveListener(eventType, eventHandler);
        }



        /// <summary> 是否已经拥有该类型的事件监听器 </summary>
        /// <param name="eventType">事件类型</param>
        public static bool HasListener(EventType eventType)
        {
            return sender.HasListener(eventType);
        }

        /// <summary> 发送事件 </summary>
        /// <param name="eventType">事件类型</param>
        public static void SendMessage(EventType eventType)
        {
            sender.SendMessage(eventType);
        }

        /// <summary> 发送事件 </summary>
        /// <param name="eventType">事件类型</param>
        public static void SendMessage<T>(EventType eventType, T eventArg)
        {
            sender.SendMessage(eventType, eventArg);
        }

        public static void SendMessage<T,U>(EventType eventType, T eventArg, U eventArg2)
        {
            sender.SendMessage(eventType, eventArg, eventArg2);
        }

        public static void SendMessage<T,U,V>(EventType eventType, T eventArg, U eventArg2, V eventArg3)
        {
            sender.SendMessage(eventType, eventArg,eventArg3);
        }

        public static void SendMessage<T, U, V,W>(EventType eventType, T eventArg, U eventArg2, V eventArg3, W eventArg4)
        {
            sender.SendMessage(eventType, eventArg,eventArg2,eventArg3,eventArg4);
        }

        public static void SendMessage<T, U, V, W,X>(EventType eventType, T eventArg, U eventArg2, V eventArg3, W eventArg4, X eventArg5)
        {
            sender.SendMessage(eventType, eventArg, eventArg2, eventArg3, eventArg4, eventArg5);
        }

        public static void SendMessage<T, U, V, W, X,Y>(EventType eventType, T eventArg, U eventArg2, V eventArg3, W eventArg4, X eventArg5, Y eventArg6)
        {
            sender.SendMessage(eventType, eventArg, eventArg2, eventArg3, eventArg4, eventArg5, eventArg6);
        }


        /// <summary> 清理所有事件监听器 </summary>
        public static void Clear()
        {
            sender.Clear();
        }
    }

    public enum EventType
    {
        GameStart,
        LoginSucceed,
        UIViewPopChange,
        DisposeUI,
    }
}
