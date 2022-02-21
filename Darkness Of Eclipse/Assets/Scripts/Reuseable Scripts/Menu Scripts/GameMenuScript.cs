using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*! \brief A script that controls the game menu.
 *
 *  References: GameRules.
 */
public class GameMenuScript : MonoBehaviour
{
    public static bool isOpened = false; //!< A boolean that determines whether or not the menu is open.
    public static bool isOpenable = true; //!< A boolean that controls whether or not the menu mechanic is active.

    public static bool autoPrimaryMenu = true; //!< A boolean that controls whether or not the first menu will open when the main game menu button is pressed.

    [Header("UI")]
    public GameObject menuUI; //!< The menu UI game object.
    [SerializeField] private GameObject[] subMenuUIs; //!< The sub menu UI game objects.

    [Header("Actions")]
    [SerializeField] private InputActionReference mainMenuAction; //!< The main game menu action.
    [SerializeField] private InputActionReference[] subMenuActions; //!< The sub game menu actions.
    [SerializeField] private InputActionReference escapeAction; //!< The escape action.

    private Action<InputAction.CallbackContext>[] subMenuHandler; //!< The sub menu handlers.

    private AudioSource[] masterAudio; //!< All audio sources to pause.

    private CursorLockMode mouseLock; //!< The cursor lock mode that the cursor will set itself to when unpaused.
    private bool mouseVisable; //!< A boolean that determines whether or not the cursor is visable that the cursor will set itself to when unpaused.

    private bool closeMenuLate = false; //!< A boolean that closes the menu in the late update.

    void Awake()
    {
        subMenuHandler = new Action<InputAction.CallbackContext>[subMenuActions.Length];

        for (int i = 0; i < subMenuActions.Length; i++)
        {
            int subMenu = i;

            subMenuHandler[i] = ctx => ToggleSubMenu(subMenu);
        }

        isOpened = false;
        isOpenable = true;
    }

    void OnEnable()
    {
        mainMenuAction.action.performed += ToggleMainMenu;
        mainMenuAction.action.Enable();

        for (int i = 0; i < subMenuActions.Length; i++)
        {
            subMenuActions[i].action.performed += subMenuHandler[i];
            subMenuActions[i].action.Enable();
        }

        escapeAction.action.performed += EscapeGameMenu;
        escapeAction.action.Enable();
    }

    void OnDisable()
    {
        mainMenuAction.action.performed -= ToggleMainMenu;
        mainMenuAction.action.Disable();

        for (int i = 0; i < subMenuActions.Length; i++)
        {
            subMenuActions[i].action.performed -= subMenuHandler[i];
            subMenuActions[i].action.Disable();
        }

        escapeAction.action.performed -= EscapeGameMenu;
        escapeAction.action.Disable();
    }

    /*!
     *  A method that toggles the main game menu.
     */
    private void ToggleMainMenu(InputAction.CallbackContext ctx)
    {
        if (isOpened)
        {
            closeMenuLate = true;
        }
        else if (isOpenable)
        {
            if (autoPrimaryMenu)
            {
                subMenuUIs[0].SetActive(true);

                for (int i = 1; i < subMenuUIs.Length; i++)
                {
                    subMenuUIs[i].SetActive(false);
                }
            }

            GameMenuOpen();
        }
    }

    /*!
     *  A method that toggles a sub game menu.
     */
    private void ToggleSubMenu(int i)
    {
        if (isOpened)
        {
            if (subMenuUIs[i].activeSelf)
            {
                closeMenuLate = true;
            }
            else
            {
                for (int j = 0; j < subMenuUIs.Length; j++)
                {
                    if (i == j) { subMenuUIs[j].SetActive(true); }
                    else { subMenuUIs[j].SetActive(false); }
                }
            }
        }
        else
        {
            for (int j = 0; j < subMenuUIs.Length; j++)
            {
                if (i == j) { subMenuUIs[j].SetActive(true); }
                else { subMenuUIs[j].SetActive(false); }
            }

            GameMenuOpen();
        }
    }

    /*!
     *  A method that escapes the game menu.
     */
    private void EscapeGameMenu(InputAction.CallbackContext ctx)
    {
        if (isOpened)
        {
            closeMenuLate = true;
        }
    }

    void LateUpdate()
    {
        if (closeMenuLate) 
        {
            closeMenuLate = false;

            GameMenuClose();
        }
    }

    /*!
     *  A method that opens the game menu.
     */
    public void GameMenuOpen()
    {
        if (GameRules.cancelInputOverride > 0) { return; }

        isOpened = true;
        Time.timeScale = 0f;

        GameRules.frozenTimeScale = true;

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

        GameRules.frozenTimeScale = false;

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
