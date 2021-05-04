using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a method that enables and disables game objects.
 *
 *  [Reusable Script]
 */
public class EnableAndDisableMethod : MonoBehaviour
{
    public GameObject[] enableObjects; //!< The game objects to enable when the method is activated.
    public GameObject[] disableObjects; //!< The game objects to disable when the method is activated.

    /*!
    *  A method that enables and disable game objects.
    */
    public void EnableDisable()
    {
        for (int i = 0; i < enableObjects.Length; i++)
        {
            enableObjects[i].SetActive(true);
        }

        for (int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(false);
        }
    }
}
