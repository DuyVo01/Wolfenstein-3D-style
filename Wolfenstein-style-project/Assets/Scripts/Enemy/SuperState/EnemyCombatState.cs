using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombatState : BaseEnemyState
{
    public EnemyCombatState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager) : base(stateMachine, enemyStateManager)
    {
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
