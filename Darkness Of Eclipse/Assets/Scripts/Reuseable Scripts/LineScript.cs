using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the line detectors.
 *
 *  [Reusable Script]
 */
public class LineScript : MonoBehaviour
{
    [Header("Arrays")]
    public GameObject[] lineDetector; //!< The line detecting child game objects.
    public float[] lineTime; //!< The amount of time after the previous line until the next line will play.
    public AudioSource[] lineAudio; //!< The audio sources for the line audios.
    public bool[] isSolo; //!< Booleans that controls whether or not the corresponding line audio is meant to be played as soon as it's triggered.

    [Header("Inputs")]
    public bool isStacking; //!< A boolean that controls whether or not the lines stack.

    private bool[] activated; //!< Booleans that determine whether or not a line is currently active.
    private bool playingLine = false; //!< A boolean that determines whether or not any lines are currently playing.
    private float lineTimer = 0f; //!< The line timer.
    private int lineOrder = 0; //!< An integer that controls the order of the lines.

    private TriggerDetectionEnter[] detectionScript; //!< The detection scripts.

    void Awake()
    {
        detectionScript = new TriggerDetectionEnter[lineDetector.Length];
        for (int i = 0; i < lineDetector.Length; i++)
        {
            if (lineDetector[i] != null)
            {
                detectionScript[i] = lineDetector[i].GetComponent<TriggerDetectionEnter>();
            }
        }

        activated = new bool[lineAudio.Length];
        for (int i = 0; i < activated.Length; i++)
        {
            activated[i] = false;
        }
    }

    void Update()
    {
        if (!PauseScript.isPaused)
        {
            playingLine = false;
        }
        for (int i = 0; i < lineAudio.Length; i++)
        {
            if (lineAudio[i].isPlaying)
            {
                playingLine = true;
            }
        }

        if (lineOrder < lineAudio.Length && lineTime[lineOrder] > 0f && lineTimer >= lineTime[lineOrder])
        {
            lineTimer = 0f;
            activated[lineOrder] = true;
            lineOrder++;
        }

        for (int i = 0; i < lineDetector.Length; i++)
        {
            if (lineDetector[i] != null)
            {
                if (detectionScript[i].activated)
                {
                    lineTimer = 0f;
                    activated[i] = true;
                    lineOrder = i++;
                }
            }
        }

        for (int i = 0; i < lineAudio.Length; i++)
        {
            if (activated[i])
            {
                if (isSolo[i])
                {
                    for (int j = 0; j < lineAudio.Length; j++)
                    {
                        lineAudio[j].Stop();
                        playingLine = false;
                        activated[i] = false;
                    }
                }

                if (!playingLine || isStacking)
                {
                    lineAudio[i].Play();
                    playingLine = true;
                    activated[i] = false;
                }
            }
        }

        if (lineOrder < lineAudio.Length)
        {
            lineTimer += Time.deltaTime;
        }
    }
}
