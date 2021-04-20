using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerDetectionEnter))]

/*! \brief A script that activates the Unver when triggered by the TriggerDetection script.
 *
 *  [Mechanic Script]
 */
public class ActivateUnverAI : MonoBehaviour
{
    [Header("Scripts and References")]
    public GhostStage ghostStageScript; //!< The script that controls the ghost stages.
    private TriggerDetectionEnter detectionScript; //!< The script that controls the trigger detection.

    void Start()
    {
        detectionScript = gameObject.GetComponent<TriggerDetectionEnter>();
    }

    void Update()
    {
        if (detectionScript.activated)
        {
            ghostStageScript.ghostStagesActive = true;
            ghostStageScript.GhostSpawn();
        }
    }
}