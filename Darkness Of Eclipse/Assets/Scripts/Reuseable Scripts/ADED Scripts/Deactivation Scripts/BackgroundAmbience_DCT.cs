using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the background ambience on deactivation.
 *
 *  [Reusable Script]
 */
public class BackgroundAmbience_DCT : MonoBehaviour
{
    [Header("Ambience Sound Looper")]
    [SerializeField] private AmbienceSoundLooper soundSource; //!< The ambience audio script.
    [SerializeField] private int setAudioNumber; //!< The ambience audio ID to play on deactivation.

    /*!
     *  A method that is triggered on deactivation.
     */
    public void Deactivated()
    {
        soundSource.currentBackgroundAmbience = setAudioNumber;
    }
}
