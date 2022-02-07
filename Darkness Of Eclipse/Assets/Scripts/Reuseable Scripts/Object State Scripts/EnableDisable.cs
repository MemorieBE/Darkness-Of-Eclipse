using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that enables and disables game objects on activation.
 *
 *  Independent
 */
public class EnableDisable : MonoBehaviour
{
    [Header("Game Objects")]
    [SerializeField] private GameObject[] enableOnActivation; //!< The game objects to enable on activation.
    [SerializeField] private GameObject[] disableOnActivation; //!< The game objects to disable on activation.

    /*!
     *  A method that eneables the enable objects and disables the disable objects.
     */
    public void PositiveTrigger()
    {
        for (int i = 0; i < enableOnActivation.Length; i++) { enableOnActivation[i].SetActive(true); }
        for (int i = 0; i < disableOnActivation.Length; i++) { disableOnActivation[i].SetActive(false); }
    }

    /*!
     *  A method that disables the enable objects and enables the disable objects.
     */
    public void NegativeTrigger()
    {
        for (int i = 0; i < enableOnActivation.Length; i++) { enableOnActivation[i].SetActive(false); }
        for (int i = 0; i < disableOnActivation.Length; i++) { disableOnActivation[i].SetActive(true); }
    }
}
