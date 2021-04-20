using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls one of the keys to collect in an Unver level.
 *
 *  [Mechanic Script]
 */
public class LocalUnverKeyScript : MonoBehaviour
{
    public GameObject theUnver; //!< The Unver game object.

    public GlobalUnverKeyScript globalUnverKey; //!< The script that controls all Unver keys.
    private GhostStage ghostStageScript; //!< The script that controls the Unver stages.

    public int keyID; //!< The ID for the key.

    void Start()
    {
        ghostStageScript = theUnver.GetComponent<GhostStage>();
    }

    void Update()
    {
        if (gameObject.GetComponent<Interactable>().interacted) Interact();
    }

    //! A method that is activated when the player interacts with the gate using the Interactable script.
    void Interact()
    {
        globalUnverKey.keyCount ++;
        if (keyID == globalUnverKey.keyCount) globalUnverKey.keyCountForAchievement++;
        else globalUnverKey.keyCountForAchievement = 0;

        ghostStageScript.ghostStagesActive = true;
        ghostStageScript.GhostSpawn();
        gameObject.SetActive(false);
    }
}
