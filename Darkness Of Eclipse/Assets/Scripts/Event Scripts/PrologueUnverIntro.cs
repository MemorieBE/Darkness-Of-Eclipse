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
    [SerializeField] private GameObject player; //!< The player game object.

    [Header("Assets")]
    [SerializeField] private GameObject ghostObject; //!< The Unver game object to reference scripts.
    [SerializeField] private Image blackCanvas; //!< The black canvas UI.
    [SerializeField] private AudioSource eventAudio; //!< The event audio source.

    [Header("Scripts And References")]
    [SerializeField] private SceneCheckpoints checkpointScript; //!< The script that controls the checkpoints and scenes.
    [SerializeField] private FOVRaycast raycastScript; //!< The script that controls the player Unver raycast.
    [SerializeField] private AmbienceSoundLooper ghostAmbienceScript; //!< The script that controls the Unver ambience sound looper.
    [SerializeField] private AmbienceSoundLooper worldAmbienceScript; //!< The script that controls the world ambience sound looper.
    [SerializeField] private AudioSource distanceBasedAmbienceAudio; //!< The audio source that controls the audio by distance script.
    [SerializeField] private OpenTransition transitionScript; //!< The script that controls the open transition of the next checkpoint.

    [Header("Layers")]
    [SerializeField] private int floorLayer = 8; //!< The floor layer.

    [Header("Inputs")]
    [SerializeField] private float ghostDistance = 1f; //!< The distance away from the player the ghost should spawn.
    [SerializeField] private float raycastRange = 1000f; //!< The range of the floor raycast.

    private bool eventActive = false; //!< A boolean that determines whether or not the event is active.

    [Header("Times")]
    [SerializeField] private float starePauseTime = 0.5f; //!< The amount of time in seconds the event will cut to black if the player looks at the Unver early.
    [SerializeField] private float autoCutTime = 3.5f; //!< The amount of time in seconds the event will cut to black if the player doesn't look at the Unver early.
    [SerializeField] private float staredAudioDelay = 1f; //!< The amount of time in seconds the event audio will start if the player looks at the Unver early.
    [SerializeField] private float audioStartTime = 5f; //!< The amount of time in seconds the event audio will start if the player doesn't look at the Unver early.

    private float stareTimer = 0f; //!< A float timer that counts up only when the player has stared at the Unver.
    private float timer = 0f; //!< A float timer that counts up throughout the entire event.

    private bool stared = false; //!< A boolean that determines whether or not the player looked at the Unver.
    private bool audioStarted = false; //!< A boolean that determines whether or not the event audio has been player.
    private bool hasCut = false; //!< A boolean that determines whether or not the event has cut to black.

    public void StartEvent()
    {
        ghostObject.SetActive(true);
        ghostObject.GetComponent<Animator>().SetInteger("Stage", 1);
        GhostSpawnBehindPlayer();
        eventActive = true;
        ghostAmbienceScript.activeSounds = true;
        eventAudio.gameObject.transform.position = ghostObject.transform.position;
    }

    void Update()
    {
        if (eventActive && raycastScript.targetInSight && !stared)
        {
            stared = true;

            timer = starePauseTime * -1f;

            PauseScript.isPausable = false;
            GameMenuScript.isOpenable = false;
        }

        if (!audioStarted && ((!hasCut && timer >= audioStartTime) || (hasCut && timer >= staredAudioDelay)))
        {
            timer = 0f;
            audioStarted = true;
            eventAudio.Play();
        }

        if (!hasCut && (stareTimer > starePauseTime || (audioStarted && timer >= autoCutTime)))
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
        GameMenuScript.isOpenable = false;
    }

    /*!
     *  A method that is activated when the event is over.
     */
    private void EventOver()
    {
        checkpointScript.LoadCheckpoint(0, 1);
    }
}