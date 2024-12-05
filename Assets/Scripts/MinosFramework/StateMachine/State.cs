using System;
using System.Collections.Generic;

public abstract class State
{
    /// <summary>
    /// 状态名称
    /// </summary>
    public abstract string StateName { get; }

    /// <summary>
    /// 状态机
    /// </summary>
    private StateMachine stateMachine;

    /// <summary>
    /// 状态机
    /// </summary>
    protected virtual StateMachine StateMachine => stateMachine;

    /// <summary>
    /// 初始化状态
    /// </summary>
    /// <param name="stateMachine"></param>
    public void Init(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    /// <summary>
    /// 状态进入
    /// </summary>
    /// <param name="fromStateName">上一个状态名称</param>
    /// <param name="data">参数</param>
    public abstract void Enter(string fromStateName = "", Object data = null);

    /// <summary>
    /// 每帧执行
    /// </summary>
    public abstract void Tick();

    /// <summary>
    /// 状态退出
    /// </summary>
    /// <param name="toStateName">下一个状态名称</param>
    public abstract void Exit(string toStateName = "");
}