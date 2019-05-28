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

    private Queue<Action> actions = new Queue<Action>(5);
    /// <summary>
    /// 添加action
    /// </summary>
    /// <param name="action"></param>
    public void EnqueueAction(Action action)
    {
        actions.Enqueue(action);
    }

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