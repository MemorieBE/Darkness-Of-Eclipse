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
    [SerializeField] private Animation[] deathAnimations; //!< The stare death audio sources.

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        StartCoroutine(StartRandomDeathScene());
    }

    /*!
     *  A coroutine that kills the player with a stare death scenario.
     */
    public IEnumerator StartRandomDeathScene()
    {
        GameRules.CancelAllInput();
        Time.timeScale = 0f;

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

        AudioSource[] masterAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].Stop();
        }

        animator.PlayInFixedTime(deathAnimations[stareDeathOutcome].name);

        yield return new WaitForSecondsRealtime(deathAnimations[stareDeathOutcome].clip.length);

        sceneLoader.ReloadScene();
    }
}
