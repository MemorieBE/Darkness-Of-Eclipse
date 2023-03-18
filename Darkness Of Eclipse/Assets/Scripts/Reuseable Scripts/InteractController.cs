using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Linq;

/*! \brief A script that controls how the player interacts with game objects.
 *
 *  References: InvokeInteractable, InvokeItemBasedInteractable, InventoryScript, PlayerControllerCC.
 */
public class InteractController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private float interactDistance = 2f; //!< The distance of the interact raycast.
    [SerializeField] private int[] raycastIgnoreLayers; //!< The layers that the raycast will ignore.
    [SerializeField] private int checkParents = 1; //!< The amount of parents the collider will check for an interactable script.

    [Header("UI")]
    public GameObject interactUI; //!< The interact UI game object.
    [SerializeField] private Text interactText; //!< The interact UI text.
    [SerializeField] private Image interactImage; //!< The interact UI image.

    [Header("Action")]
    [SerializeField] private InputActionReference interactAction; //!< The interact action.

    private Action<InputAction.CallbackContext> interactHandler; //!< The interact handler.

    private bool interactInput; //!< A boolean that checks the interact input.

    private Collider lastCollider; //!< The last collider that was checked.
    private InvokeItemBasedInteractable lastColliderItemBasedInt; //!< The item based interactable script on the last collider checked.
    private InvokeInteractable lastColliderInt; //!< The interactable script on the last collider checked.

    void Awake()
    {
        interactHandler = ctx => interactInput = true;

        interactAction.action.Enable();
    }

    void OnEnable()
    {
        interactAction.action.performed += interactHandler;
    }

    void OnDisable()
    {
        interactAction.action.performed -= interactHandler;
    }

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
            if (hit.collider == lastCollider)
            {
                if (lastColliderItemBasedInt != null && !lastColliderItemBasedInt.activated)
                {
                    ItemBasedInteractableQuickCheck(lastColliderItemBasedInt, lastColliderInt);
                }
                else if (lastColliderInt != null)
                {
                    if (interactInput && !GameRules.freezePlayer) { lastColliderInt.Interacted(); }
                }
            }
            else
            {
                Transform currentParent = hit.collider.transform;
                for (int i = 0; true; i++)
                {
                    if (currentParent.GetComponent<InvokeItemBasedInteractable>() != null)
                    {
                        if (!currentParent.GetComponent<InvokeItemBasedInteractable>().activated)
                        {
                            InvokeItemBasedInteractable interactableScript = currentParent.GetComponent<InvokeItemBasedInteractable>();

                            interactUI.SetActive(true);

                            ItemBasedInteractableQuickCheck(interactableScript, currentParent.GetComponent<InvokeInteractable>());

                            break;
                        }
                    }
                    else if (currentParent.GetComponent<InvokeInteractable>() != null)
                    {
                        InvokeInteractable interactableScript = currentParent.GetComponent<InvokeInteractable>();

                        interactUI.SetActive(true);

                        interactText.text = interactableScript.prompt;
                        interactImage.sprite = interactableScript.sprite;

                        if (interactInput && !GameRules.freezePlayer) { interactableScript.Interacted(); }

                        break;
                    }
                    else
                    {
                        interactUI.SetActive(false);
                    }

                    if (currentParent.parent == null || i >= checkParents) { break; }
                    else { currentParent = currentParent.parent; }
                }

                lastCollider = hit.collider;
                lastColliderItemBasedInt = currentParent.GetComponent<InvokeItemBasedInteractable>();
                lastColliderInt = currentParent.GetComponent<InvokeInteractable>();
            }
        }
        else
        {
            interactUI.SetActive(false);

            lastCollider = null;
            lastColliderItemBasedInt = null;
            lastColliderInt = null;
        }

        interactInput = false;
    }

    private void ItemBasedInteractableQuickCheck(InvokeItemBasedInteractable itemBasedIntScript, InvokeInteractable intScript)
    {
        if (InventoryScript.inventoryItemStates[itemBasedIntScript.itemNeeded])
        {
            interactText.text = itemBasedIntScript.promptWithItem;
            interactImage.sprite = itemBasedIntScript.spriteWithItem;
        }
        else
        {
            interactText.text = itemBasedIntScript.promptWithoutItem;
            interactImage.sprite = itemBasedIntScript.spriteWithoutItem;
        }

        if (interactInput && !GameRules.freezePlayer)
        {
            if (InventoryScript.inventoryItemStates[lastColliderItemBasedInt.itemNeeded]) { lastColliderItemBasedInt.InteractedWithItem(); }
            else if (intScript != null) { intScript.Interacted(); }
        }
    }
}
