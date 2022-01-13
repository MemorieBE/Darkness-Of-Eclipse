using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that exits the application using a method.
 *
 *  Independent
 */
public class ExitMethod : MonoBehaviour
{
    /*!
    *  A method that exits the application.
    */
    public void Exit()
    {
        Application.Quit();
    }
}
