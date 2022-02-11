using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the current equippable.
 *
 *  References: EquippableController.
 */
public class CurrentEquippable : MonoBehaviour
{
    [Header("Equippables")]
    public static int currentEquippable = 0; //!< The current equippable ID.
    private int equippableUpdate = 0; //!< The integer of the current equippable in the last frame.
    [SerializeField] private GameObject[] equippable; //!< The equippable game objects.

    void Start()
    {
        UpdateEquippable();
    }

    void Update()
    {
        if (equippableUpdate != currentEquippable)
        {
            UpdateEquippable();
        }
    }

    /*!
     *  A method that updates the current equippable.
     */
    public void UpdateEquippable()
    {
        if (currentEquippable >= equippable.Length)
        {
            currentEquippable = equippable.Length - 1;
        }

        bool activeState = false;

        if (equippableUpdate > 0)
        {
            activeState = equippable[equippableUpdate].GetComponent<EquippableController>().isActive;

            equippable[equippableUpdate].GetComponent<EquippableController>().isActive = false;
            equippable[equippableUpdate].GetComponent<EquippableController>().ReadjustEquippableAnimation();

            equippable[equippableUpdate].SetActive(false);
        }

        if (currentEquippable > 0)
        {
            equippable[currentEquippable].SetActive(true);

            if (equippableUpdate > 0 && activeState) { equippable[currentEquippable].GetComponent<EquippableController>().isActive = true; }
            equippable[currentEquippable].GetComponent<EquippableController>().ReadjustEquippableAnimation();
        }

        equippableUpdate = currentEquippable;
    }
}
