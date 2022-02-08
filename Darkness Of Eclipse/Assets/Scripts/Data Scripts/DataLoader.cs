using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLoader : MonoBehaviour
{
    /*!
     *  A method that loads all data to the corresponding static variables.
     */
    public static void LoadAllData()
    {
        SaveSystem.LoadGame();

        GameData data = SaveSystem.LoadGame();

        {
            SceneCheckpoints.savedScene = data.scene;
            SceneCheckpoints.sceneCheckpoint = data.checkpoint;
        }


        {
            ContinuousSavedData.hasSavedData = data.continuousHasSavedData;

            if (ContinuousSavedData.hasSavedData)
            {
                ContinuousSavedData.inventoryItemStates = data.inventoryItemStates;

                ContinuousSavedData.currentEquippable = data.currentEquippable;

                ContinuousSavedData.wispCount = data.wispCount;

                ContinuousSavedData.notepadChapterStates = data.notepadChapterStates;
                ContinuousSavedData.notepadCurrentChapter = data.notepadCurrentChapter;
                ContinuousSavedData.notepadCurrentPage = data.notepadCurrentPage;
            }
            else
            {
                ContinuousSavedData.ResetContinuousData();
            }
        }


        {
            SceneSavePoint.hasSavePoint = data.SceneHasSavedData;

            if (SceneSavePoint.hasSavePoint)
            {
                SceneSavePoint.playerPosition.x = data.playerPosition[0];
                SceneSavePoint.playerPosition.y = data.playerPosition[1];
                SceneSavePoint.playerPosition.z = data.playerPosition[2];

                SceneSavePoint.playerRotation.x = data.playerRotation[0];
                SceneSavePoint.playerRotation.y = data.playerRotation[1];
                SceneSavePoint.playerRotation.z = data.playerRotation[2];

                SceneSavePoint.equippableID = data.equippableID;
                for (int i = 0; i < SceneSavePoint.equippableID.Length; i++)
                {
                    SceneSavePoint.equippablePosition[i].x = data.equippablePosition[0][i];
                    SceneSavePoint.equippablePosition[i].y = data.equippablePosition[1][i];
                    SceneSavePoint.equippablePosition[i].z = data.equippablePosition[2][i];

                    SceneSavePoint.equippableRotation[i].x = data.equippableRotation[0][i];
                    SceneSavePoint.equippableRotation[i].y = data.equippableRotation[1][i];
                    SceneSavePoint.equippableRotation[i].z = data.equippableRotation[2][i];
                }

                SceneSavePoint.objectActive = data.objectActive;
                SceneSavePoint.doorLocked = data.doorLocked;
                SceneSavePoint.doorOpen = data.doorOpen;
            }
            else
            {
                SceneSavePoint.ResetSavePoint();
            }
        }


        {
            LIDSavePoint.hasSavePoint = data.LIDHasSavedData;

            if (LIDSavePoint.hasSavePoint)
            {
                LIDSavePoint.savedPlankStates = data.savedPlankStates;

                LIDSavePoint.savedCorpseAudioState = data.savedCorpseAudioState;

                LIDSavePoint.savedUnverActiveState = data.savedUnverActiveState;
                LIDSavePoint.savedUnverStagesState = data.savedUnverStagesState;
                LIDSavePoint.savedUnverTier = data.savedUnverTier;

                LIDSavePoint.savedKeyCount = data.savedKeyCount;
                LIDSavePoint.savedKeyCountForAchievement = data.savedKeyCountForAchievement;
            }
            else
            {
                LIDSavePoint.ResetSavePoint();
            }
        }
    }
}
