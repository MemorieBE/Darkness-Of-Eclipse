using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A class that manages the game data and stores variables to save and load.
 *
 *  [Data Script]
 */
public class GameData
{
    public bool hasSavedData;


    public int scene;
    public int checkpoint;


    public bool continuousHasSavedData;

    public bool[] inventoryState;

    public int currentEquippable;


    public bool SceneHasSavedData;

    public float[] playerPosition;
    public float[] playerRotation;

    public int[] equippableID;
    public float[][] equippablePosition;
    public float[][] equippableRotation;

    public bool[] objectActive;
    public bool[] doorLocked;
    public bool[] doorOpen;


    public bool LIDHasSavedData;

    public bool[] savedPlankStates;

    public bool savedCorpseAudioState;

    public bool savedUnverActiveState;
    public bool savedUnverStagesState;
    public int savedUnverTier;

    public int savedKeyCount;
    public int savedKeyCountForAchievement;
}
