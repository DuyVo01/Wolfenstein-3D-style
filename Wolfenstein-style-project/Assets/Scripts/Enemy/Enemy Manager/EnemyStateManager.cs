using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateManager : MonoBehaviour
{
    [Header("Navmesh Agent")]
    public NavMeshAgent agent;
    [Header("Animator")]
    public Animator enemyAnimator;

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
    public Vector3 lastKnownTargetPosition;

    [Space(10)]
    [Header("Enemy Searching State")]
    public float searchingTime;

    [Space(10)]
    [Header("Enemy Patrol State")]
    public EnemyPatrolPointsData patrolPointsData;
    public float recognitionTime;
    public float patrolLookingTime;


    [Space(10)]
    [SerializeField] float rotatonSpeed;
    private EnemyStateMachine _stateMachine;

    public Rigidbody enemyRB;
    private Detection_Test _detection;

    //States
    public EnemyShootingState enemyShootingState;
    public EnemySearchingState enemySearchingState;
    public EnemyPatrolState enemyPatrolState;

    public bool isDetectingPlayer;
    public Vector3 targetPosition;

    private void Awake()
    {
        enemyRB = GetComponent<Rigidbody>();
        _detection = GetComponent<Detection_Test>();
        agent = GetComponent<NavMeshAgent>();
        patrolPointsData = GetComponent<EnemyPatrolPointsData>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _stateMachine = new EnemyStateMachine();

        enemyShootingState = new EnemyShootingState(_stateMachine, this);
        enemySearchingState = new EnemySearchingState(_stateMachine, this, searchingTime);
        enemyPatrolState = new EnemyPatrolState(_stateMachine, this, patrolPointsData.patrolPoints);

        _stateMachine.Initialize(enemyPatrolState);
    }

    // Update is called once per frame
    void Update()
    {
        isDetectingPlayer = _detection.isDetectingPlayer;
        targetPosition = _detection.targetPlayerPosition;
        _stateMachine.LogicalUpdate();
        
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicalUpdate();
    }

    public void RotateToTarget()
    {
        Vector3 targetDirection = _detection.targetPlayerPosition - transform.position;
        Quaternion lookToTarget = Quaternion.LookRotation(targetDirection);

        enemyRB.MoveRotation(Quaternion.Lerp(enemyRB.rotation, lookToTarget, Time.deltaTime * rotatonSpeed));
    }

    public Vector3 EnemyVelocity()
    {
        return enemyRB.velocity;
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
