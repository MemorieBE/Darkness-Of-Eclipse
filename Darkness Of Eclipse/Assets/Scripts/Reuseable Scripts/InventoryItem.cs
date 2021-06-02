using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]

public class InventoryItem : MonoBehaviour
{
    [Header("Item")]
    public int itemID;

    [Header("Inventory")]
    public InventoryScript inventoryScript;

    void Update()
    {
         if (gameObject.GetComponent<Interactable>().interacted)
        {
            inventoryScript.InventoryUpdate(itemID, true);

            gameObject.SetActive(false);
        }
    }
}
