using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a door.
 *
 *  [Reusable Script]
 */
public class DoorScript : MonoBehaviour
{
    [Header("Animation")]
    public Animator doorAnimator; //!< The door animator.
    public bool hasUnlockedAnimations = false; //!< A boolean that controls whether or not the door has a locked animation.

    [Header("Audio")]
    public AudioSource openAudio; //!< The open audio source.
    public AudioSource closeAudio; //!< The close audio source.
    public AudioSource lockedAudio; //!< The locked audio source.

    [Header("Inputs")]
    public bool locked = false; //!< A boolean that controls whether or not the is locked.
    public bool open = false; //!< A boolean that controls whether or not the is open.

    void Start()
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

    /*!
     *  A method that is triggered on elevation.
     */
    public void Elevation()
    {
        locked = !locked;

        if (locked)
        {
            if (hasUnlockedAnimations) doorAnimator.SetBool("DoorUnlocked", false);
        }
        else
        {
            if (hasUnlockedAnimations) doorAnimator.SetBool("DoorUnlocked", true);
        }
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

    public void UpdateDoorAnimation()
    {
        if (open == doorAnimator.GetBool("DoorOpen")) { return; }

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
}
