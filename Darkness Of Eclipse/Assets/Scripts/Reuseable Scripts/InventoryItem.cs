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
    public int itemID; //!< The item ID.

    public void Activated()
    {
        InventoryScript.InventoryUpdateItem(itemID, true);
    }
}
