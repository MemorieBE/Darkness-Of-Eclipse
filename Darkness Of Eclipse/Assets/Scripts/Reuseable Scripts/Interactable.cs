using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls whether or not the player has interacted with the game object.
 *
 *  [Reusable Script]
 */
public class Interactable : MonoBehaviour
{
    [Header("Interact UI")]
    public string prompt = "Interact"; //!< The text prompt that will show up when hovering over game object.
    public Sprite sprite; //!< The sprite that will show up when hovering over game object.

    [HideInInspector] public bool interacted = false; //!< A boolean that determines whether or not the player has interacted with the game object.

    void LateUpdate()
    {
        if (interacted) interacted = false;
    }
}
