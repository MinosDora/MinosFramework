using System;
using System.Collections.Generic;
using MinosFramework;
using UnityEngine;

public class ActorJumpState : State
{
    public override string StateName => nameof(ActorJumpState);

    private int? jumTimerId;

    public override void Enter()
    {
        jumTimerId = TimerManager.Instance.DelayInvoke(OnJumpEnd, 2f);
    }

    public override void Tick()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            this.stateMachine.ChangeState<ActorIdleState>();
        }
    }

    public override void Exit()
    {
        if (jumTimerId != null)
        {
            TimerManager.Instance.CancelInvoke(jumTimerId.Value);
        }
    }

    public void OnJumpEnd()
    {
        jumTimerId = null;
        this.stateMachine.ChangeState<ActorIdleState>();
    }
}