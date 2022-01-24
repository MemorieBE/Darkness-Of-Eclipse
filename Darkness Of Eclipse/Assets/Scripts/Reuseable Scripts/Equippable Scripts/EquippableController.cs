using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the player's current equippable.
 *
 *  References: FlashlightController, PlayerControllerCC.
 */
public class EquippableController : MonoBehaviour
{
    [Header("Inputs")]
    public bool isActive = false; //!< A boolean that determines whether or not the equippable is active.
    public float animationStartPause = 1f; //!< The ammount of time in seconds until the active animation will start.

    [Header("Assets")]
    [SerializeField] private FlashlightController flashlight; //!< The flashlight script.

    private Animator equippableAnimator; //!< The equippable animator.

    void Start()
    {
        equippableAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && gameObject.activeSelf && PlayerControllerCC.allowPlayerInputs)
        {
            if (isActive)
            {
                StartCoroutine(InactiveEquippable());
            }
            else
            {
                StartCoroutine(ActiveEquippable());
            }
        }
    }

    /*!
     *  A coroutine that activates the equippable.
     */
    private IEnumerator ActiveEquippable()
    {
        StopCoroutine(InactiveEquippable());

        isActive = true;

        flashlight.UpdateFlashlightSide(false);

        for (float i = 0; i < animationStartPause; i += Time.deltaTime)
        {
            if (!isActive)
            {
                yield break;
            }

            yield return null;
        }

        equippableAnimator.SetBool("IsActive", true);
    }

    /*!
     *  A coroutine that deactivates the equippable.
     */
    private IEnumerator InactiveEquippable()
    {
        StopCoroutine(ActiveEquippable());

        isActive = false;

        equippableAnimator.SetBool("IsActive", false);

        for (float i = 0; i < animationStartPause; i += Time.deltaTime)
        {
            if (isActive)
            {
                yield break;
            }

            yield return null;
        }

        flashlight.UpdateFlashlightSide(true);
    }
}
