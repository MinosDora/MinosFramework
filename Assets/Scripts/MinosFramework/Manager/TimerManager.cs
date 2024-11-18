using System;
using System.Collections.Generic;
using UnityEngine;

namespace MinosFramework
{
    /// <summary>
    /// 计时器管理
    /// </summary>
    public class TimerManager
    {
        public static readonly TimerManager Instance = new TimerManager();
        private TimerManager() { }

        /// <summary>
        /// 计时器
        /// </summary>
        private class Timer
        {
            /// <summary>
            /// 待执行的事件
            /// </summary>
            private Delegate action;

            /// <summary>
            /// 下次执行的时间
            /// </summary>
            private float nextInvokeTime;

            /// <summary>
            /// 下次执行的时间
            /// </summary>
            public float NextInvokeTime => nextInvokeTime;

            /// <summary>
            /// 间隔执行的时间
            /// </summary>
            private float intervalTime;

            /// <summary>
            /// 是否重复执行事件
            /// </summary>
            public bool IsRepeat => intervalTime != 0f;

            public Timer(Delegate action, float nextInvokeTime, float intervalTime = 0f)
            {
                this.action = action;
                this.nextInvokeTime = nextInvokeTime;
                this.intervalTime = intervalTime;
            }

            /// <summary>
            /// 检测执行
            /// </summary>
            /// <returns>是否到达执行事件的时间</returns>
            public bool Tick()
            {
                if (Time.realtimeSinceStartup >= nextInvokeTime)
                {
                    action?.DynamicInvoke();
                    if (IsRepeat)
                    {
                        nextInvokeTime = nextInvokeTime + intervalTime;
                    }
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 计时器列表
        /// </summary>
        private List<Timer> timerList = new List<Timer>();

        /// <summary>
        /// 计时器队列，key为计时器所在列表的索引，value为计时器下次执行的时间
        /// </summary>
        private PriorityQueue<int, float> timerPQ = new PriorityQueue<int, float>();

        /// <summary>
        /// 延迟执行事件
        /// </summary>
        /// <param name="action">事件</param>
        /// <param name="delay">延迟时间</param>
        /// <returns>计时器索引</returns>
        public int DelayInvoke(Delegate action, float delay)
        {
            float nextInvokeTime = Time.realtimeSinceStartup + delay;
            int timerIndex = timerList.Count;
            Timer timer = new Timer(action, nextInvokeTime);
            timerList.Add(timer);
            timerPQ.Enqueue(timerIndex, nextInvokeTime);
            return timerIndex;
        }

        /// <summary>
        /// 重复执行事件
        /// </summary>
        /// <param name="action">事件</param>
        /// <param name="interval">间隔时间</param>
        /// <returns>计时器索引</returns>
        public int RepeatInvoke(Delegate action, float interval)
        {
            float nextInvokeTime = Time.realtimeSinceStartup + interval;
            int timerIndex = timerList.Count;
            Timer timer = new Timer(action, nextInvokeTime, interval);
            timerList.Add(timer);
            timerPQ.Enqueue(timerIndex, nextInvokeTime);
            return timerIndex;
        }

        /// <summary>
        /// 取消执行
        /// </summary>
        /// <param name="timerIndex">计时器索引</param>
        public void CancelInvoke(int timerIndex)
        {
            timerList[timerIndex] = null;
        }

        /// <summary>
        /// 每帧检测
        /// </summary>
        public void Tick()
        {
            while (timerPQ.Count > 0)
            {
                int timerIndex = timerPQ.Peek();
                // 根据根节点取到的索引拿到当前优先级最高的计时器
                Timer timer = timerList[timerIndex];
                if (timer != null)
                {
                    if (timer.Tick())
                    {
                        // 计时器执行后先出列，如果是重复执行的计时器，则使用新的执行时间再次入列
                        timerPQ.Dequeue();
                        if (!timer.IsRepeat)
                        {
                            timerList[timerIndex] = null;
                        }
                        else
                        {
                            timerPQ.Enqueue(timerIndex, timer.NextInvokeTime);
                        }
                    }
                    // 如果当前计时器未执行，表明优先级低的计时器都未到执行时间，退出本次循环
                    else
                    {
                        break;
                    }
                }
                // 如果timer为空，表示该计时器已取消，直接出列
                else
                {
                    timerPQ.Dequeue();
                }
            }
        }

        /// <summary>
        /// 清空计时器列表
        /// </summary>
        public void Clear()
        {
            timerList.Clear();
            timerPQ.Clear();
        }
    }
}