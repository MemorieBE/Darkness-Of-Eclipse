using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that gets data from enemy collider and coverts it into a bool variable for the Unver player detection script.
 *
 *  [Mechanic Script]
 */
public class PlayerToGhostDetector : MonoBehaviour
{
    [Header("Player")]
    public Collider playerCollider; //!< The player collider.
    public DeathByUnver ghostScript; //!< The script that controls the Unver player detection.

    [HideInInspector] public bool playerDetected = false; //!< A read only player detection variable.

    void FixedUpdate()
    {
        playerDetected = false;
    }

    void OnTriggerStay(Collider collisionData)
    {
        if (collisionData == playerCollider)
        {
            playerDetected = true;

            Debug.Log("Unver Collision Is True");
        }
    }
}