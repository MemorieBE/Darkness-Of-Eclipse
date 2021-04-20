using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the volume of an audio source depending on the distance between two transforms.
 *
 *  [Reusable Script]
 */
public class VolumeByDistance : MonoBehaviour
{
    [Header("Distance Between Objects")]
    public Transform transform1; //!< The first transform.
    public Transform transform2; //!< The second transform.

    [Header("Audio Source")]
    public AudioSource audioSource; //!< The audio source.

    [Header("Inputs")]
    public float maxVolume = 1f; //!< The maximum volume.
    public float minVolume = 0f; //!< The minimum volume.
    public float maxDistance = 50f; //!< The maximum distance.
    public float minDistance = 0f; //!< The minimum distance.

    [Header("Inverter")]
    public bool inverted = false; //!< A boolean that controls whether or not the volume changes are inverted.

    private float distance; //!< The current distance from the first transform to the second transform.

    void Update()
    {
        distance = Vector3.Distance(transform1.position, transform2.position);

        if (distance <= minDistance)
        {
            if (inverted) audioSource.volume = minVolume;
            else audioSource.volume = maxVolume;
        }
        else if (distance >= maxDistance)
        {
            if (inverted) audioSource.volume = maxVolume;
            else audioSource.volume = minVolume;
        }
        else
        {
            if (inverted) audioSource.volume = minVolume + (((distance - minDistance) / (maxDistance - minDistance)) * (maxVolume - minVolume));
            else audioSource.volume = maxVolume - (((distance - minDistance) / (maxDistance - minDistance)) * (maxVolume - minVolume));
        }
    }
}
