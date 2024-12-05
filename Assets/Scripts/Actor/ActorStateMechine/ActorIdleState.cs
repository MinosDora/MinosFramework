using System;
using System.Collections.Generic;

public class ActorIdleState : ActorState
{
    public override string StateName => nameof(ActorIdleState);

    public override void Enter(string fromStateName = "", Object data = null)
    {

    }

    public override void Tick()
    {
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.Space))
        {
            this.StateMachine.ChangeState<ActorJumpState>();
        }
    }

    public override void Exit(string toStateName = "")
    {

    }
}