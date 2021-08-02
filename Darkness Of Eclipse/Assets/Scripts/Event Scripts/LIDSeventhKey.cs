using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GlobalUnverKeyScript))]

/*! \brief A script that controls a key that can unlock a door.
 *
 *  [Event Script]
 */
public class LIDSeventhKey : MonoBehaviour
{
    [Header("Seventh Key")]
    public GameObject seventhKey; //!< The seventh key game object.
    public AudioSource lineSeventeenAudio; //!< The audio source of the line seventeen audio.
    private bool lineSeventeenPlayed = false; //!< A boolean that determines whether or not the line seventeen audio has been played.
    public Vector3 seventhKeyPositionOffset; //!< The offset position from the Unver position that the seventh key will spawn at.
    private bool keySpawned = false; //!< A boolean that determines whether or not the key has spawned in this deactivation stage.

    [Header("Unver")]
    public GameObject unver; //!< The Unver game object.

    void Start()
    {
        seventhKey.SetActive(false);
    }

    void Update()
    {
        if (!unver.GetComponent<GhostStage>().ghostDeactivationStage) { keySpawned = false; }

        if (GlobalUnverKeyScript.keyCount == 6)
        {
            if (!keySpawned)
            {
                keySpawned = true;

                seventhKey.SetActive(true);

                seventhKey.GetComponent<Rigidbody>().velocity = Vector3.zero;
                seventhKey.transform.position = unver.transform.position + seventhKeyPositionOffset;
                seventhKey.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
            }

            if (!lineSeventeenPlayed)
            {
                lineSeventeenAudio.Play();
                lineSeventeenPlayed = true;
            }
        }

        if (seventhKey.transform.position.y < 0f) seventhKey.SetActive(false); //< Disables the seventh key when it's position reaches under the map.
    }
}
