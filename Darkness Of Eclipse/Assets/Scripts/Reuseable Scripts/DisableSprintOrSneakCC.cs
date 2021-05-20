using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that disables/enables the character controller based player controller's sprint and/or sneak mechanic on enable.
 *
 *  [Reusable Script]
 */
public class DisableSprintOrSneakCC : MonoBehaviour
{
    [SerializeField] private bool disableSprint = false; //!< A boolean that controls whether or not sprint will be disabled or enabled.
    [SerializeField] private bool disableSneak = false; //!< A boolean that controls whether or not sneak will be disabled or enabled.

    void OnEnable()
    {
        PlayerControllerCC.sprintDisabled = disableSprint;
        PlayerControllerCC.sneakDisabled = disableSneak;
    }
}
