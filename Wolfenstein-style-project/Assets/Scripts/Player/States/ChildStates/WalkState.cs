using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : GroundState
{
    public WalkState(StateMachine stateMachine, PlayerStateManager playerStateManager) : base(stateMachine, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Walk State");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if(_movementDirection == Vector3.zero)
        {
            stateMachine.ChangeState(playerStateManager.idleState);
        }
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
        WalkMove(_movementDirection);
    }

    public void WalkMove(Vector3 movementDirection)
    {
        Vector3 playerHorizontalVelocity = playerStateManager.playerRB.velocity;

        playerHorizontalVelocity.y = 0f;

        Vector3 finalSpeed = playerStateManager.walkMoveSpeed * movementDirection;

        Vector3 speedDif = finalSpeed - playerHorizontalVelocity;

        float accelRate = 5;

        Vector3 movement = accelRate * speedDif;

        playerStateManager.playerRB.AddForce(movement, ForceMode.Acceleration);
    }
}
