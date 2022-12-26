using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyState
{
    public EnemyStateMachine stateMachine;
    public EnemyStateManager enemyStateManager;

    public BaseEnemyState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager)
    {
        this.stateMachine = stateMachine;
        this.enemyStateManager = enemyStateManager;
    }

    public virtual void Enter()
    {

    }

    public virtual void LogicalUpdate()
    {

    }

    public virtual void PhysicalUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
