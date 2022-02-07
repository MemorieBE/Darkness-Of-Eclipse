using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that invokes a function to certain scripts on trigger exit.
 *
 *  Independent
 */
public class InvokeOnTriggerExit : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private Collider targetCollider; //!< The collider that will trigger the scripts.

    [Header("Invoke")]
    [SerializeField] private MonoBehaviour[] scripts; //!< The script that will be effected.
    [SerializeField] private string[] invokes; //!< The name of the functions to invoke.

    [Header("Input")]
    [SerializeField] private bool disableOnActivation = true; //!< A boolean that controls whether or not the detector will be disabled when it is activated.
    [SerializeField] private bool resetsActivation = false; //!< A boolean that controls whether or not the deactivation will reset after it has been deactivated.
    [SerializeField] private float delay = 0f; //!< The amount of time in seconds the trigger will be delayed.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    void OnTriggerExit(Collider collider)
    {
        if (collider == targetCollider && !activated)
        {
            if (activated) { return; }

            InvokeFunctions();
        }
    }

    /*!
     *  A method that invokes the functions.
     */
    private void InvokeFunctions()
    {
        activated = true;

        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].Invoke(invokes[i], delay);
        }

        if (resetsActivation) { activated = false; }
        if (disableOnActivation) { gameObject.SetActive(false); }
    }

    void OnValidate()
    {
        if (scripts.Length != invokes.Length)
        {
            string[] oldInvokes = invokes;
            invokes = new string[scripts.Length];

            for (int i = 0; i < invokes.Length && i < oldInvokes.Length; i++)
            {
                invokes[i] = oldInvokes[i];
            }
        }
    }
}
