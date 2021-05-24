using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*! \brief A script that controls the cheat system.
 *
 *  [Reusable Script]
 */
public class CheatScript : MonoBehaviour
{
    public static bool cheatsEnabled = true; //!< A boolean that controls whether or not cheats are enabled.

    [Header("Input Field")]
    public InputField cheatInputField; //!< The cheat input field.

    [Header("Scripts And References")]
    public SceneCheckpoints checkpointScript; //!< The checkpoint script.
    public GameObject playerObject; //!< The player game object.
    public GameObject editorLight; //!< The editor light game object.

    private bool cheatPanelOpen = false; //!< A boolean that determines whether the cheat panel is open.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cheatPanelOpen = !cheatPanelOpen;
            cheatInputField.gameObject.SetActive(cheatPanelOpen);

            if (cheatPanelOpen)
            {
                cheatInputField.ActivateInputField();

                PlayerControllerCC.allowPlayerInputs = false;
            }
            else
            {
                cheatInputField.text = "";

                PlayerControllerCC.allowPlayerInputs = !StaticVars.automaticEvent;
            }
        }

        if (cheatPanelOpen && Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteCommand(cheatInputField.text);
            cheatInputField.text = "";

            cheatPanelOpen = false;
            cheatInputField.gameObject.SetActive(false);

            PlayerControllerCC.allowPlayerInputs = true;
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

            if (!PauseScript.isPaused) Time.timeScale = timescaleValue;
            StaticVars.timeScaleMultiplier = timescaleValue;
        }

        // Loadpoint Command.
        int loadCheckpoint;
        int loadScene;
        if (splitString.Length == 3 && 
            splitString[0] == "loadpoint" && 
            int.TryParse(splitString[1], out loadScene) && 
            int.TryParse(splitString[2], out loadCheckpoint))
        {
            checkpointScript.LoadCheckpoint(loadScene, loadCheckpoint);
            if (loadScene == 0 || loadScene == SceneManager.GetActiveScene().buildIndex) checkpointScript.ReloadScene();
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
    }
}
