using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*! \brief A script that controls how the ghost/unver readjusts when spawning inside of a collider.
 *
 *  [Mechanic Script]
 */
public class GhostPositionAdjuster : MonoBehaviour
{
    [Header("Unver")]
    public GameObject theUnver; //!< The Unver game object.
    private GhostStage unverScript; //!< The Unver script.

    [Header("Layers")]
    public int[] unverIgnoreLayers; //!< The layers of colliders that the Unver will ignore.

    void Start()
    {
        unverScript = theUnver.GetComponent<GhostStage>();
    }

    void OnTriggerStay(Collider collisionData)
    {
        int layerMask = 1 << unverIgnoreLayers[0];
        foreach (var unverIgnoredLayer in unverIgnoreLayers.Skip(1))
        {
            layerMask = (layerMask | 1 << unverIgnoredLayer);
        }
        layerMask = ~layerMask;

        if (collisionData == null) return;
        if ((collisionData.gameObject.layer == layerMask) && (unverScript.ghostStalkingStage)) unverScript.GhostSpawn();
    }
}
