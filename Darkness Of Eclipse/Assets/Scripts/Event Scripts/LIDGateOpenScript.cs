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

    private bool noRepeat = true; //!< A boolean that determines whether or not the gate hasn't been open.

    void Start()
    {
        noRepeat = true;
    }

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        if ((GlobalUnverKeyScript.keyCount >= 7) && (noRepeat))
        {
            gateOpenSound.Play();
            noRepeat = false;

            gateAnimator.SetBool("GateOpen", true);
        }
    }
}
