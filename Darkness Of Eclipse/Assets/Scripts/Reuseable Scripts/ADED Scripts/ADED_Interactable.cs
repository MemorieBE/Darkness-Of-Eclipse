using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that invokes an "Activated" function to certain scripts when the player has interacted with the game object.
 *
 *  Independent
 */
public class ADED_Interactable : MonoBehaviour
{
    [Header("Interact UI")]
    public string prompt = "Interact"; //!< The text prompt that will show up when hovering over game object.
    public Sprite sprite; //!< The sprite that will show up when hovering over game object.

    [Header("ADED")]
    [SerializeField] private ADED aDED = ADED.activate; //!< The ADED type.
    [SerializeField] private MonoBehaviour[] scripts; //!< The script that will be effected.

    [Header("Inputs")]
    [SerializeField] private bool disableOnActivation = false; //!< A boolean that controls whether or not the detector will be disabled when it is activated.
    [SerializeField] private bool resetsActivation = true; //!< A boolean that controls whether or not the deactivation will reset after it has been deactivated.
    [SerializeField] private float delay = 0f; //!< The amount of time in seconds the trigger will be delayed.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    private enum ADED
    {
        activate,
        deactivate,
        elevate,
        delevate
    }

    /*!
     *  A method that is triggered when interacted.
     */
    public void Interacted()
    {
        if (activated) { return; }

        if (delay > 0f)
        {
            StartCoroutine(DelayedTrigger());
            return;
        }

        InvokeADED();
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
