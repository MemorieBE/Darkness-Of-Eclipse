using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the line detectors.
 *
 *  [Reusable Script]
 */
public class SmartLineController : MonoBehaviour
{
    [Header("Arrays")]
    [SerializeField] private AudioSource[] lineAudio; //!< The audio sources for the line audios.
    [SerializeField] private float[] lineAfterTime; //!< The amount of time in seconds before the next line will play.
    [SerializeField] private bool[] isSolo; //!< Booleans that control whether or not the corresponding line audio is meant to be played as soon as it's triggered.
    [SerializeField] private bool[] isStacking; //!< Boolean that control whether or not a line plays on top of others.

    [SerializeField] private bool playingLine = false; //!< A boolean that determines whether or not any lines are currently playing.
    [SerializeField] private bool activeLineTimer = false; //!< A boolean that determines whether or not the line timer is active.
    [SerializeField] private float lineBeforeTimer = 0f; //!< The pause before the line timer.
    [SerializeField] private float lineTimer = 0f; //!< The line timer.
    [SerializeField] private int[] lineOrder; //!< An array of integers that controls the order of the lines.

    void Awake()
    {
        lineOrder = new int[lineAudio.Length];

        for (int i = 0; i < lineAudio.Length; i++)
        {
            lineOrder[i] = 0;
        }
    }

    public void ActivateLine(int line)
    {
        if (isStacking[line])
        {
            lineAudio[line].Play();
            playingLine = true;

            lineBeforeTimer = lineAfterTime[line];
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
                lineBeforeTimer = lineAfterTime[line];
                activeLineTimer = true;
            }
            else if (isSolo[line])
            {
                for (int j = 0; j < lineAudio.Length; j++)
                {
                    lineAudio[j].Stop();
                    playingLine = false;

                    lineBeforeTimer = 0f;
                    lineOrder[j] = 0;
                }

                lineBeforeTimer = lineAfterTime[line];
            }

            lineOrder[line] = highestOrderValue + 1;
        }
    }

    void Update()
    {
        if (!playingLine && activeLineTimer)
        {
            if (lineBeforeTimer <= 0f)
            {
                lineBeforeTimer = 0f;

                for (int i = 0; i < lineAudio.Length; i++)
                {
                    if (lineOrder[i] == 1)
                    {
                        lineAudio[i].Play();
                        playingLine = true;
                        lineTimer = lineAudio[i].clip.length;

                        activeLineTimer = false;

                        for (int j = 0; j < lineAudio.Length; j++)
                        {
                            if (lineOrder[j] > 0)
                            {
                                lineOrder[j]--;

                                if (lineOrder[j] == 1)
                                {
                                    if (lineAfterTime[j] > 0)
                                    {
                                        lineBeforeTimer = lineAfterTime[j];
                                    }
                                    else
                                    {
                                        lineBeforeTimer = 0f;
                                    }

                                    activeLineTimer = true;
                                }
                            }
                        }

                        return;
                    }
                }

                playingLine = false;
            }
            else
            {
                lineBeforeTimer -= Time.deltaTime;
            }
        }
        else if (playingLine)
        {
            if (lineTimer <= 0f)
            {
                playingLine = false;

                lineTimer = 0f;
            }
            else
            {
                lineTimer -= Time.deltaTime;
            }
        }
    }
}
