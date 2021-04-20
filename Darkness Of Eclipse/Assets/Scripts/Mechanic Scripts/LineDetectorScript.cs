using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the line detectors.
 *
 *  [Mechanic Script]
 */
public class LineDetectorScript : MonoBehaviour
{
    [Header("Arrays")]
    public GameObject[] detectingChild; //!< The line detecting child game objects.
    public AudioSource[] lineAudio; //!< The audio sources for the line audios.
    public bool[] isSolo; //!< Booleans that controls whether or not the corresponding line audio is meant to be played as soon as it's triggered.

    private TriggerDetectionEnter[] detectionScript; //!< The detection scripts.

    void Start()
    {
        detectionScript = new TriggerDetectionEnter[detectingChild.Length];

        for (int i = 0; i < detectingChild.Length; i++)
        {
            detectionScript[i] = detectingChild[i].GetComponent<TriggerDetectionEnter>();
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < detectingChild.Length; i++)
        {
            if (detectionScript[i].activated)
            {
                detectingChild[i].SetActive(false);

                if (isSolo[i])
                {
                    for (int j = 0; j < detectingChild.Length; j++)
                    {
                        lineAudio[j].Stop();
                    }
                }

                lineAudio[i].Play();
            }
        }
    }
}
