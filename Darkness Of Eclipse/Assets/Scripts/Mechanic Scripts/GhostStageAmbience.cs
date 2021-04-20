using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GhostStage))]

/*! \brief A script that controls the Unver stage ambience.
 *
 *  [Mechanic Script]
 */
public class GhostStageAmbience : MonoBehaviour
{
    [Header("Ambience")]
    public AmbienceSoundLooper ambienceSounds; //!< The ambience sounds script.

    private GhostStage ghostStageScript; //!< The Unver stage script.

    void Start()
    {
        ghostStageScript = gameObject.GetComponent<GhostStage>();
    }

    void Update()
    {
        if (ghostStageScript.ghostDeactivationStage || !ghostStageScript.ghostStagesActive)
        {
            ambienceSounds.activeSounds = false;
        }
        else
        {
            ambienceSounds.activeSounds = true;
        }

        if (!ghostStageScript.ghostChasingStage)
        {
            if (ghostStageScript.ghostTier >= 1 && ghostStageScript.ghostTier < ghostStageScript.ghostSecondTierAnimation)
            {
                ambienceSounds.currentBackgroundAmbience = 0;
            }
            if (ghostStageScript.ghostTier >= ghostStageScript.ghostSecondTierAnimation && ghostStageScript.ghostTier < ghostStageScript.ghostThirdTierAnimation)
            {
                ambienceSounds.currentBackgroundAmbience = 1;
            }
            if (ghostStageScript.ghostTier >= ghostStageScript.ghostThirdTierAnimation && ghostStageScript.ghostTier <= ghostStageScript.ghostMaxTier)
            {
                ambienceSounds.currentBackgroundAmbience = 2;
            }
        }
    }
}
