using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the line detectors.
 *
 *  Independent
 */
public class SmartLineController : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] private AudioSource[] lineAudio; //!< The audio sources for the line audios.
    [SerializeField] private float[] lineAfterTime; //!< The amount of time in seconds before the next line will play.
    [SerializeField] private bool[] isSolo; //!< Booleans that control whether or not the corresponding line audio is meant to be played as soon as it's triggered.
    [SerializeField] private bool[] isStacking; //!< Boolean that control whether or not a line plays on top of others.

    [Header("Subtitles")]
    [SerializeField] private DialogueSubtitles subtitles; //!< The subtitles script.
    [SerializeField] private int scene = 0; //!< The line scene.
    [SerializeField] private int interaction = 0; //!< The line interaction.

    private bool playingLine = false; //!< A boolean that determines whether or not any lines are currently playing.
    private float beforeLineTimer = 0f; //!< The pause before the line timer.
    private float lineTimer = 0f; //!< The line timer.
    private int[] lineOrder; //!< An array of integers that controls the order of the lines.

    void Awake()
    {
        lineOrder = new int[lineAudio.Length];

        for (int i = 0; i < lineAudio.Length; i++)
        {
            lineOrder[i] = 0;
        }
    }

    /*!
     *  A method that activates a smart line.
     *  
     *  \param line The line ID.
     */
    public void ActivateLine(int line)
    {
        if (isStacking[line])
        {
            lineAudio[line].Play();
            playingLine = true;

            beforeLineTimer = lineAfterTime[line];

            if (subtitles != null)
            {
                subtitles.ActivateSubtitle(scene, interaction, line);
            }
        }
        else
        {
            int highestOrderValue = 0;
            for (int i = 0; i < lineOrder.Length; i++)
            {
                if (lineOrder[i] > highestOrderValue) { highestOrderValue = lineOrder[i]; }
            }

            if (highestOrderValue == 0) 
            {
                beforeLineTimer = lineAfterTime[line];
            }
            else if (isSolo[line])
            {
                for (int j = 0; j < lineAudio.Length; j++)
                {
                    lineAudio[j].Stop();
                    playingLine = false;

                    beforeLineTimer = 0f;
                    lineOrder[j] = 0;
                }

                beforeLineTimer = lineAfterTime[line];
            }

            lineOrder[line] = highestOrderValue + 1;

            StartCoroutine(PlayNewLine(line));
        }
    }

    private IEnumerator PlayNewLine(int line)
    {
        while (playingLine || lineOrder[line] > 1)
        {
            yield return null;
        }

        yield return new WaitForSeconds(beforeLineTimer);

        for (int i = 0; i < lineAudio.Length; i++)
        {
            if (lineOrder[i] == 1)
            {
                lineAudio[i].Play();
                playingLine = true;
                lineTimer = lineAudio[i].clip.length;

                if (subtitles != null)
                {
                    subtitles.ActivateSubtitle(scene, interaction, line);
                }

                for (int j = 0; j < lineAudio.Length; j++)
                {
                    if (lineOrder[j] > 0)
                    {
                        lineOrder[j]--;

                        if (lineOrder[j] == 1)
                        {
                            if (lineAfterTime[j] > 0)
                            {
                                beforeLineTimer = lineAfterTime[j];
                            }
                            else
                            {
                                beforeLineTimer = 0f;
                            }
                        }
                    }
                }

                break;
            }
        }

        yield return new WaitForSeconds(lineTimer);

        playingLine = false;
    }
}
