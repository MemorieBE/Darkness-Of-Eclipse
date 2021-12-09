using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the events in the Locked In Despair Intro Death Area.
 *
 *  [Event Script]
 */
public class LIDIntroDeathAreaEvent : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private GameObject unverAudio; //!< The Unver audio game object.
    [SerializeField] private GameObject unver1; //!< The first Unver game object.
    [SerializeField] private GameObject unver2; //!< The second Unver game object.
    [SerializeField] private SceneCheckpoints checkpointScript; //!< The checkpoint script.

    private PlayerToGhostDetector unverDetectionScript; //!< The script that controls the ghost player detection.

    [Header("Inputs")]
    [SerializeField] private float duration; //!< The duration of the evnt in seconds.

    private float timer = 0f; //!< The event timer.
    private bool eventStarted = false; //!< A boolean that determines whether or not the event has started.

    void Start()
    {
        unverDetectionScript = unver2.GetComponent<PlayerToGhostDetector>();
    }

    /*!
     *  A method that is trigger on activation.
     */
    public void Activated()
    {
        eventStarted = true;
    }

    void Update()
    {
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
                }
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}
