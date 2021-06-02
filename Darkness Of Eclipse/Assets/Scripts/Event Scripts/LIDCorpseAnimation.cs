using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the corpse animation in the Locked In Despair scene.
 *
 *  [Event Script]
 */
public class LIDCorpseAnimation : MonoBehaviour
{
    [Header("Assets")]
    public Animator corpseAnimator; //!< The corpse animator.
    public AudioSource corpseFlopAudio; //!< The audio source for the corpse flopping sound.
    public DoorScript doorScript; //!< The script that controls the door.

    private bool audioPlayed = false; //!< A boolean that determines whether or not the audio has been played.

    void Update()
    {
        if (doorScript.open)
        {
            doorScript.locked = true;

            if (!audioPlayed)
            {
                corpseFlopAudio.Play();
                audioPlayed = true;
            }

            corpseAnimator.SetBool("Fallen", true);
        }
    }
}
