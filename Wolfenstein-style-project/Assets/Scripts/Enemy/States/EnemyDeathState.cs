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
        PlayerStatus.AddScore(20);
        enemyStateManager.enemyStatus.isDeath = true;
        enemyStateManager.agent.Stop();
        LevelRecord.Instance.UpdateEnemyKilled();
        enemyStateManager.GetComponent<Collider>().enabled = false;
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
