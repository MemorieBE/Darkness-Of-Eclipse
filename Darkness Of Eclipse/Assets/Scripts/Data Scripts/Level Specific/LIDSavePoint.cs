using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that controls the current save point in the scene for Locked In Despair.
 *
 *  [Data Script]
 */
public class LIDSavePoint : MonoBehaviour
{
    [Header("Scene")]
    public int scene; //!< The Locked In Despair scene index.

    [Header("References")]
    [SerializeField] private PlankScript[] plankScripts; //!< The plank scripts.
    [SerializeField] private LIDCorpseAnimation corpseScript; //!< The corpse script.
    [SerializeField] private GhostStage unverScript; //!< The Unvermeidlich script.

    public static bool hasSavePoint = false; //!< A boolean that controls whether or not the scene currently has a save point for Locked In Despair.

    public static bool[] savedPlankStates; //!< The plank states to save.

    public static bool savedCorpseAudioState; //!< The corpse audio state to save.

    public static bool savedUnverActiveState; //!< The Unver active state to save.
    public static bool savedUnverStagesState; //!< The Unver activation stage state to save.
    public static int savedUnverTier; //!< The Unver tier to  save.

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

        savedCorpseAudioState = corpseScript.audioPlayed;

        savedUnverActiveState = unverScript.gameObject.activeSelf;
        savedUnverStagesState = unverScript.ghostStagesActive;
        savedUnverTier = unverScript.ghostTier;

        savedKeyCount = GlobalUnverKeyScript.keyCount;
        savedKeyCountForAchievement = GlobalUnverKeyScript.keyCountForAchievement;
    }

    /*!
     *  A method that resets the save point static variables.
     */
    public static void ResetSavePoint()
    {
        hasSavePoint = false;

        savedPlankStates = new bool[0];

        savedCorpseAudioState = false;

        savedUnverActiveState = false;
        savedUnverStagesState = false;
        savedUnverTier = 0;

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

        corpseScript.audioPlayed = savedCorpseAudioState;

        unverScript.gameObject.SetActive(savedUnverActiveState);
        unverScript.ghostStagesActive = savedUnverStagesState;
        unverScript.ghostTier = savedUnverTier;
        
        GlobalUnverKeyScript.keyCount = savedKeyCount;
        GlobalUnverKeyScript.keyCountForAchievement = savedKeyCountForAchievement;
    }
}
