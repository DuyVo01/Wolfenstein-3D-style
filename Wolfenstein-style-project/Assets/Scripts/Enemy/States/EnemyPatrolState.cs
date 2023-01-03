using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : BaseEnemyState
{
    private Transform[] patrolPoints;

    private int patrolIndex = 0;
    private Vector3 lastKnownPatrolPoint;

    private float recognitionStartTime;
    private float recognitionTime;

    public EnemyPatrolState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager, Transform[] patrolPoints) : base(stateMachine, enemyStateManager)
    {
        this.patrolPoints = patrolPoints;
        lastKnownPatrolPoint = patrolPoints[0].position;
        recognitionStartTime = enemyStateManager.recognitionTime;
    }

    public override void Enter()
    {
        base.Enter();
        enemyStateManager.agent.SetDestination(lastKnownPatrolPoint);
        recognitionTime = recognitionStartTime;
        Debug.Log("Enemy Patrol");
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
        if(enemyStateManager.agent.remainingDistance <= 0.5f)
        {
            //if (patrolLookingTime > 0)
            //{
            //    patrolLookingTime -= Time.deltaTime;
            //}
            //else
            //{
            //    patrolIndex++;
            //    patrolLookingTime = patrolLookingStartTime;
            //    ToNextPatrolPoint();
            //}
            patrolIndex++;
            ToNextPatrolPoint();
            stateMachine.ChangeState(enemyStateManager.enemyStationaryState);
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
    }
}
