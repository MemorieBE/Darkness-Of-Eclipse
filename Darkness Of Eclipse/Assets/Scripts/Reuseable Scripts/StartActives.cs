using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how objects are enabled and disabled at the start of the game.
 *
 *  [Reusable Script]
 */
public class StartActives : MonoBehaviour
{
    [Header("Enable Game Objects")]
    public GameObject[] enableObject; //!< Objects to enable on start.

    [Header("Disable Game Objects")]
    public GameObject[] disableObject; //!< Objects to disable on start.

    [Header("Editor")]
    public bool useInEditor = false; //!< A boolean that controls whether or not the objects will be enabled and disabled in the editor.

    void Start()
    {
        for (int i = 0; i < enableObject.Length; i++)
        {
            enableObject[i].SetActive(true);
        }

        for (int i = 0; i < disableObject.Length; i++)
        {
            disableObject[i].SetActive(false);
        }
    }
}
