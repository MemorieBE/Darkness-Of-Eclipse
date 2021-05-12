using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls the close transition at the end of a scene or checkpoint.
 *
 *  [Reusable Script]
 */
public class CloseTransition : MonoBehaviour
{
    [Header("Assets")]
    public AmbienceSoundLooper ambience; //!< The background ambience to activate.
    public Image blackUI; //!< The black UI panel to fade out.
    public SceneCheckpoints checkpointScripts; //!< The script that controls the current checkpoint and scene.

    [Header("Inputs")]
    public float fadeTime = 3f; //!< The amount of time in seconds it will take for the black UI panel to fade out.
    private float timer = 0f; //!< The fade timer.
    private bool activeTimer = false; //!< A boolean that determines whether or not the timer is active.
    public int checkpointTransition = 0; //!< The checkpoint to transition to.
    public int sceneTransition = 0; //!< The scene to transition to.

    /*!
     *  A method that is activated to fade in the scene.
     */
    public void Transition()
    {
        ambience.activeSounds = false;

        timer = 0f;
        activeTimer = true;
    }

    void Update()
    {
        if (gameObject.GetComponent<Interactable>() != null)
        {
            if (gameObject.GetComponent<Interactable>().interacted)
            {
                Transition();
            }
        }

        if (activeTimer)
        {
            Color uIColour = blackUI.color;

            if (timer < fadeTime)
            {
                timer += Time.deltaTime;

                uIColour.a = timer / fadeTime;
                blackUI.color = uIColour;
            }
            else
            {
                timer = 0f;

                uIColour.a = 1f;
                blackUI.color = uIColour;

                activeTimer = false;

                checkpointScripts.LoadCheckpoint(checkpointTransition, sceneTransition);
            }
        }
    }
}
