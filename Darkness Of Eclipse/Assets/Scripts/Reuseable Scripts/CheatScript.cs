using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*! \brief A script that controls the cheat system.
 *
 *  References: DataSaveMaster, DropEquippable, GameRules, PlayerControllerCC, SceneCheckpoints, SceneLoader.
 */
public class CheatScript : MonoBehaviour
{
    public static bool cheatsEnabled = true; //!< A boolean that controls whether or not cheats are enabled.
    public static bool cheatsInputDisabled = false; //!< A boolean that controls whether or not the cheats input is disabled.

    [Header("Input Field")]
    public InputField cheatInputField; //!< The cheat input field.

    [Header("Scripts And References")]
    [SerializeField] private SceneCheckpoints checkpointScript; //!< The checkpoint script.
    [SerializeField] private DataSaveMaster savePointScript; //!< The save point game script.
    [SerializeField] private SceneLoader sceneloaderScript; //!< The scene loader game script.
    [SerializeField] private DropEquippable dropScript; //!< The drop script.
    [SerializeField] private GameObject playerObject; //!< The player game object.
    [SerializeField] private GameObject editorLight; //!< The editor light game object.

    [Header("Actions")]
    [SerializeField] private InputActionReference cheatAction; //!< The cheat input field action.
    [SerializeField] private InputActionReference escapeAction; //!< The escape action.
    [SerializeField] private InputActionReference confirmAction; //!< The confirm action.

    private bool cheatPanelOpen = false; //!< A boolean that determines whether the cheat panel is open.

    void OnEnable()
    {
        cheatAction.action.performed += ToggleCheatInput;
        cheatAction.action.Enable();

        escapeAction.action.performed += EscapeCheatInput;
        escapeAction.action.Enable();

        confirmAction.action.performed += ConfirmCheatInput;
        confirmAction.action.Enable();
    }

    void OnDisable()
    {
        cheatAction.action.performed -= ToggleCheatInput;
        cheatAction.action.Disable();

        escapeAction.action.performed -= EscapeCheatInput;
        escapeAction.action.Disable();

        confirmAction.action.performed -= ConfirmCheatInput;
        confirmAction.action.Disable();
    }

    /*!
     *  A method that toggles the cheat input field.
     */
    private void ToggleCheatInput(InputAction.CallbackContext ctx)
    {
        if (cheatsEnabled && !cheatsInputDisabled)
        {
            if (!cheatPanelOpen && GameRules.cancelInputOverride <= 0)
            {
                cheatPanelOpen = true;
                cheatInputField.gameObject.SetActive(true);

                cheatInputField.ActivateInputField();

                GameRules.CancelAllInput();
            }
            else
            {
                if (cheatPanelOpen)
                {
                    cheatPanelOpen = false;
                    cheatInputField.gameObject.SetActive(false);

                    cheatInputField.text = "";

                    GameRules.ResumeAllInput();
                }
            }
        }
        else
        {
            if (cheatPanelOpen)
            {
                cheatPanelOpen = false;
                cheatInputField.gameObject.SetActive(false);

                cheatInputField.text = "";

                GameRules.ResumeAllInput();
            }
        }
    }

    /*!
     *  A method that escapes the cheat input field.
     */
    private void EscapeCheatInput(InputAction.CallbackContext ctx)
    {
        if (cheatPanelOpen)
        {
            cheatPanelOpen = false;
            cheatInputField.gameObject.SetActive(false);

            cheatInputField.text = "";

            GameRules.ResumeAllInput();
        }
    }

    /*!
     *  A method that confirms the cheat input field.
     */
    private void ConfirmCheatInput(InputAction.CallbackContext ctx)
    {
        if (cheatPanelOpen && cheatsEnabled && !cheatsInputDisabled)
        {
            ExecuteCommand(cheatInputField.text);
            cheatInputField.text = "";

            cheatPanelOpen = false;
            cheatInputField.gameObject.SetActive(false);

            GameRules.ResumeAllInput();
        }
    }

