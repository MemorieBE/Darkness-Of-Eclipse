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

    private float newAlpha = 0f; //!< The updated alpha.

    void OnEnable()
    {
        StartCoroutine(TimeBasedPrompt());
    }

    /*!
     *  An IEnumerator that shows the UI prompt for a certain duration.
     */
    IEnumerator TimeBasedPrompt()
    {
        newAlpha = 0f;

        UpdateAlpha();

        yield return new WaitForSeconds(startTime);

        while (newAlpha < maxAlpha)
        {
            newAlpha += 1f / fadeTime * Time.deltaTime;
            if (newAlpha > maxAlpha) { newAlpha = maxAlpha; }

            UpdateAlpha();

            yield return null;
        }

        yield return new WaitForSeconds(stayTime);

        while (newAlpha > 0f)
        {
            newAlpha -= 1f / fadeTime * Time.deltaTime;
            if (newAlpha < 0f) { newAlpha = 0f; }

            UpdateAlpha();

            yield return null;
        }

        gameObject.SetActive(false);
    }

    /*!
     *  A method that updates the UI's alpha/opacity.
     */
    private void UpdateAlpha()
    {
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
    }
}
