using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*! \brief A script that controls a swinging equippable.
 *
 *  References: EquippableController, PlayerControllerCC.
 */
public class EquippableAction : MonoBehaviour
{
    [Header("Action")]
    [SerializeField] private AnimationClip actionAnimation; //!< The action animation for reference.
    [SerializeField] private InputActionReference inputAction; //!< The equippable action.

    private Animator animator; //!< The equippable animator.
    private EquippableController controller; //!< The equippable controller script.

    void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<EquippableController>();

        inputAction.action.Enable();
    }

    void OnEnable()
    {
        inputAction.action.performed += TriggerEquippable;
    }

    void OnDisable()
    {
        inputAction.action.performed -= TriggerEquippable;
    }

    /*!
     *  A method that triggers the equippable.
     */
    private void TriggerEquippable(InputAction.CallbackContext ctx)
    {
        if (controller.isActive && PlayerControllerCC.allowPlayerInputs && !animator.GetBool("Action"))
        {
            StartCoroutine(Action());
        }
    }

    /*!
     *  A coroutine that swings the equippable.
     */
    private IEnumerator Action()
    {
        animator.SetBool("Action", true);

        for (float i = 0; i < actionAnimation.length; i += Time.deltaTime)
        {
            if (!controller.isActive)
            {
                animator.SetBool("Action", false);

                yield break;
            }

            yield return null;
        }

        animator.SetBool("Action", false);
    }
}
