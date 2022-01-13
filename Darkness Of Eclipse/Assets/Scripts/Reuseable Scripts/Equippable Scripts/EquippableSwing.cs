using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a swinging equippable.
 *
 *  References: EquippableController, PlayerControllerCC.
 */
public class EquippableSwing : MonoBehaviour
{
    [Header("Swing Animation")]
    [SerializeField] private float swingTime; //!< The swing animation duration in seconds.

    private Animator animator; //!< The equippable animator.
    private EquippableController controller; //!< The equippable controller script.

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<EquippableController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && controller.isActive && PlayerControllerCC.allowPlayerInputs)
        {
            StartCoroutine(Swing());
        }
    }

    /*!
     *  A coroutine that swings the equippable.
     */
    private IEnumerator Swing()
    {
        animator.SetBool("Swinging", true);

        yield return new WaitForSeconds(swingTime);

        animator.SetBool("Swinging", false);
    }
}
