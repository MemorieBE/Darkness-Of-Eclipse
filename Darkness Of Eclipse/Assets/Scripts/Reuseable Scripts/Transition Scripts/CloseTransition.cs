using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*! \brief A script that controls the close transition at the end of a scene or checkpoint.
 *
 *  References: AmbienceSoundLooper, SceneCheckpoints.
 */
public class CloseTransition : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private AmbienceSoundLooper ambience; //!< The background ambience to activate.
    [SerializeField] private Image blackUI; //!< The black UI panel to fade out.
    [SerializeField] private SceneCheckpoints checkpointScripts; //!< The script that controls the current checkpoint and scene.

    [Header("Inputs")]
    [SerializeField] private float fadeTime = 3f; //!< The amount of time in seconds it will take for the black UI panel to fade out.
    [SerializeField] private int checkpointTransition = 0; //!< The checkpoint to transition to.
    [SerializeField] private int sceneTransition = 0; //!< The scene to transition to.
    [SerializeField] private bool unfinishedNextScene = false; //!< A boolean that controls whether or not the transition will alternatively go to the main menu.

    /*!
     *  A method that starts the close transition.
     */
    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    /*!
     *  A coroutine that transitions out of a scene.
     */

    IEnumerator Transition()
    {
        ambience.activeSounds = false;

        Color uIColour = blackUI.color;

        for (float alpha = 0f; alpha < 1f; alpha += 1f / fadeTime * Time.deltaTime)
        {
            if (alpha > 1f) { alpha = 1f; }

            uIColour.a = alpha;
            blackUI.color = uIColour;

            yield return null;
        }

        if (unfinishedNextScene)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            SceneManager.LoadScene(0);
        }
        else checkpointScripts.LoadCheckpoint(sceneTransition, checkpointTransition);
    }
}
