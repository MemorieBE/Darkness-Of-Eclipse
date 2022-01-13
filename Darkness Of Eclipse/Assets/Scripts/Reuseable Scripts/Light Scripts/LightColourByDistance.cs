using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*! \brief A script that controls how lights change colour depending on a transform's position and how close it is to the target locations.
 *
 *  Independent
 */
public class LightColourByDistance : MonoBehaviour
{
    [Header("Main Assets")]
    [SerializeField] private Transform mainTransform; //!< The main transform.
    [SerializeField] private Light[] mainLights; //!< The main lights.
    [SerializeField] private Color mainColour; //!< The main colour.

    [Header("Colour Changing Locations")]
    [SerializeField] private Transform[] transformOfLocations; //!< The transforms of the colour changing locations.
    [SerializeField] private Color[] colourOfLocations; //!< The colours of the colour changing locations.
    [SerializeField] private float[] maxRadiusOfLocations; //!< The maximum radius of the colour changing locations.
    [SerializeField] private float[] minRadiusOfLocations; //!< The minimum radius of the colour changing locations.

    void Update()
    {
        float[] valueOfLocations = new float[transformOfLocations.Length];
        Color colourOutcome = mainColour;
        float maxValue = 1f;

        for (int i = 0; i < transformOfLocations.Length; i++)
        {
            valueOfLocations[i] = Mathf.Abs(Mathf.Clamp((Vector3.Distance(mainTransform.position, transformOfLocations[i].position) - minRadiusOfLocations[i]) / (maxRadiusOfLocations[i] - minRadiusOfLocations[i]), 0f, 1f) - 1f);
            maxValue += valueOfLocations[i];
            colourOutcome += colourOfLocations[i] * valueOfLocations[i];
        }

        colourOutcome = colourOutcome / maxValue;

        for (int i = 0; i < mainLights.Length; i++)
        {
            mainLights[i].color = colourOutcome;
        }
    }

    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < transformOfLocations.Length; i++)
        {
            Gizmos.color = colourOfLocations[i];
            Gizmos.DrawWireSphere(transformOfLocations[i].position, maxRadiusOfLocations[i]);
            Gizmos.DrawWireSphere(transformOfLocations[i].position, minRadiusOfLocations[i]);
        }
    }
}
