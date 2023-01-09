using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : GroundState
{
    Coroutine coroutine = null;
    public WalkState(StateMachine stateMachine, PlayerStateManager playerStateManager) : base(stateMachine, playerStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        ChangeCoroutine(CinemachineNoiseEnable());
        Debug.Log("Enter Walk State"); 
    }

    public override void Exit()
    {
        base.Exit();
        ChangeCoroutine(CinemachineNoiseDisable());
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        
        if (_movementDirection == Vector3.zero)
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

    private IEnumerator CinemachineNoiseEnable()
    {
        while (playerStateManager.cinemachineNoise.m_AmplitudeGain < 1 && playerStateManager.cinemachineNoise.m_FrequencyGain < 1)
        {
            yield return new WaitForEndOfFrame();
            playerStateManager.cinemachineNoise.m_AmplitudeGain += Time.deltaTime * 10;
            playerStateManager.cinemachineNoise.m_FrequencyGain += Time.deltaTime * 10;
        }
    }

    private IEnumerator CinemachineNoiseDisable()
    {
        while(playerStateManager.cinemachineNoise.m_AmplitudeGain > 0 && playerStateManager.cinemachineNoise.m_FrequencyGain > 0)
        {
            yield return new WaitForEndOfFrame();
            playerStateManager.cinemachineNoise.m_AmplitudeGain -= Time.deltaTime * 10;
            playerStateManager.cinemachineNoise.m_FrequencyGain -= Time.deltaTime * 10;
        }
    }

    private void ChangeCoroutine(IEnumerator newCoroutine)
    {
        if(coroutine != null)
        {
            playerStateManager.StopCoroutine(coroutine);
        }
        coroutine = playerStateManager.StartCoroutine(newCoroutine);
    }
}
