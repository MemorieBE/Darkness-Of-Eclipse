using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the events in the Locked In Despair Intro Death Area.
 *
 *  [Event Script]
 */
public class LIDIntroDeathAreaEvent : MonoBehaviour
{
    [Header("Player")]
    public GameObject player; //!< The player game object.

    [Header("Assets")]
    public GameObject unverAudio; //!< The Unver audio game object.
    public GameObject unver1; //!< The first Unver game object.
    public GameObject unver2; //!< The second Unver game object.

    [Header("Scripts and References")]
    public TriggerDetectionEnter detectorScript; //!< The script that controls the trigger detection.
    private PlayerToGhostDetector unverDetectionScript; //!< The script that controls the ghost player detection.

    [Header("Teleport")]
    public GameObject teleportToArea; //!< The game object of the area to teleport the player to.
    public GameObject teleportFromArea; //!< The game object of the area to teleport the player from.
    public Transform teleportExit; //!< The transform to teleport the player to.
    public Transform spawnPoint; //!< The spawn point transform.

    [Header("Inputs")]
    public float duration; //!< The duration of the evnt in seconds.

    private float timer = 0f;
    void Start()
    {
        unverDetectionScript = unver2.GetComponent<PlayerToGhostDetector>();
    }

    void Update()
    {
        if (detectorScript.activated)
        {
            if (timer >= duration)
            {
                unverAudio.SetActive(true);
                unver1.SetActive(false);
                unver2.SetActive(true);

                if (unverDetectionScript.playerDetected) //Go back to prologue area.
                {
                    detectorScript.activated = false;

                    player.GetComponent<CharacterController>().enabled = false;
                    player.transform.position = teleportExit.transform.position;
                    player.transform.rotation = teleportExit.transform.rotation;
                    player.GetComponent<CharacterController>().enabled = true;

                    teleportToArea.SetActive(true);
                    teleportFromArea.SetActive(false);
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
