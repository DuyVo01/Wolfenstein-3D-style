using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public BaseEnemyState currentState;

    public void Initialize(BaseEnemyState initializeState)
    {
        currentState = initializeState;
        currentState.Enter();
    }

    public void ChangeState(BaseEnemyState newState)
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
