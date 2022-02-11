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
    [SerializeField] private Animator gateAnimator; //!< The gate animator.
    [SerializeField] private AudioSource gateOpenSound; //!< The audio source for the gate opening audio.

    [HideInInspector] public bool open = false; //!< A boolean that determines whether or not the gate hasn't been open.

    void OnEnable()
    {
        if (open) { gateAnimator.Play("Gate Idle Open"); }
    }

    /*!
     *  A method that is triggered on activation.
     */
    public void TriggerGate()
    {
        if ((GlobalUnverKeyScript.keyCount >= 7) && (!open))
        {
            gateOpenSound.Play();
            open = true;

            gateAnimator.SetBool("GateOpen", true);
        }
    }
}
