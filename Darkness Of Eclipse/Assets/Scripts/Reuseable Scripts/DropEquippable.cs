using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how an equippable is dropped.
 *
 *  [Reusable Script]
 */
public class DropEquippable : MonoBehaviour
{
    [Header("Player")]
    public Transform playerTransform; //!< The player transform.

    [Header("Dropping")]
    public float dropDistance = 1f; //!< The equippable drop distance.

    [Header("Equippables")]
    public GameObject[] droppedEquippable; //!< Equippable game objects to drop.

    /*!
     *  A method that drops an equippable.
     * 
     *  \param equippableID The equippable ID.
     */
    public void EquippableDrop(int equippableID)
    {
        GameObject instantiatedEquippable = Instantiate(droppedEquippable[equippableID]);

        instantiatedEquippable.transform.position = playerTransform.TransformPoint(Vector3.forward * dropDistance);
        instantiatedEquippable.transform.rotation = Quaternion.Euler(0f, playerTransform.transform.rotation.eulerAngles.y, 0f);

        instantiatedEquippable.SetActive(true);
    }
}
