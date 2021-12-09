using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the variable to save that don't change value after a new scene.
 *
 *  [Data Script]
 */
public class ContinuousSavedData : MonoBehaviour
{
    public static bool hasSavedData = false; //!< A boolean that controls whether or not the game has continuous saved data.

    public static bool[] inventoryItemStates; //!< The inventory states to save.
    public static int currentEquippable; //!< The current equippable to save.

    void Start()
    {
        LoadContinuousData();
    }

    /*!
     *  A method that saves the continuous data into the continuous static variables.
     */
    public void ActivateContinuousData()
    {
        inventoryItemStates = InventoryScript.inventoryItemStates;

        currentEquippable = CurrentEquippable.currentEquippable;
    }

    /*!
     *  A method that resets the continuous static variables.
     */
    public static void ResetContinuousData()
    {
        inventoryItemStates = new bool[0];

        currentEquippable = 0;
    }

    /*!
     *  A method that loads the continuous static variables into the continuous data.
     */
    public void LoadContinuousData()
    {
        if (!hasSavedData) { return; }

        InventoryScript.inventoryItemStates = inventoryItemStates;

        CurrentEquippable.currentEquippable = currentEquippable;
    }
}
