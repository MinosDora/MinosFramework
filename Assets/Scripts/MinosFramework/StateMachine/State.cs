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
    protected StateMachine stateMachine;

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
    public abstract void Enter();

    /// <summary>
    /// 每帧执行
    /// </summary>
    public abstract void Tick();

    /// <summary>
    /// 状态退出
    /// </summary>
    public abstract void Exit();
}