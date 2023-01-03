using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationaryState : BaseEnemyState
{

    private float stationaryTime;
    private float stationaryPassedTime;
    private float regconitionTime;
    public EnemyStationaryState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager, float stationaryTime) : base(stateMachine, enemyStateManager)
    {
        this.stationaryTime = stationaryTime;
    }

    public override void Enter()
    {
        stationaryPassedTime = stationaryTime;
        regconitionTime = enemyStateManager.recognitionTime;
        enemyStateManager.enemyAnimator.SetBool("Stationary", true);
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemyStateManager.enemyAnimator.SetBool("Stationary", false);
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if(stationaryPassedTime > 0)
        {
            stationaryPassedTime -= Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(enemyStateManager.enemyPatrolState);
        }

        if (enemyStateManager.isDetectingPlayer)
        {
            if (regconitionTime > 0)
            {
                regconitionTime -= Time.deltaTime;
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
}
