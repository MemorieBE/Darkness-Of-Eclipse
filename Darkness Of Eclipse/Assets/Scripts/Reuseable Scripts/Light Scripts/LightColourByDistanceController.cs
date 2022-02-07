using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/*! \brief A script that controls how lights change colour depending on a transform's position and how close it is to the target locations.
 *
 *  Independent
 */
public class LightColourByDistanceController : MonoBehaviour
{
    [Header("Main Assets")]
    [SerializeField] private Transform mainTransform; //!< The main transform.
    [SerializeField] private Light[] mainLights; //!< The main lights.
    [SerializeField] private Color mainColour; //!< The main colour.

    [Header("Colour Changers")]
    public List<LightColourByDistanceLocal> colourChangers;

    void Start()
    {
        colourChangers = new List<LightColourByDistanceLocal>();
    }

    void Update()
    {
        if (colourChangers.Count == 0)
        {
            for (int i = 0; i < mainLights.Length; i++)
            {
                mainLights[i].color = mainColour;
            }

            return;
        }

        float[] valueOfLocations = new float[colourChangers.Count];
        Color colourOutcome = mainColour;
        float maxValue = 1f;

        for (int i = 0; i < colourChangers.Count; i++)
        {
            valueOfLocations[i] = Mathf.Abs(Mathf.Clamp((Vector3.Distance(mainTransform.position, colourChangers[i].transform.position) - colourChangers[i].minRadius) / (colourChangers[i].maxRadius - colourChangers[i].minRadius), 0f, 1f) - 1f);
            maxValue += valueOfLocations[i];
            colourOutcome += colourChangers[i].colour * valueOfLocations[i];
        }

        colourOutcome = colourOutcome / maxValue;

        for (int i = 0; i < mainLights.Length; i++)
        {
            mainLights[i].color = colourOutcome;
        }
    }
}
