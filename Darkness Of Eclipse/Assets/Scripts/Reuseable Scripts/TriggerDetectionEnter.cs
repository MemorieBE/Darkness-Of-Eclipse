using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that enables a boolean and disables the game object when the player enters the trigger.
 *  Also enables and disables an array of game objects when activated.
 *
 *  [Reusable Script]
 */
public class TriggerDetectionEnter : MonoBehaviour
{
    [Header("Player")]
    public Collider playerCollider; //!< The player collider.

    [Header("Game Objects")]
    public GameObject[] enableOnTrigger; //!< The game objects to enable on trigger.
    public GameObject[] disableOnTrigger; //!< The game objects to disable on trigger.

    [Header("Inputs")]
    public bool disableOnActivation = true; //!< A boolean that controls whether or not the detector will be disabled when it is activated.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    void OnEnable()
    {
        activated = false;
    }

    void OnTriggerEnter(Collider collisionData)
    {
        if (collisionData == playerCollider)
        {
            activated = true;

            for (int i = 0; i < enableOnTrigger.Length; i++) { enableOnTrigger[i].SetActive(true); }
            for (int i = 0; i < disableOnTrigger.Length; i++) { disableOnTrigger[i].SetActive(false); }
        }
    }

    void LateUpdate()
    {
        if (activated)
        {
            activated = false;
            if (disableOnActivation) gameObject.SetActive(false);
        }
    }
}
