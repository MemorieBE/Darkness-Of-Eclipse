using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

/*! \brief A script that shows a UI prompt on enable.
 *
 *  Independent
 */
public class ControlsTextPrompt : MonoBehaviour
{
    [Header("Inputs")]
    [Range(0f, 1f)] public float maxAlpha = 0.6f; //!< The max alpha for the UI.
    public float startTime = 1.5f; //!< The amount of time in seconds before the UI fades in.
    public float fadeTime = 2f; //!< The amount of time in seconds that the UI will fade for.
    public float stayTime = 2f; //!< The amount of time in seconds that the UI will stay for.

    [Header("Prompt")]
    [SerializeField] private Action action; //!< The prompt action.
    [SerializeField] private InputActionReference input; //!< The prompt input.
    [SerializeField] private string purpose; //!< The prompe purpose.

    private Text textUI; //!< The text UI.

    private bool inputCheck = false; //!< A boolean that determines whether the input has been performed this frame.

    private Action<InputAction.CallbackContext> inputHandler; //!< The input handler.

    private float newAlpha = 0f; //!< The updated alpha.

    private enum Action //!< An enumerator that lists the different actions in a prompt.
    { 
        Press, 
        Hold, 
        Release, 
        Mash 
    };

    void Awake()
    {
        inputHandler = ctx => inputCheck = true;

        textUI = gameObject.GetComponent<Text>();
    }

    void OnEnable()
    {
        input.action.performed += inputHandler;

        StartCoroutine(TimeBasedPrompt());
    }

    void OnDisable()
    {
        input.action.performed -= inputHandler;
    }

    /*!
     *  A coroutine that shows the UI prompt for a certain duration.
     */
    private IEnumerator TimeBasedPrompt()
    {
        if (input.action.bindings.Count == 0 || input.action.bindings[0].ToDisplayString() == "")
        {
            yield break;
        }

        textUI.text = action.ToString() + " " + input.action.bindings[0].ToDisplayString() + " To " + purpose;

        newAlpha = 0f;

        UpdateAlpha();

        for (float t = 0; t < startTime; t += Time.deltaTime)
        {
            if (inputCheck) { break; }

            yield return null;
        }

        while (newAlpha < maxAlpha)
        {
            if (inputCheck) { break; }

            newAlpha += 1f / fadeTime * Time.deltaTime;
            if (newAlpha > maxAlpha) { newAlpha = maxAlpha; }

            UpdateAlpha();

            yield return null;
        }

        for (float t = 0; t < stayTime; t += Time.deltaTime)
        {
            if (inputCheck) { break; }

            yield return null;
        }

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
        Color newColor = gameObject.GetComponent<Text>().color;
        newColor.a = newAlpha;
        textUI.color = newColor;
    }

    void LateUpdate()
    {
        if (inputCheck) inputCheck = false;
    }
}
