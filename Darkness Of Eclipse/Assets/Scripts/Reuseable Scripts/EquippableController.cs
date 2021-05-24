using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the player's current equippable.
 *
 *  [Reusable Script]
 */
public class EquippableController : MonoBehaviour
{
    [Header("Inputs")]
    public bool isActive = false; //!< A boolean that determines whether or not the equippable is active.
    public float animationStartPause = 1f; //!< The ammount of time in seconds until the active animation will start.
    private float timer; //!< The animation pause timer.

    private Animator equippableAnimator; //!< The equippable animator.

    void Start()
    {
        equippableAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.activeSelf && !PauseScript.isPaused && PlayerControllerCC.allowPlayerInputs)
        {
            isActive = !isActive;

            if (isActive) timer = 0f;
        }

        if (isActive)
        {
            if (timer >= animationStartPause) equippableAnimator.SetBool("IsActive", true);

            timer += Time.deltaTime;
        }
        else
        {
            equippableAnimator.SetBool("IsActive", false);
        }
    }
}
