using System;
using UnityEngine;

public class FSM<T>
{
    public FSM(T _owner, State<T> startState)
    {
        owner = _owner;
        ChangeState(startState);
    }
    
    private readonly T owner;
    private State<T> state;

    public string GetStateName()
    {
        return state != null ? state.Name : "No state";
    }

    public void ChangeState(State<T> _state)
    {
        state?.Exit(owner);

        state = _state;
        state.Enter(owner);
    }

    public void Update()
    {
        state.Execute(owner);
    }
}
