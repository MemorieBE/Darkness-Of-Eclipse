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

    [Header("References")]
    [SerializeField] private DocumentMenu notepadScript; //!< The notepad script.

    public static bool[] inventoryItemStates; //!< The inventory states to save.

    public static int currentEquippable; //!< The current equippable to save.

    public static int wispCount; //!< The wisp count.

    public static bool[] notepadActive; //!< The notepad object active states to save.

    public static bool[] notepadChapterStates; //!< The notepad chapter states.
    public static int notepadCurrentChapter; //!< The current notepad chapter.
    public static int notepadCurrentPage; //!< The current notepad page.
    const int notepadID = 0; //!< The notepad ID.

    /*!
     *  A method that saves the continuous data into the continuous static variables.
     */
    public void ActivateContinuousData()
    {
        hasSavedData = true;

        inventoryItemStates = InventoryScript.inventoryItemStates;

        currentEquippable = CurrentEquippable.currentEquippable;

        wispCount = SinicWispController.wispCount;

        notepadActive = new bool[notepadScript.savableObjectStates.Length];

        for (int i = 0; i < notepadActive.Length; i++)
        {
            notepadActive[i] = notepadScript.savableObjectStates[i].activeSelf;
        }

        if (DocumentMenu.chapterStates != null && DocumentMenu.chapterStates.Length > notepadID)
        {
            notepadChapterStates = DocumentMenu.chapterStates[notepadID];
            notepadCurrentChapter = DocumentMenu.chapter[notepadID];
            notepadCurrentPage = DocumentMenu.page[notepadID];
        }
    }

    /*!
     *  A method that resets the continuous static variables.
     */
    public static void ResetContinuousData()
    {
        hasSavedData = false;

        inventoryItemStates = null;

        currentEquippable = 0;

        wispCount = 0;

        notepadActive = null;

        notepadChapterStates = null;
        notepadCurrentChapter = 0;
        notepadCurrentPage = 0;
    }

    /*!
     *  A method that loads the continuous static variables into the continuous data.
     */
    public void LoadContinuousData()
    {
        if (!hasSavedData) { return; }

        InventoryScript.inventoryItemStates = inventoryItemStates;

        CurrentEquippable.currentEquippable = currentEquippable;

        SinicWispController.wispCount = wispCount;

        for (int i = 0; i < notepadActive.Length; i++)
        {
            notepadScript.savableObjectStates[i].SetActive(notepadActive[i]);
        }

        if (DocumentMenu.chapterStates != null && DocumentMenu.chapterStates.Length > notepadID)
        {
            DocumentMenu.chapterStates[notepadID] = notepadChapterStates;
            DocumentMenu.chapter[notepadID] = notepadCurrentChapter;
            DocumentMenu.page[notepadID] = notepadCurrentPage;
        }
    }
}
