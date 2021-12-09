using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the prompt UI state.
 *
 *  [Vital Script]
 */
public class UIPopUp : MonoBehaviour
{
    public static bool isActive = false; //!< A boolean that determines whether or not the prompt is active.

    [Header("UI")]
    public GameObject[] promptUI; //!< The prompt UI game objects.
    
    private AudioSource[] masterAudio; //!< All audio sources to pause.

    private CursorLockMode mouseLock; //!< The cursor lock mode that the cursor will set itself to when unpaused.
    private bool mouseVisable; //!< A boolean that determines whether or not the cursor is visable that the cursor will set itself to when unpaused.

    void Awake()
    {
        isActive = false;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isActive) { PromptDeactivate(); }
    }

    /*!
     *  A method that activates a prompt.
     */
    public void PromptActivate(int prompt)
    {
        isActive = true;
        Time.timeScale = 0f;

        GameRules.CancelAllInput();

        masterAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].Pause();
        }

        promptUI[prompt].SetActive(true);

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

        GameRules.ResumeAllInput();

        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].UnPause();
        }

        for (int i = 0; i < promptUI.Length; i++)
        {
            promptUI[i].SetActive(false);
        }

        Cursor.lockState = mouseLock;
        Cursor.visible = mouseVisable;
    }
}
