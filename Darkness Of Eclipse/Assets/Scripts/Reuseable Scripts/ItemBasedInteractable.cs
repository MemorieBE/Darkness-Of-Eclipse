using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls whether or not the player has interacted with the game object with a specific item or not.
 *
 *  [Reusable Script]
 */
public class ItemBasedInteractable : MonoBehaviour
{
    [Header("Interact UI With Item")]
    public string promptWithItem = "Unlock"; //!< The text prompt that will show up when hovering over game object with the item.
    public Sprite spriteWithItem; //!< The sprite that will show up when hovering over game object with the item.

    [Header("Interact UI Without Item")]
    public string promptWithoutItem = "Locked"; //!< The text prompt that will show up when hovering over game object with the item.
    public Sprite spriteWithoutItem; //!< The sprite that will show up when hovering over game object with the item.

    [Header("Inputs")]
    public int itemNeeded = 0;
    public bool convertableWithItem = false;
    public bool itemConsumable = true;

    [HideInInspector] public bool interacted = false; //!< A boolean that determines whether or not the player has interacted with the game object.

    void LateUpdate()
    {
        if (interacted && InventoryScript.inventoryItemState[itemNeeded])
        {
            interacted = false;

            if (itemConsumable)
            {
                InventoryScript.InventoryUpdate(itemNeeded, false);
            }

            if (convertableWithItem)
            {
                if (gameObject.GetComponent<Interactable>() == null)
                {
                    Debug.LogError("Missing Interactable Script");
                    return;
                }
                else
                {
                    gameObject.GetComponent<Interactable>().enabled = true;
                }

                gameObject.GetComponent<ItemBasedInteractable>().enabled = false;
            }
        }

        if (interacted && !InventoryScript.inventoryItemState[itemNeeded]) interacted = false;
    }
}
