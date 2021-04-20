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

    [HideInInspector] public bool isColliding = false; //!< A boolean that controls whether or not the player is colliding with the Unver.

    void FixedUpdate()
    {
        isColliding = false;
    }

    void OnTriggerStay(Collider collisionData)
    {
        if (collisionData == ghostScript.playerCollider)
        {
            isColliding = true;
        }
    }

    void Update()
    {
        if (isColliding) Debug.Log("Unver Collision Is True");
    }
}