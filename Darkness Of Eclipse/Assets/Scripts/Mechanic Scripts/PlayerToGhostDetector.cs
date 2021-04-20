using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/*! \brief A script that converts the GhostColliderDetectorU# script into a single frame bool variable
 *
 *  [Mechanic Script]
 */
public class PlayerToGhostDetector : MonoBehaviour
{
    [Header("Assets")]
    public Collider playerCollider; //!< The player collider.
    public GhostColliderDetector[] unverColliderScripts = new GhostColliderDetector[7]; //!< The Unver collider scripts

    [Header("Detected")]
    public bool playerDetected = false; //!< A boolean that determines whether or not the player has collided with the unver colliders.

    void Update()
    {
        playerDetected = false;

        for (int i = 0; i < unverColliderScripts.Length; i++)
        {
            if (unverColliderScripts[i].isColliding)
            {
                playerDetected = true;
            }
        }
    }

    void LateUpdate()
    {
        if (playerDetected) playerDetected = false;
    }
}
