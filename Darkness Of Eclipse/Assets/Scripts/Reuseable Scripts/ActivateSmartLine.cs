using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSmartLine : MonoBehaviour
{
    [Header("Line")]
    [SerializeField] private SmartLineController smartLineController;
    [SerializeField] private int lineID;

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        smartLineController.ActivateLine(lineID);
    }
}
