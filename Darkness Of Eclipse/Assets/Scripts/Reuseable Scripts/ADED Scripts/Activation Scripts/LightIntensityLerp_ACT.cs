using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the light intensity lerp value on activation.
 *
 *  [Reusable Script]
 */
public class LightIntensityLerp_ACT : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private LightIntensitySmoothLerp lightScript; //!< The light script.
    [SerializeField] private float setLightIntensity; //!< The light intensity to change to on activation.

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        lightScript.intensity = setLightIntensity;
    }
}
