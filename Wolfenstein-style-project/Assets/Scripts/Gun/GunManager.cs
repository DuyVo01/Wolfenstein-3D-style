using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    Animator gunAnimator;

    [SerializeField] float angleToRotate;
    [SerializeField] float returnSpeed;

    private Vector3 originalRotation;

    private Vector3 _gunShootTargetRotation;
    private Vector3 _gunShootCurrentRotation;

    private void Awake()
    {
        gunAnimator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        Shooting.ShootingBullet += ShootingAnimation;
        Shooting.ShootingBulletDone += FinishShootingAnimation;
    }

    private void OnDisable()
    {
        Shooting.ShootingBullet -= ShootingAnimation;
        Shooting.ShootingBulletDone -= FinishShootingAnimation;
    }

    private void Start()
    {
        originalRotation = transform.localRotation.eulerAngles;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _gunShootTargetRotation = Vector3.Lerp(_gunShootTargetRotation, originalRotation, returnSpeed * Time.deltaTime);
        _gunShootCurrentRotation = Vector3.Slerp(_gunShootCurrentRotation, _gunShootTargetRotation, returnSpeed * Time.deltaTime);
        transform.localRotation = Quaternion.Euler(_gunShootTargetRotation);
    }

    public void ShootingAnimation()
    {
        _gunShootTargetRotation -= new Vector3(angleToRotate, 0, 0); 
        gunAnimator.SetBool("fire", true);
    }

    public void FinishShootingAnimation()
    {
        gunAnimator.SetBool("fire", false);
    }
}
