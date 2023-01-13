using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearchingState : EnemyCombatState
{
    private float searchingTime;
    private float searchingPassedTime;

    public EnemySearchingState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager, float searchingTime) : base(stateMachine, enemyStateManager)
    {
        this.searchingTime = searchingTime;
    }

    public override void Enter()
    {
        base.Enter();
        searchingPassedTime = searchingTime;
      
        enemyStateManager.agent.SetDestination(enemyStateManager.lastKnownTargetPosition);
        enemyStateManager.enemyAnimator.SetBool("Searching", true);
        enemyStateManager.agent.speed = enemyStateManager.agent.speed * 2;
    }

    public override void Exit()
    {
        base.Exit();
        enemyStateManager.agent.speed = enemyStateManager.agent.speed / 2;
        enemyStateManager.enemyAnimator.SetBool("Searching", false);
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if (enemyStateManager.isDetectingPlayer)
        {
            stateMachine.ChangeState(enemyStateManager.enemyShootingState);
        }
        else
        {
            Searching();
            enemyStateManager.enemyAnimator.SetFloat("Distance", enemyStateManager.agent.remainingDistance);
            if (enemyStateManager.agent.remainingDistance < 0.01f)
            {
                if (searchingPassedTime > 0)
                {
                    searchingPassedTime -= Time.deltaTime;
                }
                else
                {
                    stateMachine.ChangeState(enemyStateManager.enemyPatrolState);
                }
            }
        }
        
    }

    private void Searching() 
    {
        if (enemyStateManager.enemyStatus.isTakingDamage)
        {
            enemyStateManager.lastKnownTargetPosition = enemyStateManager.player.position;
            enemyStateManager.agent.SetDestination(enemyStateManager.lastKnownTargetPosition);
            enemyStateManager.enemyStatus.isTakingDamage = false;
        }
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }
}
