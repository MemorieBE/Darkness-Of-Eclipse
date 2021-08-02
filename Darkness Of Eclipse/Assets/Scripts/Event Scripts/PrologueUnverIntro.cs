using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public AudioSource eventAudio; //!< The event audio source.

    [Header("Scripts And References")]
    public SceneCheckpoints checkpointScript; //!< The script that controls the checkpoints and scenes.
    public TriggerDetectionEnter detectorScript; //!< The script that controls the line detecting children.
    public FOVRaycast raycastScript; //!< The script that controls the player Unver raycast.
    public AmbienceSoundLooper ghostAmbienceScript; //!< The script that controls the Unver ambience sound looper.
    public AmbienceSoundLooper worldAmbienceScript; //!< The script that controls the world ambience sound looper.
    public AudioSource distanceBasedAmbienceAudio; //!< The audio source that controls the audio by distance script.
    public PlayerToGhostDetector unverDetection; //!< The script that controls the Unver player detection.
    public OpenTransition transitionScript; //!< The script that controls the open transition of the next checkpoint.

    [Header("Teleport")]
    public Transform teleportExit; //!< The transform to teleport the player to.

    [Header("Layers")]
    public int floorLayer = 8; //!< The floor layer.

    [Header("Inputs")]
    public float ghostDistance = 1f; //!< The distance away from the player the ghost should spawn.
    public float raycastRange = 1000f; //!< The range of the floor raycast.
    private bool eventActive = false; //!< A boolean that determines whether or not the event is active.

    [Header("Times")]
    public float starePauseTime = 0.5f; //!< The amount of time in seconds the event will cut to black if the player looks at the Unver early.
    public float autoCutTime = 3.5f; //!< The amount of time in seconds the event will cut to black if the player doesn't look at the Unver early.
    public float staredAudioDelay = 1f; //!< The amount of time in seconds the event audio will start if the player looks at the Unver early.
    public float audioStartTime = 5f; //!< The amount of time in seconds the event audio will start if the player doesn't look at the Unver early.

    private float stareTimer = 0f; //!< A float timer that counts up only when the player has stared at the Unver.
    private float timer = 0f; //!< A float timer that counts up throughout the entire event.

    private bool stared = false; //!< A boolean that determines whether or not the player looked at the Unver.
    private bool audioStarted = false; //!< A boolean that determines whether or not the event audio has been player.
    private bool hasCut = false; //!< A boolean that determines whether or not the event has cut to black.

    void Update()
    {
        if (detectorScript.activated)
        {
            ghostObject.SetActive(true);
            ghostObject.GetComponent<Animator>().SetInteger("Stage", 1);
            GhostSpawnBehindPlayer();
            eventActive = true;
            ghostAmbienceScript.activeSounds = true;
            eventAudio.gameObject.transform.position = ghostObject.transform.position;
        }

        if (eventActive && raycastScript.targetInSight && !stared)
        {
            stared = true;

            timer = starePauseTime * -1f;

            PauseScript.isPausable = false;
        }

        if (!audioStarted && ((!hasCut && timer >= audioStartTime) || (hasCut && timer >= staredAudioDelay)))
        {
            timer = 0f;
            audioStarted = true;
            eventAudio.Play();
        }

        if (!hasCut && (stareTimer > starePauseTime || unverDetection.playerDetected || (audioStarted && timer >= autoCutTime)))
        {
            CutOut();
            hasCut = true;

            if (!audioStarted) timer = 0f;
        }

        if (audioStarted && timer >= eventAudio.clip.length)
        {
            EventOver();
            eventActive = false;
        }

        if (hasCut) { eventAudio.spatialBlend = 0f; }
        else { eventAudio.spatialBlend = 1f; }

        if (stared) stareTimer += Time.deltaTime;
        if (eventActive) timer += Time.deltaTime;
    }

    /*!
     *  A method that is activated at the start of the event to spawn the Unver behind the player.
     */
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

    /*!
     *  A method that is activated when the event cuts to black.
     */
    private void CutOut()
    {
        Color blackUIColour = blackCanvas.color;
        blackUIColour.a = 1f;
        blackCanvas.color = blackUIColour;

        ghostObject.SetActive(false);

        ghostAmbienceScript.activeSounds = false;
        ghostAmbienceScript.gameObject.SetActive(false);
        ghostAmbienceScript.gameObject.SetActive(true);

        worldAmbienceScript.activeSounds = false;
        worldAmbienceScript.gameObject.SetActive(false);
        worldAmbienceScript.gameObject.SetActive(true);

        distanceBasedAmbienceAudio.mute = true;

        PauseScript.isPausable = false;
    }

    /*!
     *  A method that is activated when the event is over.
     */
    private void EventOver()
    {
        gameObject.GetComponent<TeleportBasicMethodCC>().Teleport();

        transitionScript.Transition();

        checkpointScript.LoadCheckpoint(0, 1);
        checkpointScript.ReloadScene();
    }
}