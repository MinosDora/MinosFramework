using System;
using System.Collections.Generic;

/// <summary>
/// 有限状态机
/// </summary>
public class StateMachine
{
    /// <summary>
    /// 当前的状态
    /// </summary>
    private State currentState;
    /// <summary>
    /// 当前的状态
    /// </summary>
    public State CurrentState => currentState;

    /// <summary>
    /// 所有状态字典，key为状态名称，value为状态
    /// </summary>
    private Dictionary<string, State> stateDict = new Dictionary<string, State>();

    /// <summary>
    /// 添加状态
    /// </summary>
    /// <typeparam name="T">状态类型</typeparam>
    public void AddState<T>() where T : State, new()
    {
        T state = new T();
        state.Init(this);

        if (!stateDict.TryAdd(state.StateName, state))
        {

        }
    }

    /// <summary>
    /// 获取状态
    /// </summary>
    /// <typeparam name="T">状态类型</typeparam>
    /// <returns>状态</returns>
    private T GetState<T>() where T : State
    {
        foreach (var item in stateDict)
        {
            if (typeof(T) == item.Value.GetType())
            {
                return item.Value as T;
            }
        }
        return null;
    }

    /// <summary>
    /// 获取状态
    /// </summary>
    /// <param name="stateName">状态名称</param>
    /// <returns></returns>
    private State GetState(string stateName)
    {
        if (stateDict.TryGetValue(stateName, out State state))
        {
            return state;
        }
        return null;
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <typeparam name="T">状态类型</typeparam>
    public void ChangeState<T>() where T : State
    {
        State targetState = GetState<T>();
        if (targetState == null)
        {
            return;
        }

        if (currentState != null)
        {
            if (currentState.StateName == targetState.StateName)
            {
                return;
            }

            currentState.Exit();
        }
        currentState = targetState;
        currentState.Enter();
        UnityEngine.Debug.Log("" + currentState.StateName);
    }

    /// <summary>
    /// 切换状态
    /// </summary>
    /// <param name="targetStateName">状态名称</param>
    public void ChangeState(string targetStateName)
    {
        if (currentState != null && currentState.StateName == targetStateName)
        {
            return;
        }
        State targetState = GetState(targetStateName);
        if (targetState == null)
        {
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = targetState;
        currentState.Enter();
        UnityEngine.Debug.Log("" + targetStateName);
    }

    /// <summary>
    /// 每帧执行
    /// </summary>
    public void Tick()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }
    }
}