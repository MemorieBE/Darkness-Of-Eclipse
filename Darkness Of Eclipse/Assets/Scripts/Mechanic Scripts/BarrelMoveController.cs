using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BarrelMoveController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private Vector2 playerBarrelDistance = Vector2.one;
    [SerializeField] private float barrelRollForce = 1000f;
    [SerializeField] private float barrelPushForce = 1000f;
    [SerializeField] private float pushCooldownTime = 1f;
    [SerializeField] private float rollableDistance = 0.5f;
    [SerializeField] private float turnSpeed = 25f;

    [Header("References")]
    [SerializeField] private PlayerControllerCC player;
    private CharacterController cC;
    [SerializeField] private InteractController interactScript;
    [SerializeField] private Animator barrelAnim;
    [SerializeField] private Collider barrelCollider;
    [SerializeField] private Collider endPlacement;
    private Rigidbody rB;

    [Header("Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference exitAction;

    private bool barrelMoved = false;
    private bool movingBarrel = false;
    private bool sideMovement = false;
    private bool pushCooldown = false;

    private Vector3 closestPlayerBarrelPosition;
    private Vector3 oldClosestPlayerBarrelPosition;
    private Vector3 playerPosition;
    private Vector3 directionBarrelRolled;
    private float distanceToPosition;

    void Awake()
    {
        cC = player.GetComponent<CharacterController>();
        rB = gameObject.GetComponent<Rigidbody>();

        moveAction.action.Enable();
        exitAction.action.Enable();
    }

    void OnDisable()
    {
       if (movingBarrel) { exitAction.action.performed -= ExitControl; }
    }

    void Update()
    {
        if (movingBarrel)
        {
            Vector3 moveInput = moveAction.action.ReadValue<Vector2>();

            float rollInput = sideMovement ? moveInput.x : moveInput.y;

            if (rollInput != 0)
            {
                Vector3 rollDir = sideMovement ? Vector3.Cross(transform.position - player.transform.position, Vector3.down) : transform.position - player.transform.position;
                rB.AddForceAtPosition((rollDir - rollDir.y * Vector3.up).normalized * rollInput * barrelRollForce * Time.deltaTime,
                    barrelCollider.ClosestPoint(transform.position + Vector3.up)); // UPDATE WITH COLLIDER RADIUS.
            }

            if (sideMovement)
            {
                if (moveInput.y != 0 && !pushCooldown) { StartCoroutine(BarrelPush(moveInput.y)); }
            }
            else
            {
                if (moveInput.x != 0) { transform.Rotate(transform.InverseTransformDirection(Vector3.up * moveInput.x * turnSpeed * Time.deltaTime)); }
            }
        }
    }

    private void ExitControl(InputAction.CallbackContext ctx)
    {
        movingBarrel = false;

        exitAction.action.performed -= ExitControl;
    }

    public void MoveBarrel()
    {
        barrelMoved = true; //TEMP

        if (barrelMoved)
        {
            player.enableController = false;
            player.alwaysAllowGravity = true;
            interactScript.enabled = false;
            interactScript.interactUI.SetActive(false);

            StartCoroutine(MovingBarrel());
        }
    }

    private IEnumerator BarrelPush(float inputMultiplier)
    {
        Vector3 rollDir = transform.position - player.transform.position;
        rB.AddForce((rollDir - rollDir.y * Vector3.up).normalized * barrelPushForce * inputMultiplier);

        pushCooldown = true;
        yield return new WaitForSeconds(pushCooldownTime);
        pushCooldown = false;
    }

    private IEnumerator MovingBarrel()
    {
        bool inRollableDistance = false;
        UpdateDistanceToPosition();

        while (true)
        {
            MovePlayerToPosition(out inRollableDistance);

            if (inRollableDistance)
            {
                break;
            }
            else
            {
                yield return null;
            }
        }

        movingBarrel = true;

        exitAction.action.performed += ExitControl;

        while (true)
        {
            if (!inRollableDistance || !movingBarrel)
            {
                movingBarrel = false;
                break;
            }
            else
            {
                yield return null;
            }

            MovePlayerToPosition(out inRollableDistance);
        }

        player.enableController = true;
        interactScript.enabled = true;
    }

    private void MovePlayerToPosition(out bool inRollableDistance)
    {
        {
            oldClosestPlayerBarrelPosition = closestPlayerBarrelPosition;

            Vector3 barrelDir = Vector3.Cross(transform.up, Vector3.up);
            Vector3 playerBarrelPosition1 = transform.position + barrelDir * playerBarrelDistance.x;
            Vector3 playerBarrelPosition2 = transform.position - barrelDir * playerBarrelDistance.x;
            Vector3 playerBarrelPosition3 = transform.position + transform.up * playerBarrelDistance.y;
            Vector3 playerBarrelPosition4 = transform.position - transform.up * playerBarrelDistance.y;

            playerPosition = player.transform.position;
            closestPlayerBarrelPosition = GetClosestPlayerBarrelPosition(playerBarrelPosition1, playerBarrelPosition2, playerBarrelPosition3, playerBarrelPosition4);
        }

        Vector3 playerMoveDirection = ((closestPlayerBarrelPosition - playerPosition) - Vector3.up * (closestPlayerBarrelPosition - playerPosition).y).normalized;

        float currentSpeed;
        if (distanceToPosition <= player.moveSpeed * Time.deltaTime) { currentSpeed = distanceToPosition / Time.deltaTime; }
        else { currentSpeed = player.moveSpeed; }

        directionBarrelRolled = (closestPlayerBarrelPosition - Vector3.up * closestPlayerBarrelPosition.y) - (oldClosestPlayerBarrelPosition - Vector3.up * oldClosestPlayerBarrelPosition.y);
        Vector3 finalDirection = directionBarrelRolled + playerMoveDirection * currentSpeed;

        if (finalDirection.magnitude > player.moveSpeed * player.sprintMultiplier) { finalDirection = finalDirection.normalized * player.moveSpeed * player.sprintMultiplier; }

        cC.Move(finalDirection * Time.deltaTime);

        UpdateDistanceToPosition();
        inRollableDistance = distanceToPosition <= rollableDistance;
    }

    private void UpdateDistanceToPosition() 
    { 
        distanceToPosition = Vector3.Distance(playerPosition - (Vector3.up * playerPosition.y), closestPlayerBarrelPosition - (Vector3.up * closestPlayerBarrelPosition.y)); 
    }

    private Vector3 GetClosestPlayerBarrelPosition(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
    {
        float aD = Vector3.Distance(playerPosition, a);
        float bD = Vector3.Distance(playerPosition, b);
        float cD = Vector3.Distance(playerPosition, c);
        float dD = Vector3.Distance(playerPosition, d);

        float minD = Mathf.Min(aD, bD, cD, dD);

        if (minD == aD) { sideMovement = false; return a; }
        if (minD == bD) { sideMovement = false; return b; }
        if (minD == cD) { sideMovement = true; return c; }
        if (minD == dD) { sideMovement = true; return d; }

        return Vector3.zero;
    }

    // [Check] Find positions that the player goes to before rolling barrel. (Two positions: either side of the barrel.)
    // Add force to the barrel relative to the players movement.
    // Make the player stay with the barrel unless the barrel becomes too far away.
    // Make barrel also move to align with player to avoid the player clipping through walls.
    // Have another barrel model as a child of the player specifically for animation purposes that the 'real' barrel will reposition to after the animation.
    // Make barrel roll down stairs with a 'whole-scene' animation.
    // Make barrel be able to work with animations without the barrel being forcefully rotated to a specific Y rotation.
}
