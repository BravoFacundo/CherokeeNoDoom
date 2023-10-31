using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f; //Debug
    public float movementMultiplier = 10f;
    public float airMovementMultiplier = 0.3f;

    float horizontalMovement;
    float verticalMovement;
    [HideInInspector] public Vector3 moveDirection;
    Vector3 slopeDirection;

    [Header("Jumping")]
    public float aditionalJumps = 1;
    public float jumpForce = 25f;
    public float fallForce = 20f;
    float storeJumps;
    private bool readyToJump;

    [Header("Sprinting")]
    [SerializeField] float walkSpeed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float acceleration = 10f;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;
    [SerializeField] float waterDrag = 10f;

    [Header("Ground Detection")]
    [SerializeField] bool isGrounded; //Debug
    [SerializeField] float groundDistance = 0.2f;
    [SerializeField] LayerMask groundMask;

    [Header("Inputs")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftControl;

    [Header("Local References")]
    public Transform storePlayerOrientation;
    [SerializeField] Transform groundCheck;
    private PhysicMaterial playerPhysicsMat;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider col;
    [HideInInspector] public Camera cam;

    [Header("References")]
    [HideInInspector] GameObject canvasManager;

    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        col = GetComponentInChildren<Collider>();
        storeJumps = aditionalJumps;
        playerPhysicsMat = GetComponentInChildren<Collider>().material;
    }
    protected virtual void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        slopeDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        MoveInput();
        SprintInput();
        JumpInput();

        DebugInput();
        
        ControlDrag();
    }

    void MoveInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = storePlayerOrientation.forward * verticalMovement + storePlayerOrientation.right * horizontalMovement;
    }
    void SprintInput()
    {
        if (Input.GetKey(sprintKey) && isGrounded)
        {
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }
    void JumpInput()
    {
        if (isGrounded) aditionalJumps = storeJumps;
        if (Input.GetKeyDown(jumpKey))
        {
            if (aditionalJumps == 0 && isGrounded)
            {
                readyToJump = true;
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            }
            else
            if (aditionalJumps != 0)
            {
                readyToJump = true;
                aditionalJumps -= 1;
            }
        }
    }

    //---------- PHYSICS -----------------------------------------------------------------------------------------------------------------//

    protected virtual void FixedUpdate()
    {
        PlayerMove();
        PlayerJump();
    }
    void PlayerMove()
    {
        if (isGrounded && !OnSlope())
        {
            playerPhysicsMat.dynamicFriction = 0;
            rb.AddForce(movementMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            playerPhysicsMat.dynamicFriction = 1;
            rb.AddForce(movementMultiplier * moveSpeed * slopeDirection.normalized, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(airMovementMultiplier * movementMultiplier * moveSpeed * moveDirection.normalized, ForceMode.Acceleration);
            rb.AddForce(-transform.up * fallForce, ForceMode.Acceleration);
        }
    }
    void PlayerJump()
    {
        if (readyToJump)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            readyToJump = false;
        }
    }

    //---------- CONTROL FUNCTIONS ---------------------------------------------------------------------------------------------------//

    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
            //if (onWater) rb.drag = waterDrag;
        }
        else if (!isGrounded) rb.drag = airDrag;
    }

    RaycastHit slopeHit;
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 1.5f))
        {
            if (slopeHit.normal != Vector3.up) return true;
            else return false;
        }
        return false;
    }

    //---------- EFFECTS FUNCTIONS ---------------------------------------------------------------------------------------------------//

    public IEnumerator FastFOVChange(float newFOV, float changeTime)
    {
        float initialFOV = cam.fieldOfView;
        float elapsedTime = 0f;

        /*
        while (elapsedTime < changeTime * 0.5)
        {
            cam.fieldOfView = Mathf.Lerp(initialFOV, newFOV, elapsedTime / changeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        */

        cam.fieldOfView = newFOV;

        elapsedTime = 0f;
        while (elapsedTime < changeTime)
        {
            cam.fieldOfView = Mathf.Lerp(newFOV, initialFOV, elapsedTime / changeTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cam.fieldOfView = initialFOV;
    }

    void DebugInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