    /*!
     *  A method that executes a command.
     *  
     *  \param The cheat command.
     */
    public void ExecuteCommand(string command)
    {
        string[] splitString = command.Split(' ');

        // Timescale Command.
        float timescaleValue;
        if (splitString.Length == 2 && 
            splitString[0] == "timescale" && 
            float.TryParse(splitString[1], out timescaleValue))
        {
            Debug.Log("Timescale set to " + timescaleValue);
            
            if (!GameRules.frozenTimeScale) Time.timeScale = timescaleValue;
            GameRules.timeScaleMultiplier = timescaleValue;
        }

        // Loadpoint Command.
        int loadCheckpoint;
        int loadScene;
        if ((splitString.Length == 3 || splitString.Length == 4) && 
            splitString[0] == "loadpoint" && 
            int.TryParse(splitString[1], out loadScene) && 
            int.TryParse(splitString[2], out loadCheckpoint))
        {
            if (splitString.Length == 4)
            {
                if (splitString[3] == "direct")
                {
                    checkpointScript.LoadCheckpoint(loadScene, loadCheckpoint, false);
                }
            }
            else
            {
                checkpointScript.LoadCheckpoint(loadScene, loadCheckpoint);
            }
        }

        // Scene Point Command.
        if (splitString.Length == 2 &&
            splitString[0] == "scenepoint")
        {
            if (splitString[1] == "save")
            {
                savePointScript.ActivateSavePoints();

                Debug.Log("Saved Scene Save Point");
            }
            else if (splitString[1] == "load")
            {
                savePointScript.LoadSavePoints();

                Debug.Log("Loaded Scene Save Point");
            }
        }

        // Reload Scene Command.
        if (splitString.Length == 1 &&
            splitString[0] == "reloadscene")
        {
            sceneloaderScript.ReloadScene();
        }

        // Editor Light Command.
        if (splitString.Length == 2 &&
            splitString[0] == "editorlight")
        {
            if (splitString[1] == "show")
            {
                editorLight.SetActive(true);

                Debug.Log("Editor Light Shown");
            }
            else if (splitString[1] == "hide")
            {
                editorLight.SetActive(false);

                Debug.Log("Editor Light Hidden");
            }
            else if (splitString[1] == "toggle")
            {
                editorLight.SetActive(!editorLight.activeSelf);

                Debug.Log("Editor Light Toggled");
            }
        }

        // Player Commands.
        if (splitString.Length == 3 &&
            splitString[0] == "player")
        {
            float playerCommandValue;

            if (splitString[1] == "movespeed" &&
                float.TryParse(splitString[2], out playerCommandValue))
            {
                if (playerObject.GetComponent<PlayerControllerCC>() != null)
                {
                    playerObject.GetComponent<PlayerControllerCC>().moveSpeed = playerCommandValue;
                }
            }

            if (splitString[1] == "sprintboost" &&
                float.TryParse(splitString[2], out playerCommandValue))
            {
                if (playerObject.GetComponent<PlayerControllerCC>() != null)
                {
                    playerObject.GetComponent<PlayerControllerCC>().sprintMultiplier = playerCommandValue;
                }
            }

            if (splitString[1] == "jumpforce" &&
                float.TryParse(splitString[2], out playerCommandValue))
            {
                if (playerObject.GetComponent<PlayerControllerCC>() != null)
                {
                    playerObject.GetComponent<PlayerControllerCC>().jumpForce = playerCommandValue;
                }
            }

            if (splitString[1] == "gravity" &&
                float.TryParse(splitString[2], out playerCommandValue))
            {
                if (playerObject.GetComponent<PlayerControllerCC>() != null)
                {
                    playerObject.GetComponent<PlayerControllerCC>().gravity = playerCommandValue;
                }
            }
        }

        // Equippable Command.
        int equippableValue;
        if (splitString.Length == 2 &&
            splitString[0] == "equip" &&
            int.TryParse(splitString[1], out equippableValue))
        {
            Debug.Log("Equipt item " + equippableValue);

            CurrentEquippable.currentEquippable = equippableValue;
        }

        // Drop Command.
        int dropValue;
        if (splitString.Length == 2 &&
            splitString[0] == "drop" &&
            int.TryParse(splitString[1], out dropValue))
        {
            Debug.Log("Drop item " + dropValue);

            dropScript.EquippableDrop(dropValue);
        }
    }
}
