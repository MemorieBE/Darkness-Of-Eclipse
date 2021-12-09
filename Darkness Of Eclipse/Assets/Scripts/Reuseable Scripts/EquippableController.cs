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

    private Animator equippableAnimator; //!< The equippable animator.

    void Start()
    {
        equippableAnimator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && gameObject.activeSelf && PlayerControllerCC.allowPlayerInputs)
        {
            StartCoroutine(SwingCooldown());
        }
    }

    private IEnumerator SwingCooldown()
    {
        isActive = false;
        equippableAnimator.SetBool("IsActive", true);

        yield return new WaitForSeconds(animationStartPause);

        isActive = true;
        equippableAnimator.SetBool("IsActive", false);
    }
}
