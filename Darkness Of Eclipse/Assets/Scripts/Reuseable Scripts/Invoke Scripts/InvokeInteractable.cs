using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that invokes a function to certain scripts when the player has interacted with the game object.
 *
 *  Independent
 */
public class InvokeInteractable : MonoBehaviour
{
    [Header("Interact UI")]
    public string prompt = "Interact"; //!< The text prompt that will show up when hovering over game object.
    public Sprite sprite; //!< The sprite that will show up when hovering over game object.

    [Header("Invoke")]
    [SerializeField] private MonoBehaviour[] scripts; //!< The script that will be effected.
    [SerializeField] private string[] invokes; //!< The name of the functions to invoke.

    [Header("Inputs")]
    [SerializeField] private bool disableOnActivation = false; //!< A boolean that controls whether or not the detector will be disabled when it is activated.
    [SerializeField] private bool resetsActivation = true; //!< A boolean that controls whether or not the deactivation will reset after it has been deactivated.
    [SerializeField] private float delay = 0f; //!< The amount of time in seconds the trigger will be delayed.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    /*!
     *  A method that is triggered when interacted.
     */
    public void Interacted()
    {
        if (activated) { return; }

        InvokeFunction();
    }

    /*!
     *  A method that invokes the.
     */
    private void InvokeFunction()
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
