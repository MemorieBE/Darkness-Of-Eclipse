using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the light intensity lerp value on deactivation.
 *
 *  [Reusable Script]
 */
public class LightIntensityLerp_DCT : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private LightIntensitySmoothLerp lightScript; //!< The light script.
    [SerializeField] private float setLightIntensity; //!< The light intensity to change to on deactivation.

    /*!
     *  A method that is triggered on deactivation.
     */
    public void Deactivated()
    {
        lightScript.intensity = setLightIntensity;
    }
}
