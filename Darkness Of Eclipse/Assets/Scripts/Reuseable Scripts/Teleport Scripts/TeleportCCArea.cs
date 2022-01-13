using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that teleports a CharacterController to a new area on activation.
 *
 *  Independent
 */
public class TeleportCCArea : MonoBehaviour
{
    [Header("Character Controller")]
    [SerializeField] private CharacterController characterController; //!< The character controller.

    [Header("Teleport")]
    [SerializeField] private GameObject spawnPoint; //!< The spawn point game object.
    [SerializeField] private GameObject teleportFromArea; //!< The area to teleport from game object.
    [SerializeField] private GameObject teleportToArea; //!< The area to teleport to game object.
    [SerializeField] private Transform teleportExit; //!< The teleport exit transform.

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        characterController.enabled = false;
        characterController.gameObject.transform.position = teleportExit.position;
        characterController.gameObject.transform.rotation = teleportExit.rotation;
        characterController.enabled = true;

        spawnPoint.transform.position = teleportExit.position;
        spawnPoint.transform.rotation = teleportExit.rotation;

        teleportToArea.SetActive(true);
        teleportFromArea.SetActive(false);
    }
}