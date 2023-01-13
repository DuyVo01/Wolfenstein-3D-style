using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAirState : BaseState
{
    private bool _isGround;
    public InAirState(StateMachine stateMachine, PlayerStateManager playerStateManager) : base(stateMachine, playerStateManager)
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
        _isGround = playerStateManager.isGround;

        if(_isGround && playerStateManager.playerRB.velocity.y < 0.01f)
        {
            stateMachine.ChangeState(playerStateManager.idleState);
        }
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();

        if (!_isGround)
        {
            playerStateManager.playerRB.AddForce(Physics.gravity.y * playerStateManager.fallMultiplier * Time.deltaTime * Vector3.up, ForceMode.VelocityChange);
        }
    }
}
