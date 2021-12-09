﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*! \brief A script that controls the cheat system.
 *
 *  [Vital Script]
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
    [SerializeField] private DropEquippable dropScript; //!< The drop script.
    [SerializeField] private GameObject playerObject; //!< The player game object.
    [SerializeField] private GameObject editorLight; //!< The editor light game object.

    private bool cheatPanelOpen = false; //!< A boolean that determines whether the cheat panel is open.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1) && cheatsEnabled && !cheatsInputDisabled)
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

                PlayerControllerCC.allowPlayerInputs = true;
            }
        }
        else if ((!cheatsEnabled || cheatsInputDisabled) && cheatPanelOpen)
        {
            cheatPanelOpen = false;
            cheatInputField.gameObject.SetActive(false);

            cheatInputField.text = "";

            PlayerControllerCC.allowPlayerInputs = !GameRules.cancelAllInputs;
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
            GameRules.timeScaleMultiplier = timescaleValue;
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