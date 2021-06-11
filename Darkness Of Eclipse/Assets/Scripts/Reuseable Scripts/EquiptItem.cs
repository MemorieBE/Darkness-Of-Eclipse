using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the current equippable when interacting with the game object.
 *
 *  [Reusable Script]
 */
public class EquiptItem : MonoBehaviour
{
    public DropEquippable dropScript;

    public int equippableID; //!< The equippableID.

    public float resetYPoint = 0f;

    void Update()
    {
        if (gameObject.GetComponent<Interactable>() != null && gameObject.GetComponent<Interactable>().interacted)
        {
            if (CurrentEquippable.currentEquippable > 0) dropScript.EquippableDrop(CurrentEquippable.currentEquippable);

            CurrentEquippable.currentEquippable = equippableID;

            if (gameObject.GetComponent<Rigidbody>().isKinematic)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (gameObject.transform.position.y <= resetYPoint)
        {
            dropScript.EquippableDrop(equippableID);
            Destroy(gameObject);
        }
    }
}
