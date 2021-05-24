using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls all static variables that don't belong in a specific script.
 *
 *  [Reusable Script]
 */
public class StaticVars : MonoBehaviour
{
    // [World]
    public static float timeScaleMultiplier = 1f; //!< The number multiplied by the time scale.
    public static bool automaticEvent = false; //!< A boolean that controls whether or not an event that uses no player input is active.

    void Awake()
    {
        timeScaleMultiplier = 1f;
        automaticEvent = false;
    }
}
