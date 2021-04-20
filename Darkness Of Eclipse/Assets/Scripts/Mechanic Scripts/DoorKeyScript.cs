using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a key that can unlock a door.
 *
 *  [Mechanic Script]
 */
public class DoorKeyScript : MonoBehaviour
{
    [Header("Door")]
    public Collider doorCollider; //!< The door lock area trigger.
    public DoorScript doorScript; //!< The script that controls the door.

    [Header("Debugs")]
    public bool pickupDebug; //!< A switch used to debug the key being picked up.

    void OnTriggerEnter(Collider collisionData)
    {
        if (collisionData == doorCollider)
        {
            doorScript.locked = false;

            gameObject.SetActive(false);
        }
    }
}
