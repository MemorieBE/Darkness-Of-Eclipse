using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]

/*! \brief A script that controls the light intensity and how it changes values in a smooth lerp.
 *
 *  [Reusable Script]
 */
public class LightIntensitySmoothLerp : MonoBehaviour
{
    public float intensity = 1f; //!< The target light intensity.
    public float transitionSpeed = 1f; //!< The light intensity transition speed.

    void Update()
    {
        if (gameObject.GetComponent<Light>().intensity + transitionSpeed * Time.deltaTime < intensity)
        {
            gameObject.GetComponent<Light>().intensity += transitionSpeed * Time.deltaTime;
        }
        else if (gameObject.GetComponent<Light>().intensity - transitionSpeed * Time.deltaTime > intensity)
        {
            gameObject.GetComponent<Light>().intensity -= transitionSpeed * Time.deltaTime;
        }
        else
        {
            gameObject.GetComponent<Light>().intensity = intensity;
        }
    }
}
