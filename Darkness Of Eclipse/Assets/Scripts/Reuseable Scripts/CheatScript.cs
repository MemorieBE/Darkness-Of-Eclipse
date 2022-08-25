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
    [SerializeField] private InventoryScript inventoryScript; //!< The inventory script.

    [Header("Actions")]
    [SerializeField] private InputActionReference cheatAction; //!< The cheat input field action.
    [SerializeField] private InputActionReference escapeAction; //!< The escape action.
    [SerializeField] private InputActionReference confirmAction; //!< The confirm action.
    [SerializeField] private InputActionReference lastCommandAction; //!< The last command action.

    private bool cheatPanelOpen = false; //!< A boolean that determines whether the cheat panel is open.
    private static string lastCommand; //!< The last command the player input.

    void Awake()
    {
        cheatAction.action.Enable();

        escapeAction.action.Enable();

        confirmAction.action.Enable();

        lastCommandAction.action.Enable();
    }

    void OnEnable()
    {
        cheatAction.action.performed += ToggleCheatInput;

        escapeAction.action.performed += EscapeCheatInput;

        confirmAction.action.performed += ConfirmCheatInput;

        lastCommandAction.action.performed += LastCommandInput;
    }

    void OnDisable()
    {
        cheatAction.action.performed -= ToggleCheatInput;

        escapeAction.action.performed -= EscapeCheatInput;

        confirmAction.action.performed -= ConfirmCheatInput;

        lastCommandAction.action.performed -= LastCommandInput;
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
     *  A method that restores the last command.
     */
    private void LastCommandInput(InputAction.CallbackContext ctx)
    {
        if (cheatPanelOpen && cheatsEnabled && !cheatsInputDisabled)
        {
            cheatInputField.text = lastCommand;
        }
    }

    /*!
     *  A method that executes a command.
     *  
     *  \param The cheat command.
     */
    public void ExecuteCommand(string command)
    {
        lastCommand = command;

        string[] splitString = command.Split(' ');

        if (splitString.Length > 0)
        {
            switch (splitString[0])
            {
                // Timescale Command.
                case "timescale":
                    if (splitString.Length == 2 &&
                        float.TryParse(splitString[1], out float timescaleValue))
                    {
                        Debug.Log("Timescale set to " + timescaleValue);

                        if (!GameRules.frozenTimeScale) Time.timeScale = timescaleValue;
                        GameRules.timeScaleMultiplier = timescaleValue;
                    }
                    break;

                // Loadpoint Command.
                case "loadpoint":
                    if ((splitString.Length == 3 || splitString.Length == 4) &&
                        int.TryParse(splitString[1], out int loadScene) &&
                        int.TryParse(splitString[2], out int loadCheckpoint))
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
                    break;

                // Scene Point Command.
                case "scenepoint":
                    if (splitString.Length == 2)
                    {
                        switch (splitString[1])
                        {
                            case "save":
                                savePointScript.ActivateSavePoints();

                                Debug.Log("Saved Scene Save Point");
                                break;

                            case "load":
                                savePointScript.LoadSavePoints();

                                Debug.Log("Loaded Scene Save Point");
                                break;
                        }
                    }
                    break;

                // Reload Scene Command.
                case "reloadscene":
                    if (splitString.Length == 1)
                    {
                        sceneloaderScript.ReloadScene();
                    }
                    break;

                // Editor Light Command.
                case "editorlight":
                    if (splitString.Length == 2)
                    {
                        switch (splitString[1])
                        {
                            case "show":
                                editorLight.SetActive(true);

                                Debug.Log("Editor Light Shown");
                                break;

                            case "hide":
                                editorLight.SetActive(false);

                                Debug.Log("Editor Light Hidden");
                                break;

                            case "toggle":
                                editorLight.SetActive(!editorLight.activeSelf);

                                Debug.Log("Editor Light Toggled");
                                break;
                        }
                    }
                    break;

                // Player Commands.
                case "player":
                    if (splitString.Length == 3)
                    {
                        if (playerObject.GetComponent<PlayerControllerCC>() != null)
                        {
                            float playerCommandValue;
                            switch (splitString[1])
                            {
                                case "movespeed":
                                    if (float.TryParse(splitString[2], out playerCommandValue))
                                    {
                                        playerObject.GetComponent<PlayerControllerCC>().moveSpeed = playerCommandValue;
                                    }
                                    break;

                                case "sprintboost":
                                    if (float.TryParse(splitString[2], out playerCommandValue))
                                    {
                                        playerObject.GetComponent<PlayerControllerCC>().sprintMultiplier = playerCommandValue;
                                    }
                                    break;

                                case "jumpforce":
                                    if (float.TryParse(splitString[2], out playerCommandValue))
                                    {
                                        playerObject.GetComponent<PlayerControllerCC>().jumpForce = playerCommandValue;
                                    }
                                    break;

                                case "gravity":
                                    if (float.TryParse(splitString[2], out playerCommandValue))
                                    {
                                        playerObject.GetComponent<PlayerControllerCC>().gravity = playerCommandValue;
                                    }
                                    break;
                            }
                        }
                    }
                    break;

                // Collect Command.
                case "collect":
                    if (splitString.Length == 2 &&
                        int.TryParse(splitString[1], out int collectValue))
                    {
                        Debug.Log("Added item " + collectValue);

                        inventoryScript.InventoryUpdateItem(collectValue, true);
                    }
                    break;

                // Dispose Command.
                case "dispose":
                    if (splitString.Length == 2 &&
                        int.TryParse(splitString[1], out int disposeValue))
                    {
                        Debug.Log("Removed item " + disposeValue);

                        inventoryScript.InventoryUpdateItem(disposeValue, false);
                    }
                    break;

                // Equippable Command.
                case "equip":
                    if (splitString.Length == 2 &&
                        int.TryParse(splitString[1], out int equippableValue))
                    {
                        Debug.Log("Equipt item " + equippableValue);

                        CurrentEquippable.currentEquippable = equippableValue;
                    }
                    break;

                // Drop Command.
                case "drop":
                    if (splitString.Length == 2 &&
                        int.TryParse(splitString[1], out int dropValue))
                    {
                        Debug.Log("Drop item " + dropValue);

                        dropScript.EquippableDrop(dropValue);
                    }
                    break;
            }
        }
    }
}
