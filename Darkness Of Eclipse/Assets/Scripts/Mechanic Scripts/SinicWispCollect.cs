using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InvokeInteractable))]

/*! \brief A script that collects a sinic wisp.
 *
 *  References: InvokeInteractable, SinicWispController.
 */
public class SinicWispCollect : MonoBehaviour
{
    [SerializeField] private SinicWispController controller; //!< The sinic wisp controller.

    /*!
     *  A method that is triggered on interaction.
     */
    public void Interacted()
    {
        controller.UpdateWispCount(1);

        Destroy(gameObject);
    }
}
