using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that triggers a smart line on activation.
 *
 *  References: SmartLineController.
 */
public class ActivateDialogue : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private SmartLineController smartLineScript; //!< The smart line controller script.
    [SerializeField] private int lineID; //!< The line ID.

    /*!
     *  A method that is triggers the line.
     */
    public void TriggerLine()
    {
        smartLineScript.ActivateLine(lineID);
    }
}
