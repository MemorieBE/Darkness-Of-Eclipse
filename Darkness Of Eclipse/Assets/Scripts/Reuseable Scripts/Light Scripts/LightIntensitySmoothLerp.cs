using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]

/*! \brief A script that controls the light intensity and how it changes values in a smooth lerp.
 *
 *  Independent
 */
public class LightIntensitySmoothLerp : MonoBehaviour
{
    [Header("Inputs")]
    public float intensity = 1f; //!< The target light intensity.
    [SerializeField] private float transitionSpeed = 1f; //!< The light intensity transition speed.

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
