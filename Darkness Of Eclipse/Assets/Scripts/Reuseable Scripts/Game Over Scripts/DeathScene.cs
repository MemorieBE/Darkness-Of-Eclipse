using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/*! \brief A script that plays a random animation after death.
 *
 *  References: GameRules, SceneLoader.
 */
public class DeathScene : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private Animator animator; //!< The animator that holds all the death animations.
    [SerializeField] private SceneLoader sceneLoader; //!< The scene loading script.

    [Header("Death Animations")]
    [SerializeField] private float[] deathChances; //!< The stare death probabilities.
    [SerializeField] private AnimationClip[] deathAnimations; //!< The stare death audio sources.

    [Header("Alternate Death Scene")]
    [SerializeField] private AnimationClip alternateAnimation; //!< The alternate death scene animation.
    public static bool alternateDeathScene = true; //!< A boolean that controls whether or the alternate death scene animation will be used instead.

    /*!
     *  A method that is triggers a death scene.
     */
    public void TriggerDeathScene()
    {
        StartCoroutine(StartRandomDeathScene());
    }

    /*!
     *  A coroutine that picks and play a random death scene.
     */
    private IEnumerator StartRandomDeathScene()
    {
        GameRules.CancelAllInput();
        Time.timeScale = 0f;

        AudioSource[] masterAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].Stop();
        }

        if (alternateDeathScene)
        {
            animator.PlayInFixedTime(alternateAnimation.name);

            yield return new WaitForSecondsRealtime(alternateAnimation.length);
        }
        else
        {
            int stareDeathOutcome = 0;

            float totalChance = 0f;
            for (int i = 0; i < deathChances.Length; i++)
            {
                totalChance += deathChances[i];
            }

            float diceRoll = Random.Range(0f, totalChance);

            float currentChance = 0f;
            for (int i = 0; i < deathChances.Length; i++)
            {
                currentChance += deathChances[i];

                if (currentChance > diceRoll)
                {
                    stareDeathOutcome = i;

                    break;
                }
            }

            animator.PlayInFixedTime(deathAnimations[stareDeathOutcome].name);

            yield return new WaitForSecondsRealtime(deathAnimations[stareDeathOutcome].length);
        }

        sceneLoader.ReloadScene();
    }
}
