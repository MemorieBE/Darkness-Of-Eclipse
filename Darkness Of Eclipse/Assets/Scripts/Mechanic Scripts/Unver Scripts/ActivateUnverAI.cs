using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that activates the Unver when triggered by activation.
 *
 *  [Mechanic Script]
 */
public class ActivateUnverAI : MonoBehaviour
{
    [Header("Scripts and References")]
    public GhostStage ghostStageScript; //!< The script that controls the ghost stages.

    /*!
     *  A method that is triggers the Unver AI.
     */
    public void ActivateAI()
    {
        ghostStageScript.ghostStagesActive = true;
        ghostStageScript.GhostSpawn();
    }
}