using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
