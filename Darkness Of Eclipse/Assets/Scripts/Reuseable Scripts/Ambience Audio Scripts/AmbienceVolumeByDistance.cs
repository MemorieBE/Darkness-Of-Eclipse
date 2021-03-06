using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the volume of an ambience sound looper depending on the distance between two transforms.
 *
 *  References: AmbienceSoundLooper.
 */
public class AmbienceVolumeByDistance : MonoBehaviour
{
    [Header("Distance Between Transforms")]
    [SerializeField] private Transform transform1; //!< The first transform.
    [SerializeField] private Transform transform2; //!< The second transform.

    [Header("Audio Source")]
    [SerializeField] private AmbienceSoundLooper ambienceAudio; //!< The ambience sound looper

    [Header("Inputs")]
    [Range(0f, 1f)] [SerializeField] private float maxVolume = 1f; //!< The maximum volume.
    [Range(0f, 1f)] [SerializeField] private float minVolume = 0f; //!< The minimum volume.
    [SerializeField] private float maxDistance = 50f; //!< The maximum distance.
    [SerializeField] private float minDistance = 0f; //!< The minimum distance.

    [Header("Inverter")]
    [SerializeField] private bool inverted = false; //!< A boolean that controls whether or not the volume changes are inverted.

    private float distance; //!< The current distance from the first transform to the second transform.

    void Update()
    {
        distance = Vector3.Distance(transform1.position, transform2.position);

        if (distance <= minDistance)
        {
            if (inverted) ambienceAudio.masterVolume = minVolume;
            else ambienceAudio.masterVolume = maxVolume;
        }
        else if (distance >= maxDistance)
        {
            if (inverted) ambienceAudio.masterVolume = maxVolume;
            else ambienceAudio.masterVolume = minVolume;
        }
        else
        {
            if (inverted) ambienceAudio.masterVolume = minVolume + (((distance - minDistance) / (maxDistance - minDistance)) * (maxVolume - minVolume));
            else ambienceAudio.masterVolume = maxVolume - (((distance - minDistance) / (maxDistance - minDistance)) * (maxVolume - minVolume));
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(transform1.position, minDistance);
        Gizmos.DrawWireSphere(transform1.position, maxDistance);
    }
}
