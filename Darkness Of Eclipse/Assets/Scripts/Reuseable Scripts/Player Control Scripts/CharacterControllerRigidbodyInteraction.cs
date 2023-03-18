using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

/*! \brief A script that allows a character controller to interact with rigidbodies.
 *
 *  Independent
 */
public class CharacterControllerRigidbodyInteraction : MonoBehaviour
{
    [Header("Input")]
    public float rigidbodyPushForce = 5f; //!< How hard the controller will push away rigidbodies when colliding with them.

    private CharacterController cC; //!< The character controller.

    void Awake()
    {
        cC = gameObject.GetComponent<CharacterController>();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Pushes collided rigidbody away.

        if (hit.rigidbody == null || hit.rigidbody.isKinematic) { return; }

        hit.rigidbody.AddForceAtPosition((hit.collider.ClosestPoint(hit.point) - cC.ClosestPoint(hit.point)).normalized * (cC.velocity.magnitude + rigidbodyPushForce), hit.point);
    }
}
