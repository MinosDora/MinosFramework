using System;
using System.Collections.Generic;

public abstract class ActorState : State
{
    protected new ActorStateMachine StateMachine => base.StateMachine as ActorStateMachine;
}