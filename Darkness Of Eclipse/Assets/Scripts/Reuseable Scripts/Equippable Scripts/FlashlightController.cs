using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the player's flashlight.
 *
 *  References: PlayerControllerCC.
 */
public class FlashlightController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private string keyBind = "f"; //!< The key bind used to activate flashlight.
    public bool isActive = false; //!< A boolean that determines whether or not the flashlight is active.
    public bool isRight = true; //!< A boolean that determines whether or not the flashlight is in the right hand.

    private Animator flashlightAnimator; //!< The flashlight animator.

    void Start()
    {
        flashlightAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyBind) && gameObject.activeSelf && PlayerControllerCC.allowPlayerInputs)
        {
            isActive = !isActive;
        }

        flashlightAnimator.SetBool("IsOn", isActive);
        flashlightAnimator.SetBool("IsRight", isRight);
    }
}
