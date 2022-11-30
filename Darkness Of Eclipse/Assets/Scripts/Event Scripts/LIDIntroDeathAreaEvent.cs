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
    [SerializeField] private DialogueSubtitles subtitlesScript;

    [Header("Inputs")]
    [SerializeField] private float duration; //!< The duration of the evnt in seconds.
    [SerializeField] private Vector3Int subtitlesID;

    /*!
     *  A method that starts the event.
     */
    public void StartEvent()
    {
        StartCoroutine(EventActive());
    }

    /*!
     *  A coroutine that plays the event.
     */
    private IEnumerator EventActive()
    {
        subtitlesScript.ActivateSubtitle(subtitlesID.x, subtitlesID.y, subtitlesID.z);

        yield return new WaitForSeconds(duration);

        unverAudio.SetActive(true);
        unver1.SetActive(false);
        unver2.SetActive(true);
    }
}
