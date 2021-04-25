using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls the introduction to the Unvermeidlich in the prologue. 
 *
 *  [Event Script]
 */
public class PrologueUnverIntro : MonoBehaviour
{
    [Header("Player")]
    public GameObject player; //!< The player game object.

    [Header("Assets")]
    public GameObject ghostObject; //!< The Unver game object to reference scripts.
    public Image blackCanvas; //!< The black canvas UI.
    public TriggerDetectionEnter detectorScript; //!< The script that controls the line detecting children.
    public FOVRaycast raycastScript; //!< The script that controls the player Unver raycast.
    public AmbienceSoundLooper ambienceScript; //!< The script that controls the ambience sound looper.

    [Header("Teleport")]
    public Transform teleportExit; //!< The transform to teleport the player to.

    [Header("Layers")]
    public int floorLayer = 8; //!< The floor layer.

    [Header("Inputs")]
    public float starePause = 0.5f; //!< The amount of time in seconds the camera will cut to black after staring at the Unver.
    public float raycastRange = 1000f; //!< The range of the floor raycast.
    public float ghostDistance = 1f; //!< The distance away from the player the ghost should spawn.
    public float eventDuration = 40f; //!< The duration in seconds the event will last for.

    private bool eventActive = false; //!< A boolean that determines whether or not the event is active.
    private bool stared = false; //!< A boolean that determines whether or not the player looked at the Unver.
    private float timer = 0f; //!< A float timer that starts counting up at the start of the event and stops when the timer reaches the event duration value.

    void Update()
    {
        if (detectorScript.activated)
        {
            ghostObject.SetActive(true);
            ghostObject.GetComponent<Animator>().SetInteger("Stage", 1);
            GhostSpawnBehindPlayer();
            eventActive = true;
            ambienceScript.activeSounds = true;
        }

        if (eventActive && raycastScript.targetInSight)
        {
            stared = true;

            if (timer > starePause)
            {
                blackCanvas.gameObject.SetActive(true);

                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = teleportExit.transform.position;
                player.transform.rotation = teleportExit.transform.rotation;
                player.GetComponent<CharacterController>().enabled = true;
            }
        }

        if (stared) timer += Time.deltaTime;
    }

    //! A method that is activated at the start of the event to spawn the Unver behind the player.
    private void GhostSpawnBehindPlayer()
    {
        Vector3 spawnPosition = player.transform.TransformPoint(Vector3.back * ghostDistance); //< Generates the position behind the player head.

        Ray raycastDown = new Ray(spawnPosition + new Vector3(0, raycastRange, 0), Vector3.down);
        RaycastHit floorSpawnHit;

        if (Physics.Raycast(raycastDown, out floorSpawnHit,raycastRange * 2, (1 << floorLayer)))
        {
            spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + (raycastRange - floorSpawnHit.distance), spawnPosition.z);
        }

        ghostObject.transform.position = spawnPosition; //< Teleports the ghost to the spawn position.
    }
}