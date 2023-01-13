using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyNonCombatState
{
    private Transform[] patrolPoints;

    private int patrolIndex = 0;
    private Vector3 lastKnownPatrolPoint;

    private float recognitionStartTime;

    public EnemyPatrolState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager, Transform[] patrolPoints) : base(stateMachine, enemyStateManager)
    {
        this.patrolPoints = patrolPoints;
        lastKnownPatrolPoint = patrolPoints[0].position;
    }

    public override void Enter()
    {
        base.Enter();
        enemyStateManager.agent.SetDestination(lastKnownPatrolPoint);
        
        enemyStateManager.enemyAnimator.SetBool("Patrol", true);
    }

    public override void Exit()
    {
        base.Exit();
        lastKnownPatrolPoint = patrolPoints[patrolIndex].position;
        enemyStateManager.enemyAnimator.SetBool("Patrol", false);
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if (!inCombat)
        {
            if (enemyStateManager.agent.remainingDistance <= 0.5f)
            {
                patrolIndex++;
                ToNextPatrolPoint();
                stateMachine.ChangeState(enemyStateManager.enemyStationaryState);
            }
        }
        
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }

    private void ToNextPatrolPoint()
    {
        if (patrolIndex == patrolPoints.Length)
        {
            patrolIndex = 0;
        }
    }
}
