using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : BaseEnemyState
{
    private float recognitionStartTime;
    private float recognitionTime;

    public EnemyIdleState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager) : base(stateMachine, enemyStateManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
        recognitionStartTime = enemyStateManager.recognitionTime;
        recognitionTime = recognitionStartTime;
        Debug.Log("Enemy Idle Enter");
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
            if(recognitionTime > 0)
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
}
