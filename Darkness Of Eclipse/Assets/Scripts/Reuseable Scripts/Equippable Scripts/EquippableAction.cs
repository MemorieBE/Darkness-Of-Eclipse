using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a swinging equippable.
 *
 *  References: EquippableController, PlayerControllerCC.
 */
public class EquippableAction : MonoBehaviour
{
    [Header("Action Animation")]
    [SerializeField] private AnimationClip action; //!< The action animation for reference.

    private Animator animator; //!< The equippable animator.
    private EquippableController controller; //!< The equippable controller script.

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<EquippableController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && controller.isActive && PlayerControllerCC.allowPlayerInputs && !animator.GetBool("Action"))
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

        for (float i = 0; i < action.length; i += Time.deltaTime)
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
