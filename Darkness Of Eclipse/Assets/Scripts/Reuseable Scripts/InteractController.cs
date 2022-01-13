﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*! \brief A script that controls how the player interacts with game objects.
 *
 *  References: ADED_Interactable, ADED_ItemBasedInteractable, InventoryScript, PlayerControllerCC.
 */
public class InteractController : MonoBehaviour
{
    public float interactDistance = 2f; //!< The distance of the interact raycast.

    public string interactKeyBind = "e"; //!< The interact key bind.

    public int[] raycastIgnoreLayers; //!< The layers that the raycast will ignore.

    public GameObject interactUI; //!< The interact UI game object.
    public Text interactText; //!< The interact UI text.
    public Image interactImage; //!< The interact UI image.

    void Update()
    {
        Ray raycast = new Ray(gameObject.transform.position, gameObject.transform.forward);
        RaycastHit hit;

        int layerMask = 1 << raycastIgnoreLayers[0];
        foreach (var unverIgnoredLayer in raycastIgnoreLayers.Skip(1))
        {
            layerMask = (layerMask | 1 << unverIgnoredLayer);
        }
        layerMask = ~layerMask;

        if (Physics.Raycast(raycast, out hit, interactDistance, layerMask))
        {
            if (hit.collider.gameObject.GetComponent<ADED_ItemBasedInteractable>() != null && !hit.collider.gameObject.GetComponent<ADED_ItemBasedInteractable>().activated)
            {
                ADED_ItemBasedInteractable interactableScript = hit.collider.gameObject.GetComponent<ADED_ItemBasedInteractable>();

                interactUI.SetActive(true);

                if (InventoryScript.inventoryItemStates[interactableScript.itemNeeded])
                {
                    interactText.text = interactableScript.promptWithItem;
                    interactImage.sprite = interactableScript.spriteWithItem;
                }
                else
                {
                    interactText.text = interactableScript.promptWithoutItem;
                    interactImage.sprite = interactableScript.spriteWithoutItem;
                }

                if (Input.GetKeyDown(interactKeyBind) && PlayerControllerCC.allowPlayerInputs)
                {
                    if (InventoryScript.inventoryItemStates[interactableScript.itemNeeded]) { interactableScript.InteractedWithItem(); }
                    else if (hit.collider.gameObject.GetComponent<ADED_Interactable>() != null) { hit.collider.gameObject.GetComponent<ADED_Interactable>().Interacted(); }
                }
            }
            else if (hit.collider.gameObject.GetComponent<ADED_Interactable>() != null)
            {
                ADED_Interactable interactableScript = hit.collider.gameObject.GetComponent<ADED_Interactable>();

                interactUI.SetActive(true);

                interactText.text = interactableScript.prompt;
                interactImage.sprite = interactableScript.sprite;

                if (Input.GetKeyDown(interactKeyBind) && PlayerControllerCC.allowPlayerInputs) { interactableScript.Interacted(); }
            }
            else
            {
                interactUI.SetActive(false);
            }
        }
        else
        {
            interactUI.SetActive(false);
        }
    }
}
