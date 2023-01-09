using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    private GameObject bulletPrefab;
    private GameObject bulletBarrel;
    private GameObject bulletPool;
    private ParticleSystem muzzleFlashPaticle;

    private int ammoHoldingCapacity;
    private int currentAmmoHolding;
    private int ammoCapacity;
    private int currentAmmo;

    [Header("Fire rate")]
    [SerializeField] float bulletSpeed;
    [SerializeField] float fireRate;
    [SerializeField] private float recoilCooldown = 0;
    private float _lastFire = 0;

    [Header("Aiming Target")]
    [SerializeField] float swayRadius;

    [Header("Recoil")]
    public Recoil _recoil;

    [Header("Current Gun Equiped")]
    GunManager currentGun;

    //Raycast
    private RaycastHit _rayHit;
    private Ray _ray;

    //Shoot variables
    private bool _isShooting;
    private bool _canShoot;
    private GameObject _bulletToShoot;

    public static event Action ShootingBullet;
    public static event Action ShootingBulletDone;
    public static event Action ShootingEmpty;

    private PlayerStateManager _playerStateManager;
    private List<GameObject> bullets = new List<GameObject>();

    private void Awake()
    {
        _playerStateManager = GetComponent<PlayerStateManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStatus.isWeaponEquipped)
        {
            if(currentGun != PlayerStatus.currentWeaponEquipped)
            {
                currentGun = PlayerStatus.currentWeaponEquipped.GetComponent<GunManager>();
            }
            _isShooting = _playerStateManager.playerActionInputHandler.GetShootInput();
            GetGunData();
            if(currentAmmo > 0)
            {
                
                _canShoot = true;
            }
            else
            {
                _canShoot = false;
                
            }
        }
        else
        {
            _canShoot = false;
        }
    }

    private void FixedUpdate()
    {
        if (_canShoot)
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
        else
        {
            if (_isShooting && Time.time > _lastFire + fireRate)
            {
                ShootingEmpty?.Invoke();
                _lastFire = Time.time;
            }
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
        _recoil.RecoilShoot();
        _bulletToShoot = BulletToShoot();
        if (_bulletToShoot != null)
        {
            _bulletToShoot.GetComponent<Rigidbody>().velocity = Vector3.zero;
            _bulletToShoot.transform.position = bulletBarrel.transform.position;
            _bulletToShoot.transform.rotation = bulletBarrel.transform.rotation;
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

    private void GetGunData()
    {
        ammoHoldingCapacity = currentGun.ammoHoldingCapacity;
        ammoCapacity = currentGun.ammoCapacity;
        currentAmmoHolding = currentGun.currentAmmoHolding;
        currentAmmo = currentGun.currentAmmo;
        bulletPool = currentGun.bulletPool;
        bulletPrefab = currentGun.bulletPrefab;
        bulletBarrel = currentGun.bulletBarrel;
        bullets = currentGun.bullets;
        fireRate = currentGun.fireRate;
        muzzleFlashPaticle = currentGun.muzzleFlash;
    }
}
