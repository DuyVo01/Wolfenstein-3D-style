using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Recoil : MonoBehaviour
{
    [Header("Cinemachine Ref")]
    public CinemachineImpulseSource cameraShake;
    public CinemachineVirtualCamera _virtualCamera;
    public CinemachinePOV _virtualPOV;

    [Header("Recoil Properties")]
    public float recoilDuration;
    public float recoilReturnSpeed;
    [SerializeField] float _recoilX;
    [SerializeField] float _recoilY;

    float _totalRecoilAmountY;
    float _totalRecoilAmountX;

    private float time;

    private void Awake()
    {
        _virtualPOV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if(time > 0)
        {
            _virtualPOV.m_VerticalAxis.Value -= (_recoilY * Time.deltaTime) / recoilDuration;
            _virtualPOV.m_HorizontalAxis.Value -= (Random.Range(-_recoilX, _recoilX) * Time.deltaTime) / recoilDuration;
            _totalRecoilAmountY += (_recoilY * Time.deltaTime) / recoilDuration;
            time -= Time.deltaTime;
        }
    }

    //public void RecoilShoot()
    //{
    //    //float recoilX = _recoilX * Time.deltaTime;
    //    //float recoilY = _recoilY * Time.deltaTime;
    //    //recoilX = Random.Range(-recoilX, recoilX);
    //    //recoilY = Random.Range(0, recoilY);
    //    //_virtualPOV.m_HorizontalAxis.Value = Mathf.Lerp(_virtualPOV.m_HorizontalAxis.Value, _virtualPOV.m_HorizontalAxis.Value - recoilX, _recoilSnappiness);
    //    //_virtualPOV.m_VerticalAxis.Value = Mathf.Lerp(_virtualPOV.m_VerticalAxis.Value, _virtualPOV.m_VerticalAxis.Value - recoilY, _recoilSnappiness);
    //    //_totalRecoilAmountX = Mathf.Lerp(_totalRecoilAmountX, _totalRecoilAmountX + recoilX, _recoilSnappiness);
    //    //_totalRecoilAmountY = Mathf.Lerp(_totalRecoilAmountY, _totalRecoilAmountY + recoilY, _recoilSnappiness);
        
    //}

    public void RecoilShoot()
    {
        time = recoilDuration;
        RecoilOnCameraShake();
    }

    public void ResetRecoil()
    {
        //if (_totalRecoilAmountY > 0)
        //{
        //    _virtualPOV.m_VerticalAxis.Value = Mathf.Lerp(_virtualPOV.m_VerticalAxis.Value, _virtualPOV.m_VerticalAxis.Value + _totalRecoilAmountY, Time.deltaTime * recoilReturnSpeed);
        //    _totalRecoilAmountY = Mathf.Lerp(_totalRecoilAmountY, 0, Time.deltaTime * recoilReturnSpeed);
        //}
        //if (_totalRecoilAmountX > 0)
        //{
        //    _virtualPOV.m_HorizontalAxis.Value = Mathf.Lerp(_virtualPOV.m_HorizontalAxis.Value, _virtualPOV.m_HorizontalAxis.Value + _totalRecoilAmountX, Time.deltaTime * recoilReturnSpeed);
        //    _totalRecoilAmountX = Mathf.Lerp(_totalRecoilAmountX, 0, Time.deltaTime * recoilReturnSpeed);
        //}
    }

    public void RecoilOnCameraShake()
    {
        cameraShake.GenerateImpulse(Camera.main.transform.forward);
    }

}
