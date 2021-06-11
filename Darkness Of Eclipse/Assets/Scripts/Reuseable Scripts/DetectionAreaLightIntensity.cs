using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerDetectionStay))]

/*! \brief A script that controls the light intensity depending on whether the player is in a certain trigger.
 *
 *  [Reusable Script]
 */
public class DetectionAreaLightIntensity : MonoBehaviour
{
    [Header("Light")]
    public LightIntensitySmoothLerp lightScript; //!< The light script.
    public float setLightIntensityEnter; //!< The light intensity to change to on trigger enter.
    public float setLightIntensityExit; //!< The light intensity to change to on trigger exit.

    private TriggerDetectionStay detectionScript; //!< The trigger detection script.

    void Start()
    {
        detectionScript = gameObject.GetComponent<TriggerDetectionStay>();
    }

    void Update()
    {
        if (detectionScript.enter) lightScript.intensity = setLightIntensityEnter;
        if (detectionScript.exit) lightScript.intensity = setLightIntensityExit;
    }
}
