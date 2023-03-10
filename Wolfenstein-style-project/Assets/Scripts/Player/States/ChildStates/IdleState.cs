using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundState
{
    public IdleState(StateMachine stateMachine, PlayerStateManager playerStateManager) : base(stateMachine, playerStateManager)
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
        if(_movementDirection != Vector3.zero)
        {
            stateMachine.ChangeState(playerStateManager.walkState);
        }


        playerStateManager.playerAnimator.SetBool("GunHolding", PlayerStatus.currentWeaponEquipped);
        
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
        DecelerateStop();
    }

    public void DecelerateStop()
    {
        playerStateManager.playerRB.AddForce(new Vector3(-playerStateManager.playerRB.velocity.x * 11, 0, -playerStateManager.playerRB.velocity.z * 11), ForceMode.Acceleration);
    }
}
