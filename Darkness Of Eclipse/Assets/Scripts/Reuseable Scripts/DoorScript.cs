using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a door.
 *
 *  Independent
 */
public class DoorScript : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator doorAnimator; //!< The door animator.
    [SerializeField] private bool hasUnlockedAnimations = false; //!< A boolean that controls whether or not the door has a locked animation.

    [Header("Audio")]
    [SerializeField] private AudioSource openAudio; //!< The open audio source.
    [SerializeField] private AudioSource closeAudio; //!< The close audio source.
    [SerializeField] private AudioSource lockedAudio; //!< The locked audio source.

    [Header("Inputs")]
    public bool locked = false; //!< A boolean that controls whether or not the is locked.
    public bool open = false; //!< A boolean that controls whether or not the is open.

    private void OnEnable()
    {
        UpdateDoorAnimationDirect();
    }

    /*!
     *  A method that is triggered on elevation.
     */
    public void Elevated()
    {
        locked = !locked;

        UpdateDoorAnimation();
    }

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        if (!locked)
        {
            open = !open;

            UpdateDoorAnimation();
        }
        else
        {
            if (lockedAudio == null) { return; }
            lockedAudio.Play();
        }
    }

    /*!
     *  A method that updates the door animation parameters.
     */
    public void UpdateDoorAnimation()
    {
        if (open != doorAnimator.GetBool("DoorOpen")) 
        {
            if (open)
            {
                doorAnimator.SetBool("DoorOpen", true);

                openAudio.Play();
            }
            else
            {
                doorAnimator.SetBool("DoorOpen", false);

                closeAudio.Play();
            }
        }

        if (locked == doorAnimator.GetBool("DoorUnlocked"))
        {
            if (locked)
            {
                if (hasUnlockedAnimations) doorAnimator.SetBool("DoorUnlocked", false);
            }
            else
            {
                if (hasUnlockedAnimations) doorAnimator.SetBool("DoorUnlocked", true);
            }
        }
    }

    /*!
     *  A method that directly updates the door animation state.
     */
    public void UpdateDoorAnimationDirect()
    {
        if (open)
        {
            doorAnimator.Play("Door Idle Open");
            doorAnimator.SetBool("DoorOpen", true);
        }
        else if (!hasUnlockedAnimations || !locked)
        {
            doorAnimator.Play("Door Idle Close");
            doorAnimator.SetBool("DoorOpen", false);
        }
        else
        {
            doorAnimator.Play("Door Idle Close (Locked)");
            doorAnimator.SetBool("DoorOpen", false);
        }

        if (locked)
        {
            doorAnimator.SetBool("DoorUnlocked", false);
        }
        else
        {
            doorAnimator.SetBool("DoorUnlocked", true);
        }
    }
}
