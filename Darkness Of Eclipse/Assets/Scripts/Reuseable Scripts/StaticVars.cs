using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls all static variables that don't belong in a specific script.
 *
 *  [Reusable Script]
 */
public class StaticVars : MonoBehaviour
{
    // [Player]
    public static bool allowPlayerInputs = true; //!< A boolean that controls whether or not the player inputs are read.

    // [World]
    public static float timeScaleMultiplier = 1f; //!< The number multiplied by the time scale.

    void Awake()
    {
        allowPlayerInputs = true;

        timeScaleMultiplier = 1f;
    }
}
