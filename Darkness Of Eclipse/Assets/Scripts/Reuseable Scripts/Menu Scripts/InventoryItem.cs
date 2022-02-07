using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls an inventory item.
 *
 *  References: InventoryScript.
 */
public class InventoryItem : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private int itemID; //!< The item ID.

    [Header("Inventory")]
    [SerializeField] private InventoryScript inventoryScript; //!< The inventory script.

    /*!
     *  A method that adds the item to the inventory.
     */
    public void AddItem()
    {
        inventoryScript.InventoryUpdateItem(itemID, true);
    }
}
