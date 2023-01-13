using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNonCombatState : BaseEnemyState
{
    private float regconitionTime;
    protected bool inCombat;
    public EnemyNonCombatState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager) : base(stateMachine, enemyStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        regconitionTime = enemyStateManager.recognitionTime;
        inCombat = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if (enemyStateManager.isDetectingPlayer)
        {
            if (regconitionTime > 0)
            {
                regconitionTime -= Time.deltaTime;
            }
            else
            {
                inCombat = true;
                stateMachine.ChangeState(enemyStateManager.enemyShootingState);
            }
        } 
        else if (enemyStateManager.enemyStatus.isTakingDamage)
        {
            stateMachine.ChangeState(enemyStateManager.enemySearchingState);
        }
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    
}
