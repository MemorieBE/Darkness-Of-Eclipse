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
    [SerializeField] private Collider playerCollider; //!< The player collider.

    [Header("Game Objects")]
    [SerializeField] private GameObject[] enableOnTrigger; //!< The game objects to enable on trigger.
    [SerializeField] private GameObject[] disableOnTrigger; //!< The game objects to disable on trigger.

    [Header("Inputs")]
    [SerializeField] private bool disableOnActivation = true; //!< A boolean that controls whether or not the detector will be disabled when it is activated.
    [SerializeField] private bool multipleUses = false; //!< A boolean that controls whether or not the detector can be used more than once.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    void OnTriggerEnter(Collider collisionData)
    {
        if (collisionData == playerCollider && !activated)
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
            if (multipleUses) activated = false;
            if (disableOnActivation) gameObject.SetActive(false);
        }
    }
}
