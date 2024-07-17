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

        private class Timer
        {
            private Delegate action;

            private float nextInvokeTime;

            public float NextInvokeTime => nextInvokeTime;

            private float intervalTime;

            public bool IsRepeat => intervalTime != 0f;

            public Timer(Delegate action, float nextInvokeTime, float intervalTime = 0f)
            {
                this.action = action;
                this.nextInvokeTime = nextInvokeTime;
                this.intervalTime = intervalTime;
            }

            public bool Tick()
            {
                if (Time.realtimeSinceStartup > nextInvokeTime)
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

        private List<Timer> timerList = new List<Timer>();
        private PriorityQueue<int, float> timerPQ = new PriorityQueue<int, float>();

        public int DelayInvoke(Delegate action, float delay)
        {
            float nextInvokeTime = Time.realtimeSinceStartup + delay;
            int timerIndex = timerList.Count;
            Timer timer = new Timer(action, nextInvokeTime);
            timerList.Add(timer);
            timerPQ.Enqueue(timerIndex, nextInvokeTime);
            return timerIndex;
        }

        public int RepeatInvoke(Delegate action, float interval)
        {
            float nextInvokeTime = Time.realtimeSinceStartup + interval;
            int timerIndex = timerList.Count;
            Timer timer = new Timer(action, nextInvokeTime, interval);
            timerList.Add(timer);
            timerPQ.Enqueue(timerIndex, nextInvokeTime);
            return timerIndex;
        }

        public void CancelInvoke(int timerIndex)
        {
            timerList[timerIndex] = null;
        }

        public void Tick()
        {
            while (timerPQ.Count > 0)
            {
                int timerIndex = timerPQ.Peek();
                Timer timer = timerList[timerIndex];
                if (timer != null)
                {
                    if (timer.Tick())
                    {
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
                    else
                    {
                        break;
                    }
                }
                else
                {
                    timerPQ.Dequeue();
                }
            }
        }
    }
}