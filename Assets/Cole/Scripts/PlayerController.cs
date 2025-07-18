using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float WalkSpeed = 5f;
    public float SprintSpeed = 7f;
    public float CrouchSpeed = 2f;
    public float JumpHeight = 2f;
    private const float g = -9.81f; // gravity


    [Header("Mouse Look Settings")]
    public float MouseSensitivity = 2f;
    public float MaxLookAngle = 80f;


    [Header("Crouch Settings")]
    public float StandingHeight = 2f;
    public float CrouchHeight = 1.6f;
    public float CrouchTransitionSpeed = 8f;
    public float EyeHeight = 1f;

    private CharacterController controller;
    private Camera playerCam;
    public GameObject body;

    private Vector3 velocity;

    private float horizontal;
    private float vertical;
    private bool jumpInput;
    private bool crouchInput;
    private bool sprintInput;

    private bool isGrounded;
    private bool isCrouching;
    private bool isSprinting;
    private float currentStamina;
    private float staminaRegenTimer;
    private float verticalRotation;
    private float originalCameraY;
    private float targetCameraY;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCam = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


        Vector3 center = controller.center;
        center.y = StandingHeight * 0.5f;
        controller.center = center;

        Vector3 cameraPos = playerCam.transform.localPosition;
        cameraPos.y = EyeHeight;
        playerCam.transform.localPosition = cameraPos;

        originalCameraY = EyeHeight;
        targetCameraY = originalCameraY;
    }

    private void Update()
    {
        HandleInput();
        HandleMouseInput();
        HandleMovement();
        HandleCrouch();
    }

    private void HandleInput()
    {
        // movementtttt
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // actions
        jumpInput = Input.GetButtonDown("Jump");
        crouchInput = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.C);
        sprintInput = Input.GetKey(KeyCode.LeftShift) && !isCrouching;
    }

    private void HandleMouseInput()
    {
        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.Rotate(0, mouseX, 0);

        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -MaxLookAngle, MaxLookAngle);
        playerCam.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    private void HandleMovement()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // keep grounded
        }

        // calc move dir (calc short for calculate)
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        float currentSpeed = WalkSpeed;

        if (isCrouching)
        {
            currentSpeed = CrouchSpeed;
            isSprinting = false;
        }
        else if (sprintInput && currentStamina > 0 && move.magnitude > 0.1f)
        {
            currentSpeed = SprintSpeed;
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        controller.Move(move * currentSpeed * Time.deltaTime);

        if (jumpInput && isGrounded && !isCrouching)
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * g);
        }

        velocity.y += g * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleCrouch()
    {
        if (crouchInput && !isCrouching)
        {
            StartCrouch();
        }
        else if (!crouchInput && isCrouching)
        {
            if (CanStandUp())
            {
                StopCrouch();
            }
        }

        // Smoothhhhh
        float newCameraY = Mathf.Lerp(playerCam.transform.localPosition.y, targetCameraY,
                                     CrouchTransitionSpeed * Time.deltaTime);
        playerCam.transform.localPosition = new Vector3(
            playerCam.transform.localPosition.x,
            newCameraY,
            playerCam.transform.localPosition.z);
    }
    private void StartCrouch()
    {
        isCrouching = true;
        controller.height = CrouchHeight;

        float heightDifference = StandingHeight - CrouchHeight;
        targetCameraY = EyeHeight - heightDifference;

        Vector3 newCenter = controller.center;
        newCenter.y = CrouchHeight * 0.5f;
        controller.center = newCenter;


        body.transform.localScale = new Vector3(1f, 0.5f, 1f);
        body.transform.position += new Vector3(0f, -0.5f, 0f);
    }

    void StopCrouch()
    {
        isCrouching = false;
        controller.height = StandingHeight;
        targetCameraY = EyeHeight;


        Vector3 newCenter = controller.center;
        newCenter.y = StandingHeight * 0.5f;
        controller.center = newCenter;


        body.transform.localScale = new Vector3(1f, 1f, 1f);
        body.transform.position += new Vector3(0f, +0.5f, 0f);

    }

    bool CanStandUp()
    {
        float checkHeight = StandingHeight - CrouchHeight;
        Vector3 checkStart = transform.position + Vector3.up * (controller.height + 0.1f);

        return !Physics.Raycast(checkStart, Vector3.up, checkHeight);
    }
    


    public bool IsGrounded() => isGrounded;
    public bool IsCrouching() => isCrouching;
    public bool IsSprinting() => isSprinting;
}
