using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*! \brief A script that controls the player's flashlight.
 *
 *  References: PlayerControllerCC.
 */
public class FlashlightController : MonoBehaviour
{
    [Header("Inputs")]
    public bool isActive = false; //!< A boolean that determines whether or not the flashlight is active.
    public bool isRight = true; //!< A boolean that determines whether or not the flashlight is in the right hand.

    [Header("Action")]
    [SerializeField] private InputActionReference flashlightAction; //!< The flashlight action.

    private Animator flashlightAnimator; //!< The flashlight animator.

    void Awake()
    {
        flashlightAnimator = gameObject.GetComponent<Animator>();

        flashlightAction.action.Enable();
    }

    void OnEnable()
    {
        flashlightAction.action.performed += ToggleFlashlight;
    }

    void OnDisable()
    {
        flashlightAction.action.performed -= ToggleFlashlight;
    }

    /*!
     *  A method that toggles the flashlight.
     */
    private void ToggleFlashlight(InputAction.CallbackContext ctx)
    {
        if (gameObject.activeSelf && PlayerControllerCC.allowPlayerInputs)
        {
            isActive = !isActive;

            flashlightAnimator.SetBool("IsOn", isActive);
        }
    }

    /*!
     *  A method that updates the flashlight side.
     */
    public void UpdateFlashlightSide(bool right)
    {
        isRight = right;

        flashlightAnimator.SetBool("IsRight", isRight);
    }
}
