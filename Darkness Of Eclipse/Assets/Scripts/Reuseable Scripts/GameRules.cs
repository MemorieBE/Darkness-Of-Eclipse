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
    public static int cancelInputOverride = 0; //!< The integer that determines whether or not all player inputs are disbaled.
    public static bool frozenTimeScale = false; //!< The boolean that determines whether or not the time scale is frozen/paused.
    public static bool freezePlayer = false; //!< Freezes the player for cutscenes and menus.

    void Awake()
    {
        timeScaleMultiplier = 1f;

        ResumeAllInput();
    }

    public static void CancelAllInput()
    {
        cancelInputOverride++;

        freezePlayer = true;
        PauseScript.isPausable = PauseScript.isPaused;
        GameMenuScript.isOpenable = GameMenuScript.isOpened;
    }

    public static void ResumeAllInput()
    {
        if (cancelInputOverride > 0) { cancelInputOverride--; }
        else { cancelInputOverride = 0; }

        if (cancelInputOverride == 0)
        {
            freezePlayer = false;
            PauseScript.isPausable = true;
            GameMenuScript.isOpenable = true;
        }
    }
}
