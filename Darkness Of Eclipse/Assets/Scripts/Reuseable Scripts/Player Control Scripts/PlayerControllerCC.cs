using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

/*! \brief A script that controls how the player moves using a character chontroller.
 *
 *  Independent
 */
public class PlayerControllerCC : MonoBehaviour
{
    public static bool allowPlayerInputs = true; //!< A boolean that controls whether or not the player inputs are read.

    public static bool sprintDisabled = false; //!< A boolean that controls whether or not sprinting is disabled.
    public static bool sneakDisabled = false; //!< A boolean that controls whether or not sprinting is disabled.
    public static bool normalizedMovement = false; //!< A boolean that controls whether or not the player's movement direction is normalized.

    [Header("Inputs")]
    public float moveSpeed = 2f; //!< How fast the player can move.
    public float strafeMultiplier = 0.75f; //!< How much the move speed will be multiplied when moving sideways.
    public float sprintMultiplier = 1.5f; //!< The number that is multiplied with the move speed to calculate the sprint speed.
    public float sneakSpeedMultiplier = 0.5f; //!< The number that is multiplied with the move speed to calculate the sneak speed.
    private float currentSpeed; //!< The current speed with the calculated multipliers.
    public float jumpForce = 3f; //!< How high/hard the player will jump.
    public float gravity = 5f; //!< How strong the player's gravity is.
    public float maxDropVelocity = 10f; //!< The gravity terminal velocity.
    public float rigidbodyPushForce = 5f; //!< How hard the player will push away rigidbodies when colliding with them.

    [Header("Actions")]
    [SerializeField] private InputActionReference moveAction; //!< The move action.
    [SerializeField] private InputActionReference sprintAction; //!< The sprint action.
    [SerializeField] private InputActionReference sneakAction; //!< The sneak action.
    [SerializeField] private InputActionReference jumpAction; //!< The jump action.

    private Vector2 moveInput; //!< The move input.
    private bool sprintInput; //!< The sprint input.
    private bool sneakInput; //!< The sneak input.
    private bool jumpInput; //!< The jump input.

    private CharacterController characterController; //!< The player character controller.

    private Vector3 direction = Vector3.zero; //!< The current flat direction that the character controller will move towards.

    private float currentyDropVelocity = 0f; //!< The current drop velocity.

    void Awake()
    {
        allowPlayerInputs = true;
    }

    void OnEnable()
    {
        moveAction.action.Enable();
        sprintAction.action.Enable();
        sneakAction.action.Enable();
        jumpAction.action.Enable();
    }

    void OnDisable()
    {
        moveAction.action.Disable();
        sprintAction.action.Disable();
        sneakAction.action.Disable();
        jumpAction.action.Disable();
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        sprintDisabled = false;
        sneakDisabled = false;
        normalizedMovement = false;
    }

    void FixedUpdate()
    {
        // Gets gravity.
        if (!characterController.isGrounded)
        {
            if (currentyDropVelocity + gravity * Time.fixedDeltaTime >= maxDropVelocity) currentyDropVelocity = maxDropVelocity;
            else currentyDropVelocity += gravity * Time.fixedDeltaTime;
        }

        // Calculates player movement in controller.
        characterController.Move(currentSpeed * Time.fixedDeltaTime * Vector3.ProjectOnPlane(direction, Vector3.up) + Vector3.down * currentyDropVelocity * Time.fixedDeltaTime);
    }

    void Update()
    {
        moveInput = moveAction.action.ReadValue<Vector2>();
        sprintInput = sprintAction.action.ReadValue<float>() == 1f;
        sneakInput = sneakAction.action.ReadValue<float>() == 1f;
        jumpInput = jumpAction.action.ReadValue<float>() == 1f;

        // Gets local direction of movement input.
        {
            Vector3 localDirection = Vector3.zero;
            if (moveInput.y == 1f) localDirection += Vector3.forward;
            if (moveInput.y == -1f) localDirection += Vector3.back;
            if (moveInput.x == -1f) localDirection += Vector3.left * strafeMultiplier;
            if (moveInput.x == 1f) localDirection += Vector3.right * strafeMultiplier;

            if (normalizedMovement) localDirection = localDirection.normalized;
            if (!allowPlayerInputs) localDirection = Vector3.zero;

            direction = gameObject.transform.TransformPoint(localDirection) - gameObject.transform.position;
        }

        // Gets player jump.
        if (characterController.isGrounded)
        {
            if (jumpInput && allowPlayerInputs) currentyDropVelocity = jumpForce * -1f;
            else currentyDropVelocity = 0f;
        }

        // Calculates the current speed.
        if (sprintInput && !sneakInput && !sprintDisabled) currentSpeed = moveSpeed * sprintMultiplier;
        else if (sneakInput && !sneakDisabled) currentSpeed = moveSpeed * sneakSpeedMultiplier;
        else currentSpeed = moveSpeed;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Pushes collided rigidbody away.

        if (hit.rigidbody == null || hit.rigidbody.isKinematic) { return; }

        hit.rigidbody.AddForceAtPosition((hit.collider.ClosestPoint(hit.point) - characterController.ClosestPoint(hit.point)).normalized * (characterController.velocity.magnitude + rigidbodyPushForce), hit.point);
    }
}
