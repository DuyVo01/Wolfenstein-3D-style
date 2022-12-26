using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSocket;
    [SerializeField] GameObject bulletPool;
    [SerializeField] int EnemyBulletCapacity;

    [Header("Shooting properties")]
    [SerializeField] float shootingStartTime;
    [SerializeField] float shootingDelayedTime;

    private float shootingPassedTime;
    private float shootingDelayedPassedTime;

    private List<GameObject> enemyBullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
