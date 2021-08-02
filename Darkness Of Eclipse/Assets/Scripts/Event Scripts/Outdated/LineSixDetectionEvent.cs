using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the line six detection event where the Unver appears behind the player in the prologue. 
 *  (Outdated)
 *
 *  [Event Script]
 */
public class LineSixDetectionEvent : MonoBehaviour
{
    [Header("Player")]
    public GameObject player; //!< The player game object.

    [Header("Scripts And References")]
    public GameObject ghostObject; //!< The Unver game object to reference scripts.
    private GhostStage ghostStageScript; //!< The script that controls the stages of the Unver.
    public PlayerToGhostDetector ghostDetectionScript; //!< The script that controls the Unver player detection.
    public TriggerDetectionEnter detectorScript; //!< The script that controls the line detecting children.
    public FOVRaycast raycastScript; //!< The script that controls the player Unver raycast.
    public Jumpscare jumpscareScript; //!< The script that controls the canvas Unver jumpscare.
    public AmbienceSoundLooper ambienceScript; //!< The script that controls the ambience sound looper.

    [Header("Teleport")]
    public GameObject teleportToArea; //!< The game object of the area to teleport the player to.
    public GameObject teleportFromArea; //!< The game object of the area to teleport the player from.
    public Transform teleportExit; //!< The transform to teleport the player to.
    public Transform spawnPoint; //!< The spawn point transform.

    [Header("Line Audio")]
    public AudioSource lineSevenAudio; //!< The audio source for the seventh line audio.

    [Header("Enable")]
    public GameObject[] enableOnDeath; //!< The game objects to enable on death.
    public GameObject[] enableOnWin; //!< The game objects to enable on win.

    [Header("Disables")]
    public GameObject[] disableOnDeath; //!< The game objects to disable on death.
    public GameObject[] disableOnWin; //!< The game objects to disable on win.

    [Header("Inputs")]
    public float ghostDistance = 1f; //!< The distance away from the player the ghost should spawn.
    public float eventDuration = 40f; //!< The duration in seconds the event will last for.

    private bool eventActive = false; //!< A boolean that determines whether or not the event is active.
    private bool isStaring = false; //!< A boolean that determines whether or not the player is staring at the Unver.
    private float timer = 0f; //!< A float timer that starts counting up at the start of the event and stops when the timer reaches the event duration value.

    void Start()
    {
        ghostStageScript = ghostObject.GetComponent<GhostStage>();
    }

    void Update()
    {
        if (detectorScript.activated)
        {
            GhostSpawnForLineSixEvent();
            ghostObject.SetActive(true);
            eventActive = true;
        }

        if (eventActive)
        {
            ambienceScript.activeSounds = true;

            if (raycastScript.targetInSight)
            {
                isStaring = true;
            }
            else if (isStaring)
            {
                ghostStageScript.ghostStagesActive = true;
                ghostStageScript.pauseTimer = true;
                ghostStageScript.ghostChasingStage = true;
                isStaring = false;
            }

            if (ghostDetectionScript.playerDetected) //On death here!
            {
                ghostStageScript.ghostStagesActive = false;
                ghostStageScript.pauseTimer = false;
                ghostObject.transform.position = Vector3.zero;
                eventActive = false;
                ghostObject.SetActive(false);

                for (int i = 0; i < enableOnDeath.Length; i++) enableOnDeath[i].SetActive(true);
                for (int i = 0; i < disableOnDeath.Length; i++) disableOnDeath[i].SetActive(false);

                ambienceScript.activeSounds = false;

                jumpscareScript.JumpScare(0);

                teleportToArea.SetActive(true);
                teleportFromArea.SetActive(false);

                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = teleportExit.transform.position;
                player.transform.rotation = teleportExit.transform.rotation;
                player.GetComponent<CharacterController>().enabled = true;
                spawnPoint.transform.position = teleportExit.transform.position;
                spawnPoint.transform.rotation = teleportExit.transform.rotation;

                ghostDetectionScript.playerDetected = false;
            }

            if (timer > eventDuration) //End of event here!
            {
                ghostStageScript.ghostStagesActive = false;
                ghostStageScript.pauseTimer = false;
                ghostObject.transform.position = Vector3.zero;
                eventActive = false;

                lineSevenAudio.Play();

                for (int i = 0; i < enableOnWin.Length; i++) enableOnWin[i].SetActive(true);
                for (int i = 0; i < disableOnWin.Length; i++) disableOnWin[i].SetActive(false);
            }
            else if (ghostStageScript.ghostChasingStage) timer = 0f;
            else timer += Time.deltaTime;
        }
        else
        {
            ghostStageScript.ghostStagesActive = false;
            ghostStageScript.pauseTimer = false;
        }
    }

    /*!
     *  A method that is activated at the start of the event to spawn the Unver behind the player.
     */
    private void GhostSpawnForLineSixEvent()
    {
        Vector3 spawnPosition = ghostStageScript.playerHead.transform.TransformPoint(Vector3.back * ghostDistance); //< Generates the position behind the player head.

        Ray raycastDown = new Ray(spawnPosition + new Vector3(0, ghostStageScript.raycastRange, 0), Vector3.down);
        RaycastHit floorSpawnHit;

        if (Physics.Raycast(raycastDown, out floorSpawnHit, ghostStageScript.raycastRange * 2, (1 << ghostStageScript.floorLayer)))
        {
            spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + (ghostStageScript.raycastRange - floorSpawnHit.distance), spawnPosition.z);
        }

        ghostObject.transform.position = spawnPosition; //< Teleports the ghost to the spawn position.
    }
}