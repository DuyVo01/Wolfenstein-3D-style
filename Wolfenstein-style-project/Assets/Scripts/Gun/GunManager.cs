using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [Header("Gun Data")]
    public int ammoCapacity;
    public int ammoHoldingCapacity;
    public int currentAmmo;
    public int currentAmmoHolding;
    public float fireRate;
    public ParticleSystem muzzleFlash;
    public GameObject bulletPrefab;
    public GameObject bulletBarrel;
    public GameObject bulletPool;
    public Recoil recoil;

    [Space(10)]

    Animator gunAnimator;

    [SerializeField] float angleToRotate;
    [SerializeField] float returnSpeed;
    [SerializeField] float returnPosSpeed;
    [SerializeField] float moveUpY;
    [SerializeField] float kickBackZ;

    public Transform weaponHolder;
    public AudioClip[] gunAudio;

    private Vector3 originalRotation;
    private Vector3 originalHolderPosition;

    private Vector3 _gunShootTargetRotation;
    private Vector3 _gunShootCurrentRotation;

    private Vector3 _gunShootTargetPosition;
    private Vector3 _gunShootCurrentPosition;

    public List<GameObject> bullets = new List<GameObject>();

    private void Awake()
    {
        gunAnimator = GetComponentInChildren<Animator>();
        recoil = GetComponent<Recoil>();
    }

    private void OnEnable()
    {
        Shooting.ShootingBullet += ShootingAnimation;
        Shooting.ShootingBulletDone += FinishShootingAnimation;
        Shooting.ShootingEmpty += OutOfAmmo;
    }

    private void OnDisable()
    {
        Shooting.ShootingBullet -= ShootingAnimation;
        Shooting.ShootingBulletDone -= FinishShootingAnimation;
        Shooting.ShootingEmpty -= OutOfAmmo;
    }

    private void Start()
    {
        originalRotation = weaponHolder.transform.localRotation.eulerAngles;
        originalHolderPosition = weaponHolder.transform.localPosition;

        currentAmmo = ammoCapacity;
        currentAmmoHolding = 0;
        for (int i = 0; i < ammoHoldingCapacity; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletBarrel.transform.position, bulletPrefab.transform.rotation, bulletPool.transform);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }

    private void Update()
    {
        _gunShootTargetPosition = Vector3.Lerp(_gunShootTargetPosition, originalHolderPosition, returnPosSpeed * Time.deltaTime);
        _gunShootCurrentPosition = Vector3.Lerp(_gunShootCurrentPosition, _gunShootTargetPosition, returnPosSpeed * Time.deltaTime);
        weaponHolder.transform.localPosition = _gunShootTargetPosition;

        _gunShootTargetRotation = Vector3.Lerp(_gunShootTargetRotation, originalRotation, returnSpeed * Time.deltaTime);
        _gunShootCurrentRotation = Vector3.Slerp(_gunShootCurrentRotation, _gunShootTargetRotation, returnSpeed * Time.deltaTime);
        weaponHolder.transform.localRotation = Quaternion.Euler(_gunShootTargetRotation);
    }

    public void ShootingAnimation()
    {
        if (currentAmmo > 0)
        {
            _gunShootTargetRotation -= new Vector3(angleToRotate, 0, 0);
            _gunShootTargetPosition -= new Vector3(0, moveUpY / 10, kickBackZ / 10);
            //gunAnimator.SetBool("fire", true);
            gunAnimator.SetTrigger("firing");
            currentAmmo--;
            GunAudioManager.PlayAudio(gunAudio[0]);
        }
    }

    public void OutOfAmmo()
    {
        GunAudioManager.PlayAudio(gunAudio[1]);
    }

    public void FinishShootingAnimation()
    {
        //gunAnimator.SetBool("fire", false);
    }

    
}
