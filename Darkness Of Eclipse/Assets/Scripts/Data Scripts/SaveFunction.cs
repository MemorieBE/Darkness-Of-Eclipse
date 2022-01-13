using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that activates the save method.
 *
 *  [Data Script]
 */
public class SaveFunction : MonoBehaviour
{
    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        SaveSystem.SaveGame();
    }
}
