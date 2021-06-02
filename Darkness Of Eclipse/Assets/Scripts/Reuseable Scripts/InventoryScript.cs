using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the inventory.
 *
 *  [Reusable Script]
 */
public class InventoryScript : MonoBehaviour
{
    [Header("Inventory Items")]
    public string[] inventoryItemName; //!< The inventory item names.
    public bool[] inventoryItemState; //!< The inventory item states.
    public static bool[] inventoryItemStateRemember; //!< The inventory item states to remember.

    [Header("UI")]
    public GameObject inventoryUI; //!< The inventory UI  game object.

    void Start()
    {
        if (inventoryItemStateRemember != null)
        {
            if (inventoryItemStateRemember.Length == inventoryItemState.Length)
            {
                inventoryItemState = inventoryItemStateRemember;
            }
        }
    }

    public void InventoryRemember()
    {
        inventoryItemStateRemember = inventoryItemState;
    }

    /*!
     *  A method that updates the inventory.
     * 
     *  \param itemID The item ID.
     *  \param itemState The item state.
     */
    public void InventoryUpdate(int itemID, bool itemState)
    {
        inventoryItemState[itemID] = itemState;
    }

    /*!
     *  A method that saves inventory data.
     */
    public void SaveInventory()
    {
        for (int i = 0; i < inventoryItemState.Length; i++)
        {
            PlayerPrefs.SetInt("Inventory: " + inventoryItemName[i], (inventoryItemStateRemember[i] ? 1 : 0));
        }
    }

    /*!
     *  A method that loads inventory saved data.
     */
    private void LoadInventory()
    {
        for (int i = 0; i < inventoryItemState.Length; i++)
        {
            if (!PlayerPrefs.HasKey("Inventory: " + inventoryItemName[i])) PlayerPrefs.SetInt("Inventory: " + inventoryItemName[i], 0);
            else InventoryUpdate(i, PlayerPrefs.GetInt("Inventory: " + inventoryItemName[i]) != 0);
        }
    }
}
