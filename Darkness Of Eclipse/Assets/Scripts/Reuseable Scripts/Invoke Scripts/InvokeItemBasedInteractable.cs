using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls whether or not the player has interacted with the game object with a specific item or not.
 *
 *  References: InventoryScript.
 */
public class InvokeItemBasedInteractable : MonoBehaviour
{
    [Header("Interact UI With Item")]
    public string promptWithItem = "Unlock"; //!< The text prompt that will show up when hovering over game object with the item.
    public Sprite spriteWithItem; //!< The sprite that will show up when hovering over game object with the item.

    [Header("Interact UI Without Item")]
    public string promptWithoutItem = "Locked"; //!< The text prompt that will show up when hovering over game object with the item.
    public Sprite spriteWithoutItem; //!< The sprite that will show up when hovering over game object with the item.

    [Header("Inputs")]
    public int itemNeeded = 0; //!< The item needed.
    [SerializeField] private bool itemConsumable = true; //!< A boolean that controls whether or not the item is consumed after use.

    [Header("Invoke")]
    [SerializeField] private MonoBehaviour[] scripts; //!< The script that will be effected.
    [SerializeField] private string[] invokes; //!< The name of the functions to invoke.

    [Header("Inputs")]
    [SerializeField] private bool disableOnActivation = false; //!< A boolean that controls whether or not the detector will be disabled when it is activated.
    [SerializeField] private bool resetsActivation = false; //!< A boolean that controls whether or not the deactivation will reset after it has been deactivated.
    [SerializeField] private float delay = 0f; //!< The amount of time in seconds the trigger will be delayed.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    [Header("Inventory")]
    [SerializeField] private InventoryScript inventoryScript; //!< The inventory script.

    /*!
     *  A method that is triggered when interacted with the correct item.
     */
    public void InteractedWithItem()
    {
        if (activated) { return; }

        if (itemConsumable) { inventoryScript.InventoryUpdateItem(itemNeeded, false); }

        InvokeFunctions();
    }

    /*!
     *  A method that invokes the functions.
     */
    private void InvokeFunctions()
    {
        activated = true;

        for (int i = 0; i < scripts.Length; i++)
        {
            scripts[i].Invoke(invokes[i], delay);
        }

        if (resetsActivation) { activated = false; }
        if (disableOnActivation) { gameObject.SetActive(false); }
    }

    void OnValidate()
    {
        if (scripts.Length != invokes.Length)
        {
            string[] oldInvokes = invokes;
            invokes = new string[scripts.Length];

            for (int i = 0; i < invokes.Length && i < oldInvokes.Length; i++)
            {
                invokes[i] = oldInvokes[i];
            }
        }
    }
}
