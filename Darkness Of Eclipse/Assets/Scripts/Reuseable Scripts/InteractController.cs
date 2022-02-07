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
    public float interactDistance = 2f; //!< The distance of the interact raycast.
    public int[] raycastIgnoreLayers; //!< The layers that the raycast will ignore.

    [Header("UI")]
    public GameObject interactUI; //!< The interact UI game object.
    public Text interactText; //!< The interact UI text.
    public Image interactImage; //!< The interact UI image.

    [Header("Action")]
    [SerializeField] private InputActionReference interactAction; //!< The interact action.

    private bool interactInput; //!< The interactInput.

    void Awake()
    {
        interactAction.action.performed += ctx => interactInput = true;
    }

    void OnEnable()
    {
        interactAction.action.Enable();
    }

    void OnDisable()
    {
        interactAction.action.Disable();
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
            if (hit.collider.gameObject.GetComponent<InvokeItemBasedInteractable>() != null && !hit.collider.gameObject.GetComponent<InvokeItemBasedInteractable>().activated)
            {
                InvokeItemBasedInteractable interactableScript = hit.collider.gameObject.GetComponent<InvokeItemBasedInteractable>();

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

                if (interactInput && PlayerControllerCC.allowPlayerInputs)
                {
                    if (InventoryScript.inventoryItemStates[interactableScript.itemNeeded]) { interactableScript.InteractedWithItem(); }
                    else if (hit.collider.gameObject.GetComponent<InvokeInteractable>() != null) { hit.collider.gameObject.GetComponent<InvokeInteractable>().Interacted(); }
                }
            }
            else if (hit.collider.gameObject.GetComponent<InvokeInteractable>() != null)
            {
                InvokeInteractable interactableScript = hit.collider.gameObject.GetComponent<InvokeInteractable>();

                interactUI.SetActive(true);

                interactText.text = interactableScript.prompt;
                interactImage.sprite = interactableScript.sprite;

                if (interactInput && PlayerControllerCC.allowPlayerInputs) { interactableScript.Interacted(); }
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

        interactInput = false;
    }
}
