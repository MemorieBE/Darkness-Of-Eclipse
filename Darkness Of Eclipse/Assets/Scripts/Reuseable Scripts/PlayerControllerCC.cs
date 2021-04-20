using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

/*! \brief A script that controls how the player moves using a character chontroller.
 *
 *  [Reusable Script]
 */
public class PlayerControllerCC : MonoBehaviour
{
    [Header("Inputs")]
    public float moveSpeed = 2f; //!< How fast the player can move.
    public float sprintMultiplier = 1.5f; //!< The number that is multiplied with the move speed to calculate the sprint speed.
    public float sneakSpeedMultiplier = 0.5f; //!< The number that is multiplied with the move speed to calculate the sneak speed.
    private float currentSpeed; //!< The current speed with the calculated multipliers.
    public float jumpForce = 3f; //!< How high/hard the player will jump.
    public float gravity = 5f; //!< How strong the player's gravity is.
    public float maxDropVelocity = 10f; //!< The gravity terminal velocity.
    public float rigidbodyPushForce = 5f; //!< How hard the player will push away rigidbodies when colliding with them.

    public static string moveForwardKey = "w"; //!< The key bind used to move forward.
    public static string moveBackKey = "s"; //!< The key bind used to move backward.
    public static string moveLeftKey = "a"; //!< The key bind used to move left.
    public static string moveRightKey = "d"; //!< The key bind used to move right.
    public static string sprintKey = "left shift"; //!< The key bind used to sprint.
    public static string sneakKey = "left ctrl"; //!< The key bind used to sneak.
    public static string jumpKey = "space"; //!< The key bind used to jump.

    private CharacterController characterController; //!< The player character controller.

    private Vector3 direction = Vector3.zero; //!< The current flat direction that the character controller will move towards.

    private float currentyDropVelocity = 0f; //!< The current drop velocity.

    void Start()
    {
        characterController = GetComponent<CharacterController>();
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
        // Gets local direction of movement input.
        {
            Vector3 localDirection = Vector3.zero;
            if (Input.GetKey(moveForwardKey)) localDirection += Vector3.forward;
            if (Input.GetKey(moveBackKey)) localDirection += Vector3.back;
            if (Input.GetKey(moveLeftKey)) localDirection += Vector3.left;
            if (Input.GetKey(moveRightKey)) localDirection += Vector3.right;

            direction = gameObject.transform.TransformPoint(localDirection) - gameObject.transform.position;
        }

        // Gets player jump.
        if (characterController.isGrounded)
        {
            if (Input.GetKey(jumpKey)) currentyDropVelocity = jumpForce * -1f;
            else currentyDropVelocity = 0f;
        }

        // Calculates the current speed.
        if (Input.GetKey(sprintKey) && !Input.GetKey(sneakKey)) currentSpeed = moveSpeed * sprintMultiplier;
        else if (Input.GetKey(sneakKey)) currentSpeed = moveSpeed * sneakSpeedMultiplier;
        else currentSpeed = moveSpeed;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Pushes collided rigidbody away.

        if (hit.rigidbody == null || hit.rigidbody.isKinematic) { return; }

        hit.rigidbody.AddForceAtPosition((hit.collider.ClosestPoint(hit.point) - characterController.ClosestPoint(hit.point)).normalized * (characterController.velocity.magnitude + rigidbodyPushForce), hit.point);
    }
}
