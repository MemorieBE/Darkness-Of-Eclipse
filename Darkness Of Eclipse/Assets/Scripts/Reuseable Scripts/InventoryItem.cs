﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]

/*! \brief A script that controls an inventory item.
 *
 *  [Reusable Script]
 */
public class InventoryItem : MonoBehaviour
{
    [Header("Item")]
    public int itemID; //!< The item ID.

    [Header("Inventory")]
    public InventoryScript inventoryScript; //!< The inventory script.

    private bool disable; //!< A boolean that disables the game object in a later function.

    void Update()
    {
         if (gameObject.GetComponent<Interactable>().interacted)
        {
            inventoryScript.InventoryUpdate(itemID, true);

            disable = true;
        }
    }

    void LateUpdate()
    {
        if (disable)
        {
            gameObject.SetActive(false);
            disable = false;
        }
    }
}
