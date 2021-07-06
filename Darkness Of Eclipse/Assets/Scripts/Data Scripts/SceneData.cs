using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A class that controls the scene data.
 *
 *  [Data Script]
 */
public class SceneData
{
    public int scene;
    public int checkpoint;

    public bool[] inventoryState;
    public int currentEquippable;

    public bool hasSavePoint;

    public float[] playerPosition;
    public float[] playerRotation;

    public float[][] equippablePosition;
    public float[][] equippableRotation;

    public SceneData(SceneSavePoint sceneSavePoint)
    {
        scene = SceneManager.GetActiveScene().buildIndex;
        checkpoint = SceneCheckpoints.sceneCheckpoint;

        for (int i = 0; i < inventoryState.Length; i++)
        {
            inventoryState[i] = InventoryScript.inventoryItemState[i];
        }

        currentEquippable = CurrentEquippable.currentEquippable;
    }

}
