using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundState : BaseState
{
    protected Vector3 _movementDirection;
    public GroundState(StateMachine stateMachine, PlayerStateManager playerStateManager) : base(stateMachine, playerStateManager)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        _movementDirection = playerStateManager.GetMovementDirection();
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
    }
}
