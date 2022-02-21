using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls the open transition at the start of a scene or checkpoint.
 *
 *  Independent
 */
public class OpenTransition : MonoBehaviour
{
    [Header("Assets")]
    public Image blackUI; //!< The black UI panel to fade out.

    [Header("Inputs")]
    public float fadeTime = 3f; //!< The amount of time in seconds it will take for the black UI panel to fade out.

    void Start()
    {
        StartCoroutine(Transition());
    }

    /*!
     *  A coroutine that transitions out of a scene.
     */

    IEnumerator Transition()
    {
        Color uIColour = blackUI.color;

        for (float alpha = 1f; alpha > 0f; alpha -= 1f / fadeTime * Time.deltaTime)
        {
            uIColour.a = alpha;
            blackUI.color = uIColour;

            yield return null;
        }

        uIColour.a = 0f;
        blackUI.color = uIColour;
    }
}
