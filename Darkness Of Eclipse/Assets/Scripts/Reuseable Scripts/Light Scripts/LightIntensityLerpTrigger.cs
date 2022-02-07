using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the light intensity lerp value on activation.
 *
 *  References: LightIntensitySmoothLerp.
 */
public class LightIntensityLerpTrigger : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private LightIntensitySmoothLerp lightScript; //!< The light script.
    [SerializeField] private float setLightIntensity; //!< The light intensity to change to on activation.

    /*!
     *  A method that sets the intensity.
     */
    public void SetIntensity()
    {
        lightScript.intensity = setLightIntensity;
    }
}
