using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that controls how the Unver kills the player.
 *
 *  [Mechanic Script]
 */
public class DeathByUnver : MonoBehaviour
{
    public PlayerToGhostDetector detectionScript; //!< The player to unver detection script.

    [Header("Death While Looking At Unver")]
    public float[] stareDeathChance; //!< The stare death probabilities.
    public AudioSource[] stareDeathAudio; //!< The stare death audio sources.
    public GameObject blackScreen; //!< The black screen to activate on death.

    private int stareDeathOutcome = 0; //!< The stare death integer chance outcome.
    private float currentAudioTime = 0f; //!< The audio time of the chosen audio source.
    private bool stareDeathTimerStarted = false; //!< A boolean that determines whether or not the stare death timer has started.
    private float stareDeathTimer = 0f; //!< The stare death timer.

    void Update()
    {
        if (detectionScript.playerDetected)
        {
            StareDeath();
        }

        if (stareDeathTimerStarted)
        {
            if (stareDeathTimer >= currentAudioTime)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }

            stareDeathTimer += Time.deltaTime;
        }
    }

    /*!
     *  A method that kills the player with a stare death scenario.
     */
    public void StareDeath()
    {
        float totalChance = 0f;
        for (int i = 0; i < stareDeathChance.Length; i++)
        {
            totalChance += stareDeathChance[i];
        }

        float diceRoll = Random.Range(0f, totalChance);

        float currentChance = 0f;
        for (int i = 0; i < stareDeathChance.Length; i++)
        {
            currentChance += stareDeathChance[i];

            if (currentChance > diceRoll)
            {
                stareDeathOutcome = i;

                break;
            }
        }

        currentAudioTime = stareDeathAudio[stareDeathOutcome].clip.length;

        stareDeathTimerStarted = true;

        blackScreen.SetActive(true);
    }

    /*!
     *  A method that kills the player with a sneak death scenario.
     */
    public void SneakDeath()
    {
        //Comming Soon...
    }
}
