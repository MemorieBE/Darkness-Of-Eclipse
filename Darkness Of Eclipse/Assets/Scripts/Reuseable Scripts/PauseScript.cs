using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that controls the pause menu and mechanic.
 *
 *  [Reusable Script]
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
    private float fadeTimer = 0f; //!< The audio fade timer.
    [Range(-3f, 3f)] [SerializeField] private float targetPitch = 0f; //!< The target pitch the audio will fade to.
    private bool audioFaded = false; //!< A boolean that determines whether or not the audio has faded.
    private AudioSource[] masterAudio; //!< All audio sources to pause.
    private float[] originalAudioVolume; //!< The original volume for the paused audio sources.

    private CursorLockMode mouseLock; //!< The cursor lock mode that the cursor will set itself to when unpaused.
    private bool mouseVisable; //!< A boolean that determines whether or not the cursor is visable that the cursor will set itself to when unpaused.

    void Awake()
    {
        isPausable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !settingsUI.activeSelf)
        {
            if (isPaused) UnPause();
            else if (isPausable && !GameMenuScript.isOpened) Pause();
        }

        if (isPaused && !audioFaded)
        {
            fadeTimer += Time.unscaledDeltaTime;

            if (fadeTimer >= audioFadeTime)
            {
                for (int i = 0; i < masterAudio.Length; i++)
                {
                    masterAudio[i].Pause();
                    masterAudio[i].pitch = 1f;
                    masterAudio[i].volume = originalAudioVolume[i];
                }
                fadeTimer = 0f;
                audioFaded = true;
            }
            else
            {
                for (int i = 0; i < masterAudio.Length; i++)
                {
                    masterAudio[i].pitch = 1f - (1f - audioFadeTime) * (fadeTimer / audioFadeTime);
                    masterAudio[i].volume = originalAudioVolume[i] - originalAudioVolume[i] * (fadeTimer / audioFadeTime);
                }
            }
        }
    }

    /*!
     *  A method that pauses the game.
     */
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;

        masterAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        originalAudioVolume = new float[masterAudio.Length];
        for (int i = 0; i < masterAudio.Length; i++)
        {
            originalAudioVolume[i] = masterAudio[i].volume;
        }

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

        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].UnPause();
            masterAudio[i].pitch = 1f;
            masterAudio[i].volume = originalAudioVolume[i];
        }
        fadeTimer = 0f;
        audioFaded = false;

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
        SceneManager.LoadScene(0);
    }
}
