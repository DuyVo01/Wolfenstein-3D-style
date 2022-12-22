using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    Vector3 _previousPosition;
    [SerializeField] LayerMask _layerToCollide;
    [SerializeField] ParticleSystem bulletImpact;

    TrailRenderer _trailRenderer;
    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        Ray ray = new Ray(_previousPosition, (transform.position - _previousPosition).normalized);

        if (Physics.Raycast(ray, out RaycastHit hit, Vector3.Distance(_previousPosition, transform.position), _layerToCollide))
        {
            gameObject.SetActive(false);
            Instantiate(bulletImpact, hit.point, bulletImpact.transform.rotation);
        }

        _previousPosition = transform.position;
    }

    private void FixedUpdate()
    {
        
    }

    private void OnEnable()
    {
        _previousPosition = transform.position;
        _trailRenderer.Clear();
    }


}
