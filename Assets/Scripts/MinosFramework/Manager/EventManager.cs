using System;
using System.Collections.Generic;

namespace MinosFramework
{
    /// <summary>
    /// 事件管理
    /// </summary>
    public class EventManager
    {
        public static readonly EventManager Instance = new EventManager();
        private EventManager() { }

        /// <summary>
        /// 存放事件列表的集合
        /// </summary>
        private Dictionary<EventType, Delegate> eventCollection = new Dictionary<EventType, Delegate>(Enum.GetNames(typeof(EventType)).Length);

        /// <summary>
        /// 添加事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="action">委托实例</param>
        public void AddListener(EventType eventType, Delegate action)
        {
            eventCollection.TryGetValue(eventType, out Delegate eventValue);
            eventCollection[eventType] = Delegate.Combine(eventValue, action);
        }

        /// <summary>
        /// 移除事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="action">委托实例</param>
        public void RemoveListener(EventType eventType, Delegate action)
        {
            eventCollection.TryGetValue(eventType, out Delegate eventValue);
            eventCollection[eventType] = Delegate.Remove(eventValue, action);
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="eventType">事件类型</param>
        /// <param name="args">参数列表</param>
        public void TriggerEvent(EventType eventType, params object[] args)
        {
            eventCollection.TryGetValue(eventType, out Delegate eventValue);
            if (eventValue != null)
            {
                eventValue.DynamicInvoke(args);
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="arg">参数</param>
        public void TriggerEvent<T>(EventType eventType, T arg)
        {
            eventCollection.TryGetValue(eventType, out Delegate eventValue);
            if (eventValue != null)
            {
                if (eventValue is Action<T> action)
                {
                    action(arg);
                }
                else
                {
                    Console.WriteLine($"The right type of {eventType.ToString()} is {eventValue.GetType().FullName}");
                }
            }
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <typeparam name="T1">参数类型1</typeparam>
        /// <typeparam name="T2">参数类型2</typeparam>
        /// <param name="eventType">事件类型</param>
        /// <param name="arg1">参数1</param>
        /// <param name="arg2">参数2</param>
        public void TriggerEvent<T1, T2>(EventType eventType, T1 arg1, T2 arg2)
        {
            eventCollection.TryGetValue(eventType, out Delegate eventValue);
            if (eventValue != null)
            {
                if (eventValue is Action<T1, T2> action)
                {
                    action(arg1, arg2);
                }
                else
                {
                    Console.WriteLine($"The right type of {eventType.ToString()} is {eventValue.GetType().FullName}");
                }
            }
        }
    }
}