using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStationaryState : EnemyNonCombatState
{

    private float stationaryTime;
    private float stationaryPassedTime;
    
    public EnemyStationaryState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager, float stationaryTime) : base(stateMachine, enemyStateManager)
    {
        this.stationaryTime = stationaryTime;
    }

    public override void Enter()
    {
        stationaryPassedTime = stationaryTime;
        
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

        
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }
}
