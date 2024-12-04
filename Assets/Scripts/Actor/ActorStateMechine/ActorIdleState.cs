using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorIdleState : State
{
    public override string StateName => nameof(ActorIdleState);

    public override void Enter()
    {

    }

    public override void Tick()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            this.stateMachine.ChangeState<ActorJumpState>();
        }
    }

    public override void Exit()
    {

    }
}