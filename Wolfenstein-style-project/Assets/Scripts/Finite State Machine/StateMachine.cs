using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine 
{
    public BaseState currentState;

    public void Initialize(BaseState initializeState)
    {
        currentState = initializeState;
        currentState.Enter();
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;

        currentState.Enter();
    }

    public void LogicalUpdate()
    {
        currentState.LogicalUpdate();
    }

    public void PhysicalUpdate()
    {
        currentState.PhysicalUpdate();
    }
}
