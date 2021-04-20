using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a character controller player spawnpoint.
 *
 *  [Reusable Script]
 */
public class SpawnPointCC : MonoBehaviour
{
    public GameObject player; //!< The player game object

    public float playerYResetPoint = 0f; //!< The global Y axis the player will fall under to teleport to the spawnpoint.
    public bool isEnabled = true; //!< A boolean that controls whether or not the player reseter is active.

    void Update()
    {
        if (isEnabled && player.transform.position.y <= playerYResetPoint)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = gameObject.transform.position;
            player.transform.rotation = gameObject.transform.rotation;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
