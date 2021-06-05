using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a swinging equippable.
 *
 *  [Reusable Script]
 */
public class EquippableSwing : MonoBehaviour
{
    [Header("Swing Animation")]
    public float swingTime; //!< The swing animation duration in seconds.
    private bool isSwinging = false; //!< A boolean that determines whether or not the equippable is swinging.
    private float swingTimer = 0f; //!< The swing animation timer.

    private Animator animator; //!< The equippable animator.
    private EquippableController controller; //!< The equippable controller script.

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<EquippableController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && controller.isActive)
        {
            animator.SetBool("Swinging", true);
            isSwinging = true;
        }

        if (isSwinging)
        {

            if (swingTimer >= swingTime)
            {
                animator.SetBool("Swinging", false);
                isSwinging = false;
                swingTimer = 0f;

                return;
            }

            swingTimer += Time.deltaTime;
        }
    }
}
