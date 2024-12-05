using System;
using System.Collections.Generic;
using UnityEngine;

public class ActorStateMachine : StateMachine
{
    public new ActorState CurrentState => base.CurrentState as ActorState;

    public void ChangeState<T>(string msg) where T : State
    {
        base.ChangeState<T>(msg);

        Debug.LogError($"ActorStateMachine.ChangeState, msg: {msg}");
    }
}