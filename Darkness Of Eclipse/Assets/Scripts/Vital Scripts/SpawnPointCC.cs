using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a character controller player spawnpoint.
 *
 *  [Vital Script]
 */
public class SpawnPointCC : MonoBehaviour
{
    [Header("Position Reseter")]
    [SerializeField] private GameObject player; //!< The player game object
    [SerializeField] private float playerYResetPoint = 0f; //!< The global Y axis the player will fall under to teleport to the spawnpoint.

    void FixedUpdate()
    {
        if (player.transform.position.y <= playerYResetPoint)
        {
            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = gameObject.transform.position;
            player.transform.rotation = gameObject.transform.rotation;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
