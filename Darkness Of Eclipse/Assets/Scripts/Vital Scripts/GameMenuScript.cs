using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the game menu.
 *
 *  [Vital Script]
 */
public class GameMenuScript : MonoBehaviour
{
    public static bool isOpened = false; //!< A boolean that determines whether or not the menu is open.
    public static bool isOpenable = true; //!< A boolean that controls whether or not the menu mechanic is active.

    [Header("UI")]
    public GameObject menuUI; //!< The menu UI game object.

    private AudioSource[] masterAudio; //!< All audio sources to pause.

    private CursorLockMode mouseLock; //!< The cursor lock mode that the cursor will set itself to when unpaused.
    private bool mouseVisable; //!< A boolean that determines whether or not the cursor is visable that the cursor will set itself to when unpaused.

    void Awake()
    {
        isOpened = false;
        isOpenable = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isOpened) GameMenuClose();
            else if (isOpenable) GameMenuOpen();
        }
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isOpened) { GameMenuClose(); }
    }

    /*!
     *  A method that opens the game menu.
     */
    public void GameMenuOpen()
    {
        isOpened = true;
        Time.timeScale = 0f;

        GameRules.CancelAllInput();

        masterAudio = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].Pause();
        }

        menuUI.SetActive(true);

        if (Cursor.lockState == CursorLockMode.None) mouseLock = CursorLockMode.None;
        else if (Cursor.lockState == CursorLockMode.Locked) mouseLock = CursorLockMode.Locked;
        else if (Cursor.lockState == CursorLockMode.Confined) mouseLock = CursorLockMode.Confined;
        mouseVisable = Cursor.visible;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    /*!
     *  A method that closed the game menu.
     */
    public void GameMenuClose()
    {
        isOpened = false;
        Time.timeScale = GameRules.timeScaleMultiplier;

        GameRules.ResumeAllInput();

        for (int i = 0; i < masterAudio.Length; i++)
        {
            masterAudio[i].UnPause();
        }

        menuUI.SetActive(false);

        Cursor.lockState = mouseLock;
        Cursor.visible = mouseVisable;
    }
}
