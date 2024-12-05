using System;
using System.Collections.Generic;
using MinosFramework;

public class ActorJumpState : ActorState
{
    public override string StateName => nameof(ActorJumpState);

    private int? jumTimerId;

    public override void Enter(string fromStateName = "", Object data = null)
    {
        jumTimerId = TimerManager.Instance.DelayInvoke(OnJumpEnd, 2f);
    }

    public override void Tick()
    {
        if (UnityEngine.Input.GetKey(UnityEngine.KeyCode.Escape))
        {
            this.StateMachine.ChangeState<ActorIdleState>("From Escape.");
        }
    }

    public override void Exit(string toStateName = "")
    {
        if (jumTimerId != null)
        {
            TimerManager.Instance.CancelInvoke(jumTimerId.Value);
        }
    }

    public void OnJumpEnd()
    {
        jumTimerId = null;
        this.StateMachine.ChangeState<ActorIdleState>();
    }
}