using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : GroundState
{
    public RunState(StateMachine stateMachine, PlayerStateManager playerStateManager) : base(stateMachine, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Run State");
    }

    public override void Exit()
    {
        base.Exit();
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
