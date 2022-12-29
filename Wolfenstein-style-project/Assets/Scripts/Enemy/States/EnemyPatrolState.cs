using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : BaseEnemyState
{
    private Transform[] patrolPoints;

    private int patrolIndex = 0;
    private Vector3 lastKnownPatrolPoint;

    private float patrolLookingStartTime;
    private float patrolLookingTime;

    private float recognitionStartTime;
    private float recognitionTime;

    public EnemyPatrolState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager, Transform[] patrolPoints) : base(stateMachine, enemyStateManager)
    {
        this.patrolPoints = patrolPoints;
        lastKnownPatrolPoint = patrolPoints[0].position;
        recognitionStartTime = enemyStateManager.recognitionTime;
        patrolLookingStartTime = enemyStateManager.patrolLookingTime;
    }

    public override void Enter()
    {
        enemyStateManager.agent.SetDestination(lastKnownPatrolPoint);
        recognitionTime = recognitionStartTime;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        lastKnownPatrolPoint = patrolPoints[patrolIndex].position;
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if(enemyStateManager.agent.remainingDistance <= 0.01f)
        {
            if (patrolLookingTime > 0)
            {
                patrolLookingTime -= Time.deltaTime;
            }
            else
            {
                patrolLookingTime = patrolLookingStartTime;
                ToNextPatrolPoint();
            }
            
        } 
        else if (enemyStateManager.isDetectingPlayer)
        {
            if (recognitionTime > 0)
            {
               recognitionTime -= Time.deltaTime;
            }
            else
            {
               stateMachine.ChangeState(enemyStateManager.enemyShootingState);
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
        else
        {
            enemyStateManager.agent.SetDestination(patrolPoints[patrolIndex].position);
            Debug.Log(patrolIndex);
            patrolIndex++;
        }
        
    }
}
