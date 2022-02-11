using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnableDisable))]

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

    [HideInInspector] public bool animationPlayed = false; //!< A boolean that determines whether or not the audio has been played.

    void OnEnable()
    {
        if (animationPlayed) { corpseAnimator.Play("Idle Fallen"); }
    }

    void Update()
    {
        if (doorScript.open && !animationPlayed)
        {
            animationPlayed = true;

            doorScript.locked = true;

            corpseFlopAudio.Play();

            corpseAnimator.SetBool("Fallen", true);

            gameObject.GetComponent<EnableDisable>().PositiveTrigger();
        }
    }
}
