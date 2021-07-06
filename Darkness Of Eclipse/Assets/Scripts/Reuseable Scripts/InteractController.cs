﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*! \brief A script that controls how the player interacts with game objects.
 *
 *  [Reusable Script]
 */
public class InteractController : MonoBehaviour
{
    public float interactDistance = 2f; //!< The distance of the interact raycast.

    public string interactKeyBind = "e"; //!< The interact key bind.

    public int[] raycastIgnoreLayers; //!< The layers that the raycast will ignore.

    public GameObject interactUI; //!< The interact UI game object.
    public Text interactText; //!< The interact UI text.
    public Image interactImage; //!< The interact UI image.

    private bool inputToBool = false; //!< A boolean that determines whether or not the interact key has been pressed (To get input in Update()).

    void Update()
    {
        if (Input.GetKeyDown(interactKeyBind) && !PauseScript.isPaused && PlayerControllerCC.allowPlayerInputs) inputToBool = true;
    }

    void FixedUpdate()
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
            if (hit.collider.gameObject.GetComponent<Interactable>() != null && hit.collider.gameObject.GetComponent<Interactable>().enabled)
            {
                Interactable interactableScript = hit.collider.gameObject.GetComponent<Interactable>();

                interactUI.SetActive(true);

                interactText.text = interactableScript.prompt;
                interactImage.sprite = interactableScript.sprite;

                if (inputToBool) interactableScript.interacted = true;
            }
            else if (hit.collider.gameObject.GetComponent<ItemBasedInteractable>() != null && hit.collider.gameObject.GetComponent<ItemBasedInteractable>().enabled)
            {
                ItemBasedInteractable interactableScript = hit.collider.gameObject.GetComponent<ItemBasedInteractable>();

                interactUI.SetActive(true);

                if (InventoryScript.inventoryItemState[interactableScript.itemNeeded])
                {
                    interactText.text = interactableScript.promptWithItem;
                    interactImage.sprite = interactableScript.spriteWithItem;
                }
                else
                {
                    interactText.text = interactableScript.promptWithoutItem;
                    interactImage.sprite = interactableScript.spriteWithoutItem;
                }

                if (inputToBool) interactableScript.interacted = true;
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

        if (inputToBool) inputToBool = false;
    }
}
