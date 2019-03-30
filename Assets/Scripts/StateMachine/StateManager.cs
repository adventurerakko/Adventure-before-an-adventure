using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager
{
    State currentState;
    public State GetCurrentState()
    {
        return currentState;
    }
    public void ChangeState(State newState)
    {
        if(currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    public void ExecuteCurrentState()
    {
        if(currentState != null)
            currentState.Execute();
    }
}