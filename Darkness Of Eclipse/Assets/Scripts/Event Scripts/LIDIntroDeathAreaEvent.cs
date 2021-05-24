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
    public SceneCheckpoints checkpointScript; //!< The checkpoint script.

    [Header("Scripts and References")]
    public TriggerDetectionEnter detectorScript; //!< The script that controls the trigger detection.
    private PlayerToGhostDetector unverDetectionScript; //!< The script that controls the ghost player detection.

    [Header("Inputs")]
    public float duration; //!< The duration of the evnt in seconds.

    private float timer = 0f; //!< The event timer.
    private bool eventStarted = false; //!< A boolean that determines whether or not the event has started.

    void Start()
    {
        unverDetectionScript = unver2.GetComponent<PlayerToGhostDetector>();
    }

    void Update()
    {
        if (detectorScript.activated)
        {
            eventStarted = true;
        }

        if (eventStarted)
        {
            if (timer >= duration)
            {
                unverAudio.SetActive(true);
                unver1.SetActive(false);
                unver2.SetActive(true);

                if (unverDetectionScript.playerDetected) //Go back to prologue area.
                {
                    eventStarted = false;

                    checkpointScript.LoadCheckpoint(0, 1);
                    Debug.Log("Test");
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
