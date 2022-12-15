using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState 
{
    public StateMachine stateMachine;
    public PlayerStateManager playerStateManager;

    public BaseState(StateMachine stateMachine, PlayerStateManager playerStateManager)
    {
        this.stateMachine = stateMachine;
        this.playerStateManager = playerStateManager;
    }

    public virtual void Enter()
    {

    }

    public virtual void LogicalUpdate()
    {

    }

    public virtual void PhysicalUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}
