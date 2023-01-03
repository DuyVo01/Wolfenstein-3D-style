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
        enemyStateManager.enemyAnimator.SetBool("Death", false);
    }

    public virtual void LogicalUpdate()
    {
        if (enemyStateManager.hit)
        {
            stateMachine.ChangeState(enemyStateManager.enemySearchingState);
        } 
        if(enemyStateManager.enemyStatus.enemyCurrentHealth <= 0)
        {
            stateMachine.ChangeState(enemyStateManager.enemyDeathState);
        }
    }

    public virtual void PhysicalUpdate()
    {

    }

    public virtual void LateUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
