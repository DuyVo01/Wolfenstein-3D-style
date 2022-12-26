using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    [Header("Enemy Idle State")]
    public float recognitionTime;

    [Space(10)] 
    [Header("Enemy Shooting State")]
    public GameObject bulletPrefab;
    public GameObject bulletSocket;
    public GameObject bulletPool;
    public float enemyBulletSpeed;
    public float enemyFireRate;
    public float enemyBulletSpread;
    public int enemyBulletCapacity;
    public float shootingStartTime;
    public float shootingDelayedTime;


    [Space(10)]
    [SerializeField] float rotatonSpeed;
    private EnemyStateMachine _stateMachine;

    private Rigidbody _enemyRB;
    private Detection_Test _detection;

    public EnemyIdleState enemyIdleState;
    public EnemyShootingState enemyShootingState;

    public bool isDetectingPlayer;
    public Vector3 targetPlayer;

    private void Awake()
    {
        _enemyRB = GetComponent<Rigidbody>();
        _detection = GetComponent<Detection_Test>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new EnemyStateMachine();

        enemyIdleState = new EnemyIdleState(_stateMachine, this);
        enemyShootingState = new EnemyShootingState(_stateMachine, this);

        _stateMachine.Initialize(enemyIdleState);
    }

    // Update is called once per frame
    void Update()
    {
        isDetectingPlayer = _detection.isDetectingPlayer;
        targetPlayer = _detection.targetPlayer;
        _stateMachine.LogicalUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicalUpdate();
    }

    public void RotateToTarget()
    {
        Vector3 targetDirection = _detection.targetPlayer - transform.position;
        Quaternion lookToTarget = Quaternion.LookRotation(targetDirection);

        _enemyRB.rotation = Quaternion.Lerp(_enemyRB.rotation, lookToTarget, Time.deltaTime * rotatonSpeed);
    }

    public Vector3 EnemyVelocity()
    {
        return _enemyRB.velocity;
    }

    public List<GameObject> EnemyBullets()
    {
        List<GameObject> enemyBullets = new List<GameObject>();
        for (int i = 0; i < enemyBulletCapacity; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSocket.transform.position, bulletPrefab.transform.rotation, bulletPool.transform);
            bullet.SetActive(false);
            enemyBullets.Add(bullet);
        }
        return enemyBullets;
    }
}
