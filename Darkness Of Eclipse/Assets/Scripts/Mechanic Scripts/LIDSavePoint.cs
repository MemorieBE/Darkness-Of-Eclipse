using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that controls the current save point in the scene for Locked In Despair.
 *
 *  [Mechanic Script]
 */
public class LIDSavePoint : MonoBehaviour
{
    [Header("Scene")]
    public int scene; //!< The Locked In Despair scene index.

    [Header("References")]
    public PlankScript[] plankScripts; //!< The plank scripts;
    public LIDCorpseAnimation corpseScript; //!< The corpse script;

    public static bool hasSavePoint = false; //!< A boolean that controls whether or not the scene currently has a save point for Locked In Despair.

    public static bool[] savedPlankStates; //!< The plank states to save.

    public static bool saveCorpseAudioState; //!< The corpse audio state to save.

    public static int savedKeyCount; //!< The key count to save.
    public static int savedKeyCountForAchievement; //!< The achievement based key count to save.

    void Awake()
    {
        if (hasSavePoint && SceneManager.GetActiveScene().buildIndex == scene && hasSavePoint)
        {
            LoadSavePoint();
        }
    }

    /*!
     *  A method that saves the scene data into the save point static variables.
     */
    public void ActivateSavePoint()
    {
        hasSavePoint = true;

        savedPlankStates = new bool[plankScripts.Length];
        for (int i = 0; i < savedPlankStates.Length; i++)
        {
            savedPlankStates[i] = plankScripts[i].isBroken;
        }

        saveCorpseAudioState = corpseScript.audioPlayed;

        savedKeyCount = GlobalUnverKeyScript.keyCount;
        savedKeyCountForAchievement = GlobalUnverKeyScript.keyCountForAchievement;
    }

    /*!
     *  A method that resets the save point static variables.
     */
    public void ResetSavePoint()
    {
        hasSavePoint = false;

        savedPlankStates = new bool[0];

        saveCorpseAudioState = false;

        savedKeyCount = 0;
        savedKeyCountForAchievement = 0;
    }

    /*!
     *  A method that loads the save point static variables into the scene data.
     */
    public void LoadSavePoint()
    {
        if (!hasSavePoint) { return; }

        for (int i = 0; i < savedPlankStates.Length; i++)
        {
            if (savedPlankStates[i])
            {
                plankScripts[i].skipBreak = true;
                plankScripts[i].isBroken = true;
            }
        }

        corpseScript.audioPlayed = saveCorpseAudioState;
        
        GlobalUnverKeyScript.keyCount = savedKeyCount;
        GlobalUnverKeyScript.keyCountForAchievement = savedKeyCountForAchievement;
    }
}
