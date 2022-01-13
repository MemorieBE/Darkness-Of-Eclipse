using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that invokes an "Deactivated" function to certain scripts on trigger exit.
 *
 *  Independent
 */
public class ADED_OnTriggerExit : MonoBehaviour
{
    [Header("Collision")]
    [SerializeField] private Collider targetCollider; //!< The collider that will trigger the scripts.

    [Header("ADED")]
    [SerializeField] private ADED aDED = ADED.deactivate; //!< The ADED type.
    [SerializeField] private MonoBehaviour[] scripts; //!< The script that will be effected.

    [Header("Inputs")]
    [SerializeField] private bool disableOnActivation = true; //!< A boolean that controls whether or not the detector will be disabled when it is deactivated.
    [SerializeField] private bool resetsActivation = false; //!< A boolean that controls whether or not the activation will reset after it has been activated.
    [SerializeField] private float delay = 0f; //!< The amount of time in seconds the trigger will be delayed.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    private enum ADED
    {
        activate,
        deactivate,
        elevate,
        delevate
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider == targetCollider && !activated)
        {
            if (activated) { return; }

            if (delay > 0f)
            {
                StartCoroutine(DelayedTrigger());
                return;
            }

            InvokeADED();
        }
    }

    /*!
     *  An IEnumerator that delays the trigger.
     */
    private IEnumerator DelayedTrigger()
    {
        yield return new WaitForSeconds(delay);

        InvokeADED();
    }

    /*!
     *  A method that invokes the ADED specific trigger.
     */
    private void InvokeADED()
    {
        for (int i = 0; i < scripts.Length; i++)
        {
            activated = true;

            switch (aDED)
            {
                case ADED.activate:
                    scripts[i].Invoke("Activated", 0f);
                    break;
                case ADED.deactivate:
                    scripts[i].Invoke("Deactivated", 0f);
                    break;
                case ADED.elevate:
                    scripts[i].Invoke("Elevated", 0f);
                    break;
                case ADED.delevate:
                    scripts[i].Invoke("Delevated", 0f);
                    break;
            }
        }

        if (resetsActivation) { activated = false; }
        if (disableOnActivation) { gameObject.SetActive(false); }
    }
}
