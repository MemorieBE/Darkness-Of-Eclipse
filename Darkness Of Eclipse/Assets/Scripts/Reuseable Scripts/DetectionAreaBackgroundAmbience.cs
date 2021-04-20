using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerDetectionStay))]

/*! \brief A script that controls the background ambience depending on whether the player is in a certain trigger.
 *
 *  [Reusable Script]
 */
public class DetectionAreaBackgroundAmbience : MonoBehaviour
{
    [Header("Ambience Sound Looper")]
    public AmbienceSoundLooper soundSource; //!< The ambience audio script.
    public int setAudioNumberEnter; //!< The ambience audio ID to play on trigger enter.
    public int setAudioNumberExit; //!< The ambience audio ID to play on trigger exit.

    private TriggerDetectionStay detectionScript; //!< The trigger detection script.

    void Start()
    {
        detectionScript = gameObject.GetComponent<TriggerDetectionStay>();
    }

    void Update()
    {
        if (detectionScript.activated) soundSource.currentBackgroundAmbience = setAudioNumberEnter;
        else soundSource.currentBackgroundAmbience = setAudioNumberExit;
    }
}
