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
    public bool hasLockedAnimations = false; //!< A boolean that controls whether or not the door has a locked animation.

    [Header("Audio")]
    public AudioSource openAudio; //!< The open audio source.
    public AudioSource closeAudio; //!< The close audio source.
    public AudioSource lockedAudio; //!< The locked audio source.

    [Header("Inputs")]
    public bool locked = false; //!< A boolean that controls whether or not the is locked.
    public bool open = false; //!< A boolean that controls whether or not the is open.

    private bool openUpdate = false; //!< A boolean used to play open and close audio.

    void Start()
    {
        if (open)
        {
            doorAnimator.Play("Door Idle Open");
        }
        else if (hasLockedAnimations)
        {
            doorAnimator.Play("Door Idle Close (Locked)");
        }

        openUpdate = open;
    }

    void Update()
    {
        if (locked)
        {
            if (hasLockedAnimations) doorAnimator.SetBool("DoorUnlocked", false);
        }
        else
        {
            if (hasLockedAnimations) doorAnimator.SetBool("DoorUnlocked", true);
        }

        if (open)
        {
            doorAnimator.SetBool("DoorOpen", true);

            if (openUpdate != open)
            {
                openAudio.Play();
                openUpdate = open;
            }
        }
        else
        {
            doorAnimator.SetBool("DoorOpen", false);

            if (openUpdate != open)
            {
                closeAudio.Play();
                openUpdate = open;
            }
        }

        if (gameObject.GetComponent<Interactable>().interacted) Interact();
    }

    //! A method that is activated when the player interacts with the gate using the Interactable script.
    private void Interact()
    {
        if (!locked)
        {
            if (open) open = false;
            else open = true;
        }
        else
        {
            if (lockedAudio == null) { return; }
            lockedAudio.Play();
        }
    }
}
