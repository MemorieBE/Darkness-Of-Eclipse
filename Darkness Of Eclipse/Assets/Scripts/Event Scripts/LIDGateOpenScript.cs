using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the gate doors and whether or not they are open/locked in the Locked In Despair scene.
 *
 *  [Event Script]
 */
public class LIDGateOpenScript : MonoBehaviour
{
    [Header("Assets")]
    public Animator gateAnimator; //!< The gate animator.
    public AudioSource gateOpenSound; //!< The audio source for the gate opening audio.

    private bool open = false; //!< A boolean that determines whether or not the gate is open.
    private bool noRepeat = true; //!< A boolean that determines whether or not the gate hasn't been open.

    void Start()
    {
        noRepeat = true;
    }

    void Update()
    {
        if (open) gateAnimator.SetBool("GateOpen", true);

        if (gameObject.GetComponent<Interactable>().interacted) Interact();
    }

    /*!
     *  A method that is activated when the player interacts with the gate using the Interactable script.
     */
    private void Interact()
    {
        if ((GlobalUnverKeyScript.keyCount >= 7) && (noRepeat))
        {
            open = true;
            gateOpenSound.Play();
            noRepeat = false;
        }
    }
}
