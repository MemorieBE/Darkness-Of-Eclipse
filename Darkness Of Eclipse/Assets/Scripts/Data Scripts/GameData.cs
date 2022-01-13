using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A class that manages the game data and stores variables to save and load.
 *
 *  [Data Script]
 */
 [System.Serializable]
public class GameData
{
    public int scene;
    public int checkpoint;


    public bool continuousHasSavedData;

    public bool[] inventoryItemStates;

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

    /*!
     *  A method that stores the corresponding data.
     */
    public GameData()
    {
        {
            scene = SceneCheckpoints.savedScene;
            checkpoint = SceneCheckpoints.sceneCheckpoint;
        }

        if (ContinuousSavedData.hasSavedData)
        {
            continuousHasSavedData = true;

            {
                inventoryItemStates = new bool[ContinuousSavedData.inventoryItemStates.Length];

                for (int i = 0; i < inventoryItemStates.Length; i++)
                {
                    inventoryItemStates[i] = ContinuousSavedData.inventoryItemStates[i];
                }
            }
            
            currentEquippable = ContinuousSavedData.currentEquippable;
        }
        else
        {
            continuousHasSavedData = false;

            inventoryItemStates = null;

            currentEquippable = 0;
        }

        if (SceneSavePoint.hasSavePoint) 
        {
            SceneHasSavedData = true;

            {
                playerPosition = new float[3];

                playerPosition[0] = SceneSavePoint.playerPosition.x;
                playerPosition[1] = SceneSavePoint.playerPosition.y;
                playerPosition[2] = SceneSavePoint.playerPosition.z;

                playerRotation = new float[3];

                playerRotation[0] = SceneSavePoint.playerRotation.x;
                playerRotation[1] = SceneSavePoint.playerRotation.y;
                playerRotation[2] = SceneSavePoint.playerRotation.z;
            }

            {
                equippableID = new int[SceneSavePoint.equippableID.Length];

                equippablePosition = new float[3][];

                equippablePosition[0] = new float[SceneSavePoint.equippableID.Length];
                equippablePosition[1] = new float[SceneSavePoint.equippableID.Length];
                equippablePosition[2] = new float[SceneSavePoint.equippableID.Length];

                equippableRotation = new float[3][];

                equippableRotation[0] = new float[SceneSavePoint.equippableID.Length];
                equippableRotation[1] = new float[SceneSavePoint.equippableID.Length];
                equippableRotation[2] = new float[SceneSavePoint.equippableID.Length];

                for (int i = 0; i < equippableID.Length; i++)
                {
                    equippableID = SceneSavePoint.equippableID;

                    equippablePosition[0][i] = SceneSavePoint.equippablePosition[i].x;
                    equippablePosition[1][i] = SceneSavePoint.equippablePosition[i].y;
                    equippablePosition[2][i] = SceneSavePoint.equippablePosition[i].z;

                    equippableRotation[0][i] = SceneSavePoint.equippableRotation[i].x;
                    equippableRotation[1][i] = SceneSavePoint.equippableRotation[i].y;
                    equippableRotation[2][i] = SceneSavePoint.equippableRotation[i].z;
                }
            }

            {
                objectActive = new bool[SceneSavePoint.objectActive.Length];

                doorLocked = new bool[SceneSavePoint.doorLocked.Length];
                doorOpen = new bool[SceneSavePoint.doorOpen.Length];

                for (int i = 0; i < objectActive.Length; i++)
                {
                    objectActive[i] = SceneSavePoint.objectActive[i];
                }

                for (int i = 0; i < doorOpen.Length; i++)
                {
                    doorLocked[i] = SceneSavePoint.doorLocked[i];
                    doorOpen[i] = SceneSavePoint.doorOpen[i];
                }
            }
        }
        else
        {
            SceneHasSavedData = false;
            
            playerPosition = null;
            playerRotation = null;
            
            equippableID = null;
            equippablePosition = null;
            equippableRotation = null;

            objectActive = null;
            doorLocked = null;
            doorOpen = null;
        }

        if (LIDSavePoint.hasSavePoint)
        {
            LIDHasSavedData = true;

            {
                savedPlankStates = new bool[LIDSavePoint.savedPlankStates.Length];

                for (int i = 0; i < savedPlankStates.Length; i++)
                {
                    savedPlankStates[i] = LIDSavePoint.savedPlankStates[i];
                }
            }
            
            savedCorpseAudioState = LIDSavePoint.savedCorpseAudioState;
            
            savedUnverActiveState = LIDSavePoint.savedUnverActiveState;
            savedUnverStagesState = LIDSavePoint.savedUnverStagesState;
            savedUnverTier = LIDSavePoint.savedUnverTier;
            
            savedKeyCount = LIDSavePoint.savedKeyCount;
            savedKeyCountForAchievement = LIDSavePoint.savedKeyCountForAchievement;
        }
        else
        {
            LIDHasSavedData = false;
            
            savedPlankStates = null;
            
            savedCorpseAudioState = false;
            
            savedUnverActiveState = false;
            savedUnverStagesState = false;
            savedUnverTier = 0;
            
            savedKeyCount = 0;
            
            savedKeyCountForAchievement = 0;
        }
    }
}
