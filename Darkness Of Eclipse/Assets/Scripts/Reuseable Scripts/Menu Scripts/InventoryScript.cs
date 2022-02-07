using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the inventory.
 *
 *  Independent
 */
public class InventoryScript : MonoBehaviour
{
    [Header("Inventory Items")]
    [SerializeField] private string[] inventoryItemNames; //!< The inventory item names.
    [SerializeField] private GameObject[] inventoryItemUIs; //!< The inventory item UI images.

    public static bool[] inventoryItemStates; //!< The inventory item states.

    void Awake()
    {
        if (inventoryItemStates == null)
        {
            inventoryItemStates = new bool[inventoryItemNames.Length];
        }
    }

    /*!
     *  A method that updates the inventory.
     * 
     *  \param itemID The item ID.
     *  \param itemState The item state.
     */
    public void InventoryUpdateItem(int itemID, bool itemState)
    {
        inventoryItemStates[itemID] = itemState;

        inventoryItemUIs[itemID].SetActive(itemState);
    }
}
