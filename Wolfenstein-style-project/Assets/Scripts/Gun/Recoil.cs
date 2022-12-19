using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Recoil : MonoBehaviour
{
    public CinemachineVirtualCamera _virtualCamera;
    public CinemachinePOV _virtualPOV;

    public PlayerActionInputHandler playerActionInputHandler;

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
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        _virtualPOV = _virtualCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void RecoilShoot()
    {
        float recoilX = _recoilX * Time.deltaTime * _recoilSnappiness;
        float recoilY = _recoilY * Time.deltaTime * _recoilSnappiness;

        recoilX = Random.Range(-recoilX, recoilX);
        recoilY = Random.Range(0, recoilY);

        _virtualPOV.m_HorizontalAxis.Value -= recoilX;
        _virtualPOV.m_VerticalAxis.Value -= recoilY;

        _totalRecoilAmountX += recoilX;
        _totalRecoilAmountY += recoilY;
    }

    public void ResetRecoil()
    {
        if (_totalRecoilAmountY > 0)
        {
            _virtualPOV.m_VerticalAxis.Value += _recoilY * Time.deltaTime * _recoilReturnSpeed;
            _totalRecoilAmountY -= _recoilY * Time.deltaTime * _recoilReturnSpeed;
        }

        if(_totalRecoilAmountX > 0)
        {
            _virtualPOV.m_HorizontalAxis.Value += _recoilX * Time.deltaTime * _recoilReturnSpeed;
            _totalRecoilAmountX -= _recoilX * Time.deltaTime * _recoilReturnSpeed;
        }
    }

}
