using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*! \brief A script that controls the close transition at the end of a scene or checkpoint.
 *
 *  [Reusable Script]
 */
public class CloseTransition : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private AmbienceSoundLooper ambience; //!< The background ambience to activate.
    [SerializeField] private Image blackUI; //!< The black UI panel to fade out.
    [SerializeField] private SceneCheckpoints checkpointScripts; //!< The script that controls the current checkpoint and scene.

    [Header("Inputs")]
    [SerializeField] private float fadeTime = 3f; //!< The amount of time in seconds it will take for the black UI panel to fade out.
    private float timer = 0f; //!< The fade timer.
    private bool activeTimer = false; //!< A boolean that determines whether or not the timer is active.
    [SerializeField] private int checkpointTransition = 0; //!< The checkpoint to transition to.
    [SerializeField] private int sceneTransition = 0; //!< The scene to transition to.
    [SerializeField] private bool unfinishedNextScene = false; //!< A boolean that controls whether or not the transition will alternatively go to the main menu.

    /*!
     *  A method that is activated to fade in the scene.
     */
    public void Transition()
    {
        PauseScript.isPausable = false;

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
                gameObject.GetComponent<Interactable>().interacted = false;
                gameObject.GetComponent<Interactable>().enabled = false;
            }
        }

        if (gameObject.GetComponent<TriggerDetectionEnter>() != null)
        {
            if (gameObject.GetComponent<TriggerDetectionEnter>().activated)
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

                if (unfinishedNextScene)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                    SceneManager.LoadScene(0);
                }
                else checkpointScripts.LoadCheckpoint(sceneTransition, checkpointTransition);
            }
        }
    }
}
