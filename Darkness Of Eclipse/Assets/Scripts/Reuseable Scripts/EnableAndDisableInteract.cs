using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]

/*! \brief A script  that enables and disables game objects when interacted.
 *
 *  [Reusable Script]
 */
public class EnableAndDisableInteract : MonoBehaviour
{
    public GameObject[] enableObjects; //!< The game objects to enable when interacted.
    public GameObject[] disableObjects; //!< The game objects to disable when interacted.

    private void Update()
    {
        if (gameObject.GetComponent<Interactable>().interacted)
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
}
