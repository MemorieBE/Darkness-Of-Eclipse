using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that teleports a CharacterController to a transform on activation.
 *
 *  Independent
 */
public class TeleportCCBasic : MonoBehaviour
{
    [Header("Character Controller")]
    [SerializeField] private CharacterController characterController; //!< The character controller.

    [Header("Teleport")]
    [SerializeField] private Transform teleportExit; //!< The teleport exit transform.

    /*!
     *  A method that teleports the player.
     */
    public void Teleport()
    {
        characterController.enabled = false;
        characterController.gameObject.transform.position = teleportExit.transform.position;
        characterController.gameObject.transform.rotation = teleportExit.transform.rotation;
        characterController.enabled = true;
    }
}