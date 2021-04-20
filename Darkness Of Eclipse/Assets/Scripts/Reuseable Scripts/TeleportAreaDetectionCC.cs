using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TriggerDetectionEnter))]

/*! \brief A script that controls how a character controller player teleports to a new area when the player collides with a trigger.
 *  Can also enables and disables objects.
 *
 *  [Reusable Script]
 */
public class TeleportAreaDetectionCC : MonoBehaviour
{
    [Header("Player")]
    public GameObject player; //!< The player game object.

    [Header("Detector")]
    public TriggerDetectionEnter detectorScript; //!< The detection script.

    [Header("Teleport")]
    public GameObject spawnPoint; //!< The spawn point game object.
    public GameObject teleportFromArea; //!< The area to teleport from game object.
    public GameObject teleportToArea; //!< The area to teleport to game object.
    public Transform teleportExit; //!< The teleport exit transform.
    public GameObject[] enableObject; //!< The game objects to enable when teleprting.
    public GameObject[] disableObject; //!< The game objects to disable when teleprting.

    void Update()
    {
        if (detectorScript.activated)
        {
            for (int i = 0; i < enableObject.Length; i++)
            {
                enableObject[i].SetActive(true);
            }

            for (int i = 0; i < disableObject.Length; i++)
            {
                disableObject[i].SetActive(false);
            }

            player.GetComponent<CharacterController>().enabled = false;
            player.transform.position = teleportExit.position;
            player.transform.rotation = teleportExit.rotation;
            player.GetComponent<CharacterController>().enabled = true;

            spawnPoint.transform.position = teleportExit.position;
            spawnPoint.transform.rotation = teleportExit.rotation;

            teleportToArea.SetActive(true);
            teleportFromArea.SetActive(false);
        }
    }
}