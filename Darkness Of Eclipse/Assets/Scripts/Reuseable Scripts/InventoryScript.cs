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
    public GameObject[] inventoryItemUI; //!< The inventory item UI images.
    public static bool[] inventoryItemState; //!< The inventory item states.

    [Header("UI")]
    public GameObject inventoryUI; //!< The inventory UI  game object.

    void Start()
    {
        if (inventoryItemState == null)
        {
            inventoryItemState = new bool[inventoryItemName.Length];
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < inventoryItemUI.Length; i++)
        {
            if (inventoryItemUI[i] != null)
            {
                if (inventoryItemUI[i].activeSelf != inventoryItemState[i]) inventoryItemUI[i].SetActive(inventoryItemState[i]);
            }
        }
    }

    /*!
     *  A method that updates the inventory.
     * 
     *  \param itemID The item ID.
     *  \param itemState The item state.
     */
    public static void InventoryUpdate(int itemID, bool itemState)
    {
        inventoryItemState[itemID] = itemState;
    }
}
