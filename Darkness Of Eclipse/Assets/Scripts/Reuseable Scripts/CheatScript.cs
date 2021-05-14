using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [HideInInspector] public bool cheatPanelOpen = false; //!< A boolean that controls whether the cheat panel is open.

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            cheatPanelOpen = !cheatPanelOpen;
        }

        cheatInputField.gameObject.SetActive(cheatPanelOpen);

        if (cheatPanelOpen)
        {
            cheatInputField.ActivateInputField();
        }
        else
        {
            cheatInputField.text = "";
        }

        if (cheatPanelOpen && Input.GetKeyDown(KeyCode.Return))
        {
            ExecuteCommand(cheatInputField.text);
            cheatInputField.text = "";
        }
    }

    /*!
     *  A method that executes a command.
     */
    public void ExecuteCommand(string command)
    {
        string[] splitString = command.Split(' ');
        float commandValue;
        if (splitString[0] == "timescale" && float.TryParse(splitString[1], out commandValue))
        {
            Time.timeScale = commandValue;
        }
    }
}
