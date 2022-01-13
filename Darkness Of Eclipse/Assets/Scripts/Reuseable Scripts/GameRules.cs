using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls all static variables and functions that don't belong in a specific script.
 *
 *  References: GameMenuScript, PauseScript, PlayerControllerCC.
 */
public class GameRules : MonoBehaviour
{
    public static float timeScaleMultiplier = 1f; //!< The number multiplied by the time scale.
    public static bool cancelAllInputs = false; //!< The boolean that determines whether or not all player inputs are disbaled.
    public static bool frozenTimeScale = false; //!< The boolean that determines whether or not the time scale is frozen/paused.

    void Awake()
    {
        timeScaleMultiplier = 1f;

        ResumeAllInput();
    }

    public static void CancelAllInput()
    {
        cancelAllInputs = true;

        PlayerControllerCC.allowPlayerInputs = false;
        PauseScript.isPausable = PauseScript.isPaused;
        GameMenuScript.isOpenable = GameMenuScript.isOpened;
    }

    public static void ResumeAllInput()
    {
        cancelAllInputs = false;

        PlayerControllerCC.allowPlayerInputs = true;
        PauseScript.isPausable = true;
        GameMenuScript.isOpenable = true;
    }
}
