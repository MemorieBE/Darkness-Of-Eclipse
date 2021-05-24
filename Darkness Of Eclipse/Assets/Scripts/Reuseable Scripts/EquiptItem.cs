using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the current equippable when interacting with the game object.
 *
 *  [Reusable Script]
 */
public class EquiptItem : MonoBehaviour
{
    public int equippableID; //!< The equippableID.

    void Update()
    {
        if (gameObject.GetComponent<Interactable>() != null && gameObject.GetComponent<Interactable>().interacted)
        {
            CurrentEquippable.currentEquippable = equippableID;

            gameObject.SetActive(false);
        }
    }
}
