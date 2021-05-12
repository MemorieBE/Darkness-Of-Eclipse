using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how a character controller player teleports to a new location when a method is activated.
 *
 *  [Reusable Script]
 */
public class TeleportBasicMethodCC : MonoBehaviour
{
    [Header("Player")]
    public GameObject player; //!< The player game object.

    [Header("Teleport")]
    public Transform teleportExit; //!< The teleport exit transform.

    [Header("Enable And Disable")]
    public GameObject[] enableObject; //!< The game objects to enable when teleprting.
    public GameObject[] disableObject; //!< The game objects to disable when teleprting.

    /*!
     *  A method that teleports a character controller player to a new location.
     */
    public void Teleport()
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
        player.transform.position = teleportExit.transform.position;
        player.transform.rotation = teleportExit.transform.rotation;
        player.GetComponent<CharacterController>().enabled = true;
    }
}