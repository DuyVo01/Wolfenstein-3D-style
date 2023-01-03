using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    [Header("Cinemachine Impulse")]
    [SerializeField] CinemachineImpulseSource cameraImpulseSource;

    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletSocket;
    [SerializeField] GameObject bulletPool;

    [Header("Particle Muzzle Flash")]
    [SerializeField] ParticleSystem muzzleFlashPaticle;

    [Header("Gun properties")]
    [SerializeField] int maxBulletHodingCapacity;
    [SerializeField] int totalNumberOfBullet;
    [SerializeField] int gunMaxCapacity;
    [SerializeField] int currentNumberOfBullet;

    [Header("Fire rate")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    [SerializeField] private float recoilCooldown = 0;
    private float _lastFire = 0;

    [Header("Aiming Target")]
    [SerializeField] float swayRadius;

    [Header("Recoil")]
    public Recoil _recoil;

    

    //Raycast
    private RaycastHit _rayHit;
    private Ray _ray;

    //Shoot variables
    private bool _isShooting;
    private GameObject _bulletToShoot;

    public static event Action ShootingBullet;
    public static event Action ShootingBulletDone;

    private PlayerStateManager _playerStateManager;
    private List<GameObject> bullets = new List<GameObject>();

    private void Awake()
    {
        _playerStateManager = GetComponent<PlayerStateManager>();
        cameraImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Start()
    {
        for (int i = 0; i < gunMaxCapacity; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSocket.transform.position, bulletPrefab.transform.rotation, bulletPool.transform);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    // Update is called once per frame
    void Update()
    {
        _isShooting = _playerStateManager.playerActionInputHandler.GetShootInput();
    }

    private void FixedUpdate()
    {

        if (_isShooting && Time.time > _lastFire + fireRate)
        {
            ShootingBullet?.Invoke();
            HitPoint();
            ShootingBullets();

            _lastFire = Time.time;
        }
        else
        {
            ShootingBulletDone?.Invoke();
        }

        if (Time.time > _lastFire + recoilCooldown)
        {

            _recoil.ResetRecoil();
        }
    }

    public void HitPoint()
    {
        Vector2 mousePosition = _playerStateManager.playerMovementInputHandler.GetMousePosition();

        Vector2 rayToCast = mousePosition;

        rayToCast.x += UnityEngine.Random.Range(-swayRadius, swayRadius);
        rayToCast.y += UnityEngine.Random.Range(-swayRadius, swayRadius);

        if (Vector3.Distance(rayToCast, mousePosition) > swayRadius)
        {
            rayToCast = mousePosition;
        }

        _ray = _playerStateManager.cameraMain.ScreenPointToRay(rayToCast);

        if (Physics.Raycast(_ray, out _rayHit, float.MaxValue, _playerStateManager.layerMask))
        {
            _playerStateManager.aimingTarget.position = _rayHit.point;
        }
    }

    private void ShootingBullets()
    {
        
        RecoilOnCameraShake();

        _recoil.RecoilShoot();

        _bulletToShoot = BulletToShoot();

        if (_bulletToShoot != null)
        {
            
            _bulletToShoot.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _bulletToShoot.transform.position = bulletSocket.transform.position;
            _bulletToShoot.transform.rotation = bulletSocket.transform.rotation;
            _bulletToShoot.SetActive(true);
            _bulletToShoot.GetComponent<Rigidbody>().AddForce(Time.deltaTime * bulletSpeed * (_playerStateManager.aimingTarget.position - _bulletToShoot.transform.position).normalized - _bulletToShoot.GetComponent<Rigidbody>().velocity, ForceMode.VelocityChange);

            muzzleFlashPaticle.Play();
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

    private void RecoilOnCameraShake()
    {
        cameraImpulseSource.GenerateImpulse(_playerStateManager.cameraMain.transform.forward);
    }
}
