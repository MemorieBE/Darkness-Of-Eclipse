using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls the open transition at the start of a scene or checkpoint.
 *
 *  [Reusable Script]
 */
public class OpenTransition : MonoBehaviour
{
    public AmbienceSoundLooper ambience; //!< The background ambience to activate.
    public Image blackUI; //!< The black UI panel to fade out.

    public float fadeTime; //!< The amount of time in seconds it will take for the black UI panel to fade out.

    private float timer; //!< The fade timer.
    private bool activeTimer; //!< A boolean that determines whether or not the timer is active.

    void Start()
    {
        Transition();
    }

    /*!
     *  A method that is activated to fade in the scene.
     */
    public void Transition()
    {
        ambience.activeSounds = true;

        timer = fadeTime;
        activeTimer = true;
    }

    void Update()
    {
        if (activeTimer)
        {
            Color uIColour = blackUI.color;

            if (timer > 0f)
            {
                timer -= Time.deltaTime;

                uIColour.a = timer / fadeTime;
                blackUI.color = uIColour;
            }
            else
            {
                timer = 0f;

                uIColour.a = 0f;
                blackUI.color = uIColour;

                activeTimer = false;
            }
        }
    }
}
