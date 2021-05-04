using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls a toggle that enables and disables game objects.
 *
 *  [Reusable Script]
 */
public class EnableAndDisableToggle : MonoBehaviour
{
    public Toggle toggle; //!< The toggle.

    public GameObject[] isOnEnable; //!< The game objects to enable when the toggle is activated and vice versa.
    public GameObject[] isOnDisable; //!< The game objects to disable when the toggle is activated and vice versa.

    void Update()
    {
        for (int i = 0; i < isOnEnable.Length; i++) isOnEnable[i].SetActive(toggle.isOn);
        for (int i = 0; i < isOnDisable.Length; i++) isOnDisable[i].SetActive(toggle.isOn);
    }
}
