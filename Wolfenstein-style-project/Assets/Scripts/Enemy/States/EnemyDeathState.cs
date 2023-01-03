using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : BaseEnemyState
{
    public EnemyDeathState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager) : base(stateMachine, enemyStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemyStateManager.enemyAnimator.SetBool("Death", true);
        enemyStateManager.agent.Stop();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }
}
