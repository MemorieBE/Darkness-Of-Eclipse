using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that enables a boolean when the player enters the trigger and disables the boolean when the player exits the trigger.
 *  Also enables and disables an array of game objects when activated and vice versa.
 *
 *  [Reusable Script]
 */
public class TriggerDetectionStay : MonoBehaviour
{
    [Header("Player")]
    public Collider playerCollider; //!< The player collider.

    [Header("Game Objects")]
    public GameObject[] enableOnTrigger; //!< The game objects to enable on trigger.
    public GameObject[] disableOnTrigger; //!< The game objects to disable on trigger.

    [HideInInspector] public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    void OnTriggerEnter(Collider collisionData)
    {
        if (collisionData == playerCollider)
        {
            activated = true;

            for (int i = 0; i < enableOnTrigger.Length; i++) { enableOnTrigger[i].SetActive(true); }
            for (int i = 0; i < disableOnTrigger.Length; i++) { disableOnTrigger[i].SetActive(false); }
        }
    }

    void OnTriggerExit(Collider collisionData)
    {
        if (collisionData == playerCollider)
        {
            activated = false;

            for (int i = 0; i < enableOnTrigger.Length; i++) { enableOnTrigger[i].SetActive(false); }
            for (int i = 0; i < disableOnTrigger.Length; i++) { disableOnTrigger[i].SetActive(true); }
        }
    }
}
