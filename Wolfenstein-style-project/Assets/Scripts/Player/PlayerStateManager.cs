using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [Header("Non-script Components")]
    public Rigidbody playerRB;
    public Camera cameraMain;
    public PlayerMovementInputHandler playerMovementInputHandler;
    [SerializeField] private Transform aimingTarget;

    [Header("Layer Mask")]
    [SerializeField] LayerMask layerMask;

    [Header("Movement")]
    public float walkMoveSpeed;
    public float rotationSpeed;

    private StateMachine _stateMachine;
    

    private Vector3 _movementDirection;

    //Ground child States
    public IdleState idleState;
    public WalkState walkState;
    public RunState runState;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerMovementInputHandler = GetComponent<PlayerMovementInputHandler>();
        cameraMain = Camera.main;
    }

    // Start is called before the first frame update
    private void Start()
    {
        _stateMachine = new StateMachine();

        idleState = new IdleState(_stateMachine, this);
        walkState = new WalkState(_stateMachine, this);
        runState = new RunState(_stateMachine, this);

        _stateMachine.Initialize(idleState);
        
    }

    // Update is called once per frame
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        DrawRay(aimingTarget);

        MovementDirectionRelatedToCameraDirection();
        PlayerRotationFollowCamera();
        _stateMachine.LogicalUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.PhysicalUpdate();
        
    }

    private void MovementDirectionRelatedToCameraDirection()
    {
        _movementDirection = new Vector3(playerMovementInputHandler.normalizeMovementInput.x, 0f, playerMovementInputHandler.normalizeMovementInput.y);
        _movementDirection = _movementDirection.x * cameraMain.transform.right.normalized + _movementDirection.z * cameraMain.transform.forward.normalized;
        _movementDirection.y = 0;
    }

    public Vector3 GetMovementDirection()
    {
        return this._movementDirection;
    }

    public void PlayerRotationFollowCamera()
    {
        float targetAngle = cameraMain.transform.rotation.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void DrawRay(Transform aimingTarget)
    {
        Ray ray = cameraMain.ScreenPointToRay(playerMovementInputHandler.GetMousePosition());
        if(Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask))
        {
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            aimingTarget.position = Vector3.Lerp(aimingTarget.position, hit.point, 30 * Time.deltaTime);
            Debug.Log(hit.point);
        }
    }
}
