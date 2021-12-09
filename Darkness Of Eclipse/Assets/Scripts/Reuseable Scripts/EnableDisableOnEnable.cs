using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that enables and disables game objects on enable.
 *
 *  [Reusable Script]
 */
public class EnableDisableOnEnable : MonoBehaviour
{
    [SerializeField] private GameObject[] enableObjects; //!< The game objects to enable when the object is enabled.
    [SerializeField] private GameObject[] disableObjects; //!< The game objects to disable when the object is enabled.

    void OnEnable()
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