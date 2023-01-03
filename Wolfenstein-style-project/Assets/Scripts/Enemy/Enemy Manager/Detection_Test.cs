using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Test : MonoBehaviour
{
    [SerializeField] private float _inRangeRadius;
    [SerializeField] [Range(0, 360)] private float _inRangeAngle; 
    [SerializeField] private LayerMask _layerToDetect;
    [SerializeField] private LayerMask _RaycastCheckLayers;
    [SerializeField] private float _behindDetectionRange;
    [SerializeField] private Transform aimPosition;
    [SerializeField] private float maxVerticleDetection;

    bool _isTargetInRange;
    public bool isDetectingPlayer;

    Collider[] _inRangeColliders;
    public Vector3 targetPlayerPosition;
    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerInRange();
    }

    private void CheckIfPlayerInRange()
    {
        _inRangeColliders = Physics.OverlapSphere(aimPosition.position, _inRangeRadius, _layerToDetect);

        if(_inRangeColliders.Length != 0)
        {

            for(int i = 0; i < _inRangeColliders.Length; i++)
            {
                if (_inRangeColliders[i].CompareTag("PlayerHurtbox"))
                {
                    targetPlayerPosition = _inRangeColliders[i].transform.position;
                    break;
                }
            }

            Vector3 direction = (targetPlayerPosition - aimPosition.position).normalized;
            direction.y = Mathf.Clamp(direction.y, -maxVerticleDetection, maxVerticleDetection);
            
            float angle = Vector3.Angle(aimPosition.forward, direction);
            if (_inRangeAngle/2 > angle)
            {
                _isTargetInRange = true;
                CheckForPlayerDetection(direction);
            }
            else
            {
                if (Vector3.Distance(aimPosition.position, targetPlayerPosition) <= _behindDetectionRange)
                {
                    CheckForPlayerDetection(direction);
                }
                _isTargetInRange = false;
            }
        }
        else
        {
            _isTargetInRange = false;
            isDetectingPlayer = false;
        }
    }

    private void CheckForPlayerDetection(Vector3 direction)
    {
        Ray rayToPlayer = new Ray(aimPosition.position, direction);

        if (Physics.Raycast(rayToPlayer, out RaycastHit hit, Vector3.Distance(aimPosition.position, targetPlayerPosition), _RaycastCheckLayers))
        {
            Debug.DrawLine(aimPosition.position, hit.point);
            if (hit.collider.CompareTag("PlayerHurtbox"))
            {
                isDetectingPlayer = true;
            }
            else
            {
                isDetectingPlayer = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(aimPosition.position, _inRangeRadius);
    }
}
