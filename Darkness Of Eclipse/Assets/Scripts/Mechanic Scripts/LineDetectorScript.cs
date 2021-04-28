using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the line detectors.
 *
 *  [Reusable Script]
 */
public class LineDetectorScript : MonoBehaviour
{
    [Header("Arrays")]
    public GameObject[] detectingChild; //!< The line detecting child game objects.
    public AudioSource[] lineAudio; //!< The audio sources for the line audios.
    public bool[] isSolo; //!< Booleans that controls whether or not the corresponding line audio is meant to be played as soon as it's triggered.

    [Header("Inputs")]
    public bool isStacking; //!< A boolean that controls whether or not the lines stack.

    private bool[] activated; //!< Booleans that determine whether or not a line is currently active.
    private bool playingLine = false; //!< A boolean that determines whether or not any lines are currently playing.

    private TriggerDetectionEnter[] detectionScript; //!< The detection scripts.

    void Start()
    {
        detectionScript = new TriggerDetectionEnter[detectingChild.Length];

        for (int i = 0; i < detectingChild.Length; i++)
        {
            detectionScript[i] = detectingChild[i].GetComponent<TriggerDetectionEnter>();
        }

        activated = new bool[detectingChild.Length];
    }

    void Update()
    {
        playingLine = false;
        for (int i = 0; i < detectingChild.Length; i++)
        {
            if (lineAudio[i].isPlaying)
            {
                playingLine = true;
            }
        }

        for (int i = 0; i < detectingChild.Length; i++)
        {
            if (detectionScript[i].activated) { activated[i] = true; }

            if (activated[i])
            {
                if (isSolo[i])
                {
                    for (int j = 0; j < detectingChild.Length; j++)
                    {
                        lineAudio[j].Stop();
                        activated[j] = false;
                    }
                }

                if (!playingLine || isStacking)
                {
                    lineAudio[i].Play();
                    activated[i] = false;
                }
            }
        }
    }
}
