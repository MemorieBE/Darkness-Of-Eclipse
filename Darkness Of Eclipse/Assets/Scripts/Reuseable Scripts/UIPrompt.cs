using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that shows a UI prompt on enable.
 *
 *  [Reusable Script]
 */
public class UIPrompt : MonoBehaviour
{
    [Header("Inputs")]
    [Range(0f, 1f)] public float maxAlpha = 0.6f; //!< The max alpha for the UI.
    public float startTime = 1.5f; //!< The amount of time in seconds before the UI fades in.
    public float fadeTime = 2f; //!< The amount of time in seconds that the UI will fade for.
    public float stayTime = 2f; //!< The amount of time in seconds that the UI will stay for.

    private float timer = 0f; //!< The UI timer.
    private float newAlpha = 0f; //!< The updated alpha.

    void OnEnable()
    {
        timer = 0f;
    }

    void Update()
    {
        if (timer <= startTime)
        {
            newAlpha = 0f;
        }
        else if (timer <= startTime + fadeTime)
        {
            newAlpha = ((timer - startTime) / fadeTime) * maxAlpha;
        }
        else if (timer <= startTime + fadeTime + stayTime)
        {
            newAlpha = maxAlpha;
        }
        else if (timer <= startTime + fadeTime * 2f + stayTime)
        {
            newAlpha = maxAlpha - ((timer - startTime - fadeTime - stayTime) / fadeTime) * maxAlpha;
        }
        else
        {
            gameObject.SetActive(false);
        }

        if (gameObject.GetComponent<Text>() != null)
        {
            Color newColor = gameObject.GetComponent<Text>().color;
            newColor.a = newAlpha;
            gameObject.GetComponent<Text>().color = newColor;
        }

        if (gameObject.GetComponent<Image>() != null)
        {
            Color newColor = gameObject.GetComponent<Image>().color;
            newColor.a = newAlpha;
            gameObject.GetComponent<Image>().color = newColor;
        }

        timer += Time.deltaTime;
    }
}
