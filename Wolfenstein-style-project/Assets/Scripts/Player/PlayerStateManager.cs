using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [Header("Non-script Components")]
    public Rigidbody playerRB;
    public Camera cameraMain;
    public PlayerMovementInputHandler playerMovementInputHandler;
    public PlayerActionInputHandler playerActionInputHandler;
    public Transform aimingTarget;

    [Header("Body Components")]
    public Transform body;
    public Transform feet;
    public Transform highPoint;
    public Transform feetPoint;

    [Header("Check")]
    public Transform groundCheck;
    [SerializeField] float groundCheckRadius;
    public LayerMask whatIsGround;

    [Header("Layer Mask")]
    public LayerMask layerMask;

    [Header("Movement")]
    public float walkMoveSpeed;
    public float rotationSpeed;
    public float stepClimbSpeed;

    //Check variable
    public bool isGround;

    //Private Members
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
        playerActionInputHandler = GetComponent<PlayerActionInputHandler>();
        cameraMain = Camera.main;
    }

    private void Start()
    {
        _stateMachine = new StateMachine();

        idleState = new IdleState(_stateMachine, this);
        walkState = new WalkState(_stateMachine, this);
        runState = new RunState(_stateMachine, this);

        _stateMachine.Initialize(idleState);
        
    }
    
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        BodyRotation();
        FeetRotation();

        MovementDirectionRelatedToCameraDirection();
       
        _stateMachine.LogicalUpdate();
    }
    
    private void FixedUpdate()
    {
        GroundCheck();
        _stateMachine.PhysicalUpdate();
    }
    
    private void MovementDirectionRelatedToCameraDirection()
    {
        _movementDirection = new Vector3(playerMovementInputHandler.normalizeMovementInput.x, 0f, playerMovementInputHandler.normalizeMovementInput.y);
        _movementDirection = _movementDirection.x * cameraMain.transform.right.normalized + _movementDirection.z * cameraMain.transform.forward.normalized;
        _movementDirection.y = 0;
    }

    public void BodyRotation()
    {
        Ray ray = cameraMain.ScreenPointToRay(playerMovementInputHandler.GetMousePosition());
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, float.MaxValue, layerMask))
        {
            Vector3 rotationDirection = raycastHit.point - body.position;
            Quaternion targetRotation = Quaternion.LookRotation(rotationDirection, Vector3.up);

            body.localRotation = Quaternion.Lerp(body.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
    
    public void FeetRotation()
    {
        Quaternion targetRotation = Quaternion.Euler(0, cameraMain.transform.eulerAngles.y, 0);

        feet.rotation = Quaternion.Lerp(feet.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    
    public void StepClimb()
    {
        float stepDistance = Vector3.Distance(highPoint.position, feetPoint.position);
        Ray stepRay = new Ray(highPoint.position, feetPoint.position - highPoint.position);
        RaycastHit stepRayHit;

        Debug.Log(stepDistance);

        if(Physics.Raycast(stepRay, out stepRayHit, layerMask))
        {
            Debug.DrawRay(highPoint.position, feetPoint.position - highPoint.position);
            float stepForce = (stepDistance - Vector3.Distance(highPoint.position, stepRayHit.point)) * stepClimbSpeed - playerRB.velocity.y;
            Vector3 stepForceApply = new Vector3(0, stepForce, 0);

            playerRB.AddForce(stepForceApply, ForceMode.VelocityChange);
        }
    }
    
    public void GroundCheck()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public Vector3 GetMovementDirection()
    {
        return this._movementDirection;
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
