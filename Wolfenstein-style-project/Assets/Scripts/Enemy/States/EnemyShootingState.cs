using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingState : BaseEnemyState
{

    private List<GameObject> _enemyBullets = new List<GameObject>();
    private GameObject _enemyBulletToShoot;
    private float _enemyLastFireTime;

    private float _enemyShootingTime;
    private float _enemyShootingDelayed;
    public EnemyShootingState(EnemyStateMachine stateMachine, EnemyStateManager enemyStateManager) : base(stateMachine, enemyStateManager)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemyStateManager.agent.isStopped = true;
        enemyStateManager.agent.enabled = false;
        enemyStateManager.enemyRB.isKinematic = false;
        Debug.Log("Enemy Shoot Enter");
        _enemyLastFireTime = 0;
        _enemyShootingTime = enemyStateManager.shootingStartTime;

        if(_enemyBullets.Count == 0)
        {
            _enemyBullets = enemyStateManager.EnemyBullets();
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyStateManager.lastKnownTargetPosition = enemyStateManager.targetPosition;
        enemyStateManager.agent.enabled = true;
        enemyStateManager.enemyRB.isKinematic = true;
        enemyStateManager.agent.isStopped = false;
    }

    public override void LogicalUpdate()
    {
        base.LogicalUpdate();
        if (!enemyStateManager.isDetectingPlayer)
        {
            stateMachine.ChangeState(enemyStateManager.enemySearchingState);
        }
        else
        {
            enemyStateManager.RotateToTarget();
        }
        
    }

    public override void PhysicalUpdate()
    {
        base.PhysicalUpdate();
        if(_enemyShootingTime > 0.01f)
        {
            if (Time.time > _enemyLastFireTime + enemyStateManager.enemyFireRate)
            {
                enemyStateManager.enemyAnimator.SetBool("Shooting", true);
                ShootingBullet();
                _enemyLastFireTime = Time.time;
            }
            else
            {
                enemyStateManager.enemyAnimator.SetBool("Shooting", false);
            }
            _enemyShootingTime -= Time.deltaTime;
        }
        else
        {
            if (_enemyShootingDelayed > 0.01f)
            {
                _enemyShootingDelayed -= Time.deltaTime;
            }
            else
            {
                _enemyShootingTime = enemyStateManager.shootingStartTime;
                _enemyShootingDelayed = enemyStateManager.shootingDelayedTime;
            }
        }  
    }

    private void ShootingBullet()
    {
        _enemyBulletToShoot = EnemyBulletToShoot();

        if(_enemyBulletToShoot != null)
        {
            Rigidbody bulletRB = _enemyBulletToShoot.GetComponent<Rigidbody>();
            bulletRB.velocity = Vector3.zero;
            _enemyBulletToShoot.transform.position = enemyStateManager.bulletSocket.transform.position;
            _enemyBulletToShoot.transform.rotation = enemyStateManager.bulletSocket.transform.rotation;
            _enemyBulletToShoot.SetActive(true);
            bulletRB.AddForce(Time.deltaTime * enemyStateManager.enemyBulletSpeed * BulletDirection() - bulletRB.velocity, ForceMode.VelocityChange);
            

        }
    }

    private GameObject EnemyBulletToShoot()
    {
        foreach(GameObject bullet in _enemyBullets)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }
        return null;
    }

    private Vector3 BulletDirection()
    {
        //Vector3 direction = enemyStateManager.targetPosition - enemyStateManager.transform.position;
        Vector3 direction = enemyStateManager.transform.forward;
        direction.x += Random.Range(-enemyStateManager.enemyBulletSpread, enemyStateManager.enemyBulletSpread);
        direction.y += Random.Range(-enemyStateManager.enemyBulletSpread, enemyStateManager.enemyBulletSpread);
        direction.Normalize();

        return direction;
    }
}