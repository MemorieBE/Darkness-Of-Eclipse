using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the background ambience on activation.
 *
 *  [Reusable Script]
 */
public class BackgroundAmbience_ACT : MonoBehaviour
{
    [Header("Ambience Sound Looper")]
    [SerializeField] private AmbienceSoundLooper soundSource; //!< The ambience audio script.
    [SerializeField] private int setAudioNumber; //!< The ambience audio ID to play on activation.

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        soundSource.currentBackgroundAmbience = setAudioNumber;
    }
}
