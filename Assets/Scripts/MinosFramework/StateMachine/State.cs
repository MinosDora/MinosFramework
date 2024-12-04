using System;
using System.Collections;
using System.Collections.Generic;

public abstract class State
{
    public abstract string StateName { get; }

    protected StateMachine stateMachine;

    public void Init(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public abstract void Enter();

    public abstract void Tick();

    public abstract void Exit();
}