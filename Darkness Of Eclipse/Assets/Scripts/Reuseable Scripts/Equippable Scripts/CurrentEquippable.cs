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

        EquippableController newEquippable = equippable[currentEquippable].GetComponent<EquippableController>();
        EquippableController oldEquippable = equippable[equippableUpdate].GetComponent<EquippableController>();

        if (equippableUpdate > 0 && currentEquippable > 0)
        newEquippable.isActive = oldEquippable.isActive;

        for (int i = 1; i < equippable.Length; i++)
        {
            if (i == currentEquippable) equippable[i].SetActive(true);
            else
            {
                equippable[i].GetComponent<EquippableController>().isActive = false;
                equippable[i].SetActive(false);
            }
        }

        equippableUpdate = currentEquippable;
    }
}
