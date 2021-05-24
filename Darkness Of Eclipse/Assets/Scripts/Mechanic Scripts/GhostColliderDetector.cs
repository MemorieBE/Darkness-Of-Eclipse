using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that gets data from enemy collider and coverts it into a bool variable for the Unver player detection script.
 *
 *  [Mechanic Script]
 */
public class GhostColliderDetector : MonoBehaviour
{
    [Header("Player")]
    public PlayerToGhostDetector ghostScript; //!< The script that controls the Unver player detection.

    void OnTriggerStay(Collider collisionData)
    {
        if (collisionData == ghostScript.playerCollider)
        {
            ghostScript.playerDetected = true;

            Debug.Log("Unver Collision Is True");
        }
    }
}