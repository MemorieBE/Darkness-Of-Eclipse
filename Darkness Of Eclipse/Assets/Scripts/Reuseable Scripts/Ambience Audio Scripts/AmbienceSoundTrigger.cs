using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the background ambience on activation.
 *
 *  References: AmbienceSoundLooper.
 */
public class AmbienceSoundTrigger : MonoBehaviour
{
    [Header("Ambience Sound Looper")]
    [SerializeField] private AmbienceSoundLooper soundSource; //!< The ambience audio script.
    [SerializeField] private int setAudioNumber; //!< The ambience audio ID to play on activation.

    /*!
     *  A method that sets the audio number.
     */
    public void SetAmbience()
    {
        soundSource.currentBackgroundAmbience = setAudioNumber;
    }
}
