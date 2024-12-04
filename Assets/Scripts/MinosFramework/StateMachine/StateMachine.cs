using System;
using System.Collections;
using System.Collections.Generic;

public class StateMachine
{
    private State currentState;
    public State CurrentState => currentState;

    private Dictionary<string, State> stateDict = new Dictionary<string, State>();

    public void AddState<T>() where T : State, new()
    {
        T state = new T();
        state.Init(this);

        if (!stateDict.TryAdd(state.StateName, state))
        {

        }
    }

    private T GetState<T>() where T : State
    {
        foreach (var item in stateDict)
        {
            if (typeof(T) == item.Value.GetType())
            {
                return item.Value as T;
            }
        }
        return null;
    }

    private State GetState(string stateName)
    {
        if (stateDict.TryGetValue(stateName, out State state))
        {
            return state;
        }
        return null;
    }

    public void ChangeState<T>() where T : State
    {
        State targetState = GetState<T>();
        if (targetState == null)
        {
            return;
        }

        if (currentState != null)
        {
            if (currentState.StateName == targetState.StateName)
            {
                return;
            }

            currentState.Exit();
        }
        currentState = targetState;
        currentState.Enter();
        UnityEngine.Debug.Log("" + currentState.StateName);
    }

    public void ChangeState(string targetStateName)
    {
        if (currentState != null && currentState.StateName == targetStateName)
        {
            return;
        }
        State targetState = GetState(targetStateName);
        if (targetState == null)
        {
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }
        currentState = targetState;
        currentState.Enter();
        UnityEngine.Debug.Log("" + targetStateName);
    }

    public void Tick()
    {
        if (currentState != null)
        {
            currentState.Tick();
        }
    }
}