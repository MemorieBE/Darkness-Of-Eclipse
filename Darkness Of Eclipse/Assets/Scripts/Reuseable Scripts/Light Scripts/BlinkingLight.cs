using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a blinking light.
 *
 *  Independent
 */
public class BlinkingLight : MonoBehaviour
{
    [Header("Light")]
    public Light blinkingLight; //!< The light being controled.

    [Header("Inputs")]
    public float setIntensity = 1f; //!< The maximum intensity The light will fade to.
    public float blinkSpeed = 1f; //!< The speed that the light will blink at.
    public float blinkRange = 50f; //!< The intensity range from 1 to 100 to calculate the minimum intensity.
    public bool isRandom = true; //!< A boolean that controls whether or not the light is blinking randomly.

    private float intensityPoint = 0f; //!< The set intensity the light is currently fading towards.
    private bool blinkRaising = false; //!< A boolean that determines whether or not the light is fading upwards.

    void Start()
    {
        if (isRandom)
        {
            intensityPoint = Random.Range(setIntensity * blinkRange * 0.01f, setIntensity);
        }
        else
        {
            intensityPoint = setIntensity * blinkRange * 0.01f;
        }
    }

    void Update()
    {
        if ((blinkingLight.intensity >= intensityPoint && blinkRaising) || (blinkingLight.intensity <= intensityPoint && !blinkRaising))
        {
            if (isRandom)
            {
                if (blinkRaising)
                {
                    intensityPoint = Random.Range(setIntensity - setIntensity * blinkRange * 0.01f, intensityPoint);
                }
                else
                {
                    intensityPoint = Random.Range(intensityPoint, setIntensity);
                }
            }
            else
            {
                if (blinkRaising)
                {
                    intensityPoint = setIntensity - setIntensity * blinkRange * 0.01f;
                }
                else
                {
                    intensityPoint = setIntensity;
                }
            }

            blinkRaising = !blinkRaising;
        }

        if (blinkRaising) blinkingLight.intensity += blinkSpeed * Time.deltaTime;
        else blinkingLight.intensity -= blinkSpeed * Time.deltaTime;
    }
}
