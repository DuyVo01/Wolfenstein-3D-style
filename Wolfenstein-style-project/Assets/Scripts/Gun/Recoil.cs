using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Recoil : MonoBehaviour
{
    public CinemachineVirtualCamera _virtualCamera;
    public CinemachinePOV _virtualPOV;

    [Header("Recoil Properties")]
    [SerializeField] float _recoilX;
    [SerializeField] float _recoilY;

    [Header("Recoil Settings")]
    [SerializeField] float _recoilSnappiness;
    [SerializeField] float _recoilReturnSpeed;

    private float _totalRecoilAmountX;
    private float _totalRecoilAmountY;


    private void Awake()
    {
        _virtualPOV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    public void RecoilShoot()
    {
        float recoilX = _recoilX * Time.deltaTime;
        float recoilY = _recoilY * Time.deltaTime;
        recoilX = Random.Range(-recoilX, recoilX);
        recoilY = Random.Range(0, recoilY);
        _virtualPOV.m_HorizontalAxis.Value = Mathf.Lerp(_virtualPOV.m_HorizontalAxis.Value, _virtualPOV.m_HorizontalAxis.Value - recoilX, _recoilSnappiness);
        _virtualPOV.m_VerticalAxis.Value = Mathf.Lerp(_virtualPOV.m_VerticalAxis.Value, _virtualPOV.m_VerticalAxis.Value - recoilY, _recoilSnappiness);
        _totalRecoilAmountX = Mathf.Lerp(_totalRecoilAmountX, _totalRecoilAmountX + recoilX, _recoilSnappiness);
        _totalRecoilAmountY = Mathf.Lerp(_totalRecoilAmountY, _totalRecoilAmountY + recoilY, _recoilSnappiness);
    }

    public void ResetRecoil()
    {
        if (_totalRecoilAmountY > 0)
        {
            _virtualPOV.m_VerticalAxis.Value = Mathf.Lerp(_virtualPOV.m_VerticalAxis.Value, _virtualPOV.m_VerticalAxis.Value + _totalRecoilAmountY, Time.deltaTime * _recoilReturnSpeed);
            _totalRecoilAmountY = Mathf.Lerp(_totalRecoilAmountY, 0, Time.deltaTime * _recoilReturnSpeed);
        }
        if(_totalRecoilAmountX > 0)
        {
            _virtualPOV.m_HorizontalAxis.Value = Mathf.Lerp(_virtualPOV.m_HorizontalAxis.Value, _virtualPOV.m_HorizontalAxis.Value + _totalRecoilAmountX, Time.deltaTime * _recoilReturnSpeed);
            _totalRecoilAmountX = Mathf.Lerp(_totalRecoilAmountX, 0, Time.deltaTime * _recoilReturnSpeed);
        }
    }

}
