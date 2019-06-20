using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 主线程执行工具类
/// </summary>
public class LoomUtil : MonoBehaviour
{
    public static LoomUtil Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    //存储待执行函数的队列
    private Queue<Action> actions = new Queue<Action>(5);

    /// <summary>
    /// 添加action，该方法通常由子线程调用
    /// </summary>
    /// <param name="action"></param>
    public void EnqueueAction(Action action)
    {
        actions.Enqueue(action);
    }

    /// <summary>
    /// 在Update中执行action，由主线程调用
    /// </summary>
    private void Update()
    {
        if (actions.Count > 0)
        {
            lock (actions)
            {
                while (actions.Count > 0)
                {
                    Action action = actions.Dequeue();
                    action?.Invoke();
                }
            }
        }
    }
}