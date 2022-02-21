using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*! \brief A script that controls the prompt UI state.
 *
 *  References: GameRules.
 */
public class UIPopUp : MonoBehaviour
{
    [Header("Action")]
    [SerializeField] private InputActionReference escapeAction; //!< The escape action.

    [Header("Inputs")]
    [SerializeField] private bool pause = true; //!< A boolean that controls whether or not the pop up will pause the game.

    private bool closeUILate = false; //!< A boolean that closes the UI in the late update.

    private AudioSource[] masterAudio; //!< All audio sources to pause.

    private CursorLockMode mouseLock; //!< The cursor lock mode that the cursor will set itself to when unpaused.
    private bool mouseVisable; //!< A boolean that determines whether or not the cursor is visable that the cursor will set itself to when unpaused.

    private bool isActive = false; //!< A boolean that determines whether or not the prompt is active.

    void OnEnable()
    {
        escapeAction.action.performed += EscapeUI;
        escapeAction.action.Enable();

        PromptActivate();
    }

    void OnDisable()
    {
        escapeAction.action.performed -= EscapeUI;
        escapeAction.action.Disable();
    }

    /*!
     *  A method that activates a prompt.
     */
    private void PromptActivate()
    {
        isActive = true;

        if (GameRules.cancelInputOverride > 0) { GameRules.CancelAllInput(); }
        GameRules.cancelInputOverride++;

        masterAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];

        if (pause) 
        { 
            Time.timeScale = 0f;

            for (int i = 0; i < masterAudio.Length; i++)
            {
                masterAudio[i].Pause();
            }
        }

        if (Cursor.lockState == CursorLockMode.None) mouseLock = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.Locked) mouseLock = CursorLockMode.Locked;
        else if (Cursor.lockState == CursorLockMode.Confined) mouseLock = CursorLockMode.Confined;
        mouseVisable = Cursor.visible;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /*!
     *  A method that deactivates all prompts.
     */
    public void PromptDeactivate()
    {
        isActive = false;

        Time.timeScale = GameRules.timeScaleMultiplier;

        GameRules.cancelInputOverride--;
        if (GameRules.cancelInputOverride <= 0) { GameRules.ResumeAllInput(); }

        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].UnPause();
        }

        gameObject.SetActive(false);

        Cursor.lockState = mouseLock;
        Cursor.visible = mouseVisable;
    }

    /*!
     *  A method that escapes the UI.
     */
    private void EscapeUI(InputAction.CallbackContext ctx)
    {
        if (isActive)
        {
            closeUILate = true;
        }
    }

    void LateUpdate()
    {
        if (closeUILate) 
        {
            closeUILate = false;

            PromptDeactivate(); 
        }
    }
}
