using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/*! \brief A script that controls the pause menu and mechanic.
 *
 *  References: GameRules, SaveSystem, SettingsValues.
 */
public class PauseScript : MonoBehaviour
{
    public static bool isPaused = false; //!< A boolean that determines whether or not the game is paused.
    public static bool isPausable = true; //!< A boolean that controls whether or not the pause mechanic is active.

    [Header("UI")]
    [SerializeField] private GameObject pauseUI; //!< The pause UI game object.
    [SerializeField] private GameObject settingsUI; //!< The settings UI game object.

    [Header("Audio")]
    [SerializeField] private float audioFadeTime = 0.5f; //!< The amount of time in seconds the audio will fade out to.
    [Range(-3f, 3f)] [SerializeField] private float targetPitch = 0f; //!< The target pitch the audio will fade to.
    private AudioSource[] masterAudio; //!< All audio sources to pause.
    private float[] originalAudioVolume; //!< The original volume for the paused audio sources.

    [Header("Action")]
    [SerializeField] private InputActionReference pauseAction; //!< The pause action.

    private CursorLockMode mouseLock; //!< The cursor lock mode that the cursor will set itself to when unpaused.
    private bool mouseVisable; //!< A boolean that determines whether or not the cursor is visable that the cursor will set itself to when unpaused.

    void Awake()
    {
        isPaused = false;
        isPausable = true;
    }

    void OnEnable()
    {
        pauseAction.action.performed += TogglePause;
        pauseAction.action.Enable();
    }

    void OnDisable()
    {
        pauseAction.action.performed -= TogglePause;
        pauseAction.action.Disable();
    }

    /*!
     *  A method that toggles the pause menu.
     */
    private void TogglePause(InputAction.CallbackContext ctx)
    {
        if (!settingsUI.activeSelf)
        {
            if (isPaused) { UnPause(); }
            else if (isPausable) { Pause(); }
        }
    }

    /*!
     *  An IEnumerator that fades out the audio.
     */
    private IEnumerator AudioFade()
    {
        for (float time = 0; time < audioFadeTime; time += Time.unscaledDeltaTime)
        {
            for (int i = 0; i < masterAudio.Length; i++)
            {
                masterAudio[i].pitch = 1f - (1f - audioFadeTime) * (time / audioFadeTime);
                masterAudio[i].volume = originalAudioVolume[i] - originalAudioVolume[i] * (time / audioFadeTime);
            }

            yield return null;
        }

        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].Pause();
            masterAudio[i].pitch = 1f;
            masterAudio[i].volume = originalAudioVolume[i];
        }
    }

    /*!
     *  A method that pauses the game.
     */
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        GameRules.frozenTimeScale = true;

        GameRules.CancelAllInput();

        masterAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        originalAudioVolume = new float[masterAudio.Length];
        for (int i = 0; i < masterAudio.Length; i++)
        {
            originalAudioVolume[i] = masterAudio[i].volume;
        }

        StartCoroutine(AudioFade());

        pauseUI.SetActive(true);

        if (Cursor.lockState == CursorLockMode.None) mouseLock = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.Locked) mouseLock = CursorLockMode.Locked;
        else if (Cursor.lockState == CursorLockMode.Confined) mouseLock = CursorLockMode.Confined;
        mouseVisable = Cursor.visible;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /*!
     *  A method that unpauses the game.
     */
    public void UnPause()
    {
        isPaused = false;
        Time.timeScale = GameRules.timeScaleMultiplier;

        GameRules.frozenTimeScale = false;

        GameRules.ResumeAllInput();

        StopCoroutine(AudioFade());

        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].UnPause();
            masterAudio[i].pitch = 1f;
            masterAudio[i].volume = originalAudioVolume[i];
        }

        pauseUI.SetActive(false);

        Cursor.lockState = mouseLock;
        Cursor.visible = mouseVisable;
    }

    /*!
     *  A method that exits to the main menu.
     */
    public void ExitToMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;

        GameRules.frozenTimeScale = false;

        GameRules.ResumeAllInput();

        if (SettingsValues.autoSave) { SaveSystem.SaveGame(); }

        SceneManager.LoadScene(0);
    }
}
