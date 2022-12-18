using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSocket;
    [SerializeField] GameObject bulletPool;

    [Header("Gun properties")]
    [SerializeField] int maxBulletHodingCapacity;
    [SerializeField] int totalNumberOfBullet;
    [SerializeField] int gunMaxCapacity;
    [SerializeField] int currentNumberOfBullet;

    [Header("Fire rate")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    private float lastFire = 0;

    [Header("Aiming Target")]
    [SerializeField] float swayRadius;

    //Raycast
    private RaycastHit _rayHit;
    private Ray _ray;


    private bool _isShooting;
    private GameObject _bulletToShoot;


    private PlayerStateManager _playerStateManager;
    private List<GameObject> bullets = new List<GameObject>();

    private void Awake()
    {
        _playerStateManager = GetComponent<PlayerStateManager>();
    }

    private void Start()
    {
        for (int i = 0; i < gunMaxCapacity; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletPool.transform);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _isShooting = _playerStateManager.playerActionInputHandler.GetShootInput();
        _bulletToShoot = BulletToShoot();
    }

    private void FixedUpdate()
    {
        if (_isShooting && Time.time > lastFire + fireRate)
        {
            HitPoint();
            ShootingBullets();
            lastFire = Time.time;
        }
    }

    public void HitPoint()
    {
        Vector2 mousePosition = _playerStateManager.playerMovementInputHandler.GetMousePosition();

        Vector2 rayToCast = mousePosition;

        rayToCast.x += Random.Range(-swayRadius, swayRadius);
        rayToCast.y += Random.Range(-swayRadius, swayRadius);

        if (Vector3.Distance(rayToCast, mousePosition) > swayRadius)
        {
            rayToCast = mousePosition;
        }

        _ray = _playerStateManager.cameraMain.ScreenPointToRay(rayToCast);

        if (Physics.Raycast(_ray, out _rayHit, float.MaxValue, _playerStateManager.layerMask))
        {
            //aimingTarget.position = Vector3.Lerp(aimingTarget.position, _rayHit.point, Time.deltaTime * 50f);
            _playerStateManager.aimingTarget.position = _rayHit.point;
        }
    }

    private void ShootingBullets()
    {

        if(_bulletToShoot != null)
        {
            _bulletToShoot.transform.position = bulletSocket.transform.position;
            _bulletToShoot.transform.rotation = bulletSocket.transform.rotation;
            _bulletToShoot.SetActive(true);
            _bulletToShoot.GetComponent<Rigidbody>().AddForce(Time.deltaTime * bulletSpeed * (_playerStateManager.aimingTarget.position - _bulletToShoot.transform.position).normalized - _bulletToShoot.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);
        }
        

    }

    private GameObject BulletToShoot() 
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }
        return null;
    }
}
