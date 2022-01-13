using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls whether or not the player has interacted with the game object with a specific item or not.
 *
 *  References: InventoryScript.
 */
public class ADED_ItemBasedInteractable : MonoBehaviour
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

    [Header("ADED")]
    [SerializeField] private ADED aDED = ADED.elevate; //!< The ADED type.
    [SerializeField] private MonoBehaviour[] scripts; //!< The script that will be effected.

    [Header("Inputs")]
    [SerializeField] private bool disableOnActivation = false; //!< A boolean that controls whether or not the detector will be disabled when it is activated.
    [SerializeField] private bool resetsActivation = false; //!< A boolean that controls whether or not the deactivation will reset after it has been deactivated.
    [SerializeField] private float delay = 0f; //!< The amount of time in seconds the trigger will be delayed.
    public bool activated = false; //!< A boolean that determines whether or not the trigger events have been activated.

    private enum ADED
    {
        activate,
        deactivate,
        elevate,
        delevate
    }

    /*!
     *  A method that is triggered when interacted with the correct item.
     */
    public void InteractedWithItem()
    {
        if (activated) { return; }

        if (itemConsumable) { InventoryScript.InventoryUpdateItem(itemNeeded, false); }

        if (delay > 0f)
        {
            StartCoroutine(DelayedTrigger());
            return;
        }

        InvokeADED();
    }

    /*!
     *  An IEnumerator that delays the trigger.
     */
    private IEnumerator DelayedTrigger()
    {
        yield return new WaitForSeconds(delay);

        InvokeADED();
    }

    /*!
     *  A method that invokes the ADED specific trigger.
     */
    private void InvokeADED()
    {
        for (int i = 0; i < scripts.Length; i++)
        {
            activated = true;

            switch (aDED)
            {
                case ADED.activate:
                    scripts[i].Invoke("Activated", 0f);
                    break;
                case ADED.deactivate:
                    scripts[i].Invoke("Deactivated", 0f);
                    break;
                case ADED.elevate:
                    scripts[i].Invoke("Elevated", 0f);
                    break;
                case ADED.delevate:
                    scripts[i].Invoke("Delevated", 0f);
                    break;
            }
        }

        if (resetsActivation) { activated = false; }
        if (disableOnActivation) { gameObject.SetActive(false); }
    }
}
