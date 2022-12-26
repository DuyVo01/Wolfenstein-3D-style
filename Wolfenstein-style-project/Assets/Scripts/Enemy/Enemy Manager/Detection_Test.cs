using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection_Test : MonoBehaviour
{
    [SerializeField] private float _inRangeRadius;
    [SerializeField] [Range(0, 360)] private float _inRangeAngle; 
    [SerializeField] private LayerMask _layerToDetect;
    [SerializeField] private LayerMask _RaycastCheckLayers;
    [SerializeField] private float _detectionRangeFromBehind;

    bool _isTargetInRange;
    public bool isDetectingPlayer;

    Collider[] _inRangeColliders;
    public Vector3 targetPlayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfPlayerInRange();
        
    }

    private void CheckIfPlayerInRange()
    {
        _inRangeColliders = Physics.OverlapSphere(transform.position, _inRangeRadius, _layerToDetect);

        if(_inRangeColliders.Length != 0)
        {

            for(int i = 0; i < _inRangeColliders.Length; i++)
            {
                if (_inRangeColliders[i].CompareTag("PlayerHurtbox"))
                {
                    targetPlayer = _inRangeColliders[i].transform.position;
                    break;
                }
            }

            Vector3 direction = (targetPlayer - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);
            if (_inRangeAngle/2 > angle)
            {
                _isTargetInRange = true;
                CheckForPlayerDetection(direction);
            }
            else
            {
                if (Vector3.Distance(transform.position, targetPlayer) <= _detectionRangeFromBehind)
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
        Ray rayToPlayer = new Ray(transform.position, direction);

        if (Physics.Raycast(rayToPlayer, out RaycastHit hit, Vector3.Distance(transform.position, targetPlayer), _RaycastCheckLayers))
        {
            Debug.DrawLine(transform.position, hit.point);
            Debug.Log(hit.collider.tag);
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

    private void CheckForPlayerDetectionFromBehind()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _inRangeRadius);

    }
}
