using System;
using System.Collections.Generic;
using UnityEngine;
using MinosFramework;

public class GameEntrance : MonoBehaviour
{
    public static GameEntrance Instance;

    private ActorStateMachine actorStateMachine = new ActorStateMachine();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);

        actorStateMachine.AddState<ActorIdleState>();
        actorStateMachine.AddState<ActorJumpState>();
        actorStateMachine.ChangeState<ActorIdleState>();
    }

    private void Update()
    {
        TimerManager.Instance.Tick();
        actorStateMachine?.Tick();
    }
}