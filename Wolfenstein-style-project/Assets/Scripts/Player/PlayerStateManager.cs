using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerStateManager : MonoBehaviour
{
    [Header("Non-script Components")]
    public Rigidbody playerRB;
    public Camera cameraMain;
    public PlayerMovementInputHandler playerMovementInputHandler;
    public PlayerActionInputHandler playerActionInputHandler;
    public Transform aimingTarget;
    public Transform aimingRigTarget;
    public CapsuleCollider playerCapsuleCollider;
    public CinemachineVirtualCamera cinemachineVC;
    public CinemachineBasicMultiChannelPerlin cinemachineNoise;
    public Animator playerAnimator;

    [Header("Body Components")]
    public Transform body;
    public Transform feet;

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

    [Header("Floating Collider")]
    public float floatingDistance;

    [Header("Air Movement")]
    public float fallMultiplier;

    //Check variable
    public bool isGround;

    //Private Members
    private StateMachine _stateMachine;
    private Vector3 _movementDirection;
    
    //Ground child States
    public IdleState idleState;
    public WalkState walkState;
    public RunState runState;
    
    //Air State
    public InAirState inAirState;

    Vector3 mouseTurn;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
        playerMovementInputHandler = GetComponent<PlayerMovementInputHandler>();
        playerActionInputHandler = GetComponent<PlayerActionInputHandler>();
        playerCapsuleCollider = GetComponent<CapsuleCollider>();
        cameraMain = Camera.main;
        cinemachineNoise = cinemachineVC.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Start()
    {
        _stateMachine = new StateMachine();
        idleState = new IdleState(_stateMachine, this);
        walkState = new WalkState(_stateMachine, this);
        runState = new RunState(_stateMachine, this);
        inAirState = new InAirState(_stateMachine, this);
        _stateMachine.Initialize(idleState);
    }
    
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        BodyRotation();
        _stateMachine.LogicalUpdate();
    }
    
    private void FixedUpdate()
    {
        //BodyRotation();
        GroundCheck();
        MovementDirectionRelatedToCameraDirection();
        _stateMachine.PhysicalUpdate();
        
    }

    private void LateUpdate()
    {
        cinemachineVC.transform.localRotation = Quaternion.Euler(body.eulerAngles);
    }

    private void MovementDirectionRelatedToCameraDirection()
    {
        _movementDirection = new Vector3(playerMovementInputHandler.normalizeMovementInput.x, 0f, playerMovementInputHandler.normalizeMovementInput.y);
        _movementDirection = _movementDirection.x * cameraMain.transform.right.normalized + _movementDirection.z * cameraMain.transform.forward.normalized;
        _movementDirection.y = 0;
    }
    public void BodyRotation()
    {
        //mouseTurn.x += playerMovementInputHandler.GetMouseDelta().x * Time.deltaTime * 10;
        //mouseTurn.y += playerMovementInputHandler.GetMouseDelta().y * Time.deltaTime * 10;
        //transform.rotation = Quaternion.Euler(0, mouseTurn.x, 0);
        //body.rotation = Quaternion.Euler(-mouseTurn.y, mouseTurn.x, 0);

        Quaternion targetFeetRotation = Quaternion.Euler(0, cameraMain.transform.rotation.eulerAngles.y, 0);
        body.rotation = Quaternion.Slerp(body.rotation, targetFeetRotation, rotationSpeed * Time.deltaTime);
        //playerRB.MoveRotation(targetFeetRotation);
        //Vector3 targetRot = Vector3.Cross(cinemachineVC.transform.forward, transform.forward);
        //playerRB.AddTorque(targetRot * rotationSpeed - playerRB.angularVelocity);
    }

    public void GroundCheck()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
    }
   
    public void FloatingCollider()
    {
        float rideHeight = floatingDistance;
        Ray downRay = new Ray(playerCapsuleCollider.bounds.center, Vector3.down);
        RaycastHit rayFloatHit;
        bool rayDidHit = Physics.Raycast(downRay, out rayFloatHit, rideHeight, whatIsGround);
        if (rayDidHit)
        {
            Debug.DrawRay(playerCapsuleCollider.bounds.center, downRay.direction * rideHeight, Color.yellow);
            float distanceToLift = playerCapsuleCollider.center.y * transform.localScale.y - rayFloatHit.distance;
            float amountToLift = distanceToLift * 10 - playerRB.velocity.y;
            Vector3 springForce = new Vector3(0f, amountToLift, 0f);
            playerRB.AddForce(springForce, ForceMode.VelocityChange);
        }   
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
