using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls axe equippable.
 *
 *  [Mechanic Script]
 */
public class AxeScript : MonoBehaviour
{
    [Header("Swing Animation")]
    public float swingTime; //!< The swing animation duration in seconds.
    private bool isSwinging = false; //!< A boolean that determines whether or not the axe is swinging.
    private float swingTimer = 0f; //!< The swing animation timer.

    private Animator axeAnimator; //!< The axe animator.
    private EquippableController controller; //!< The equippable controller script.

    void Start()
    {
        axeAnimator = gameObject.GetComponent<Animator>();
        controller = gameObject.GetComponent<EquippableController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && controller.isActive)
        {
            axeAnimator.SetBool("AxeSwinging", true);
            isSwinging = true;
        }

        if (isSwinging)
        {

            if (swingTimer >= swingTime)
            {
                axeAnimator.SetBool("AxeSwinging", false);
                isSwinging = false;
                swingTimer = 0f;

                return;
            }

            swingTimer += Time.deltaTime;
        }
    }
}
