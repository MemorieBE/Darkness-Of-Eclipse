using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/*! \brief A script that places cylinder primitive objects in the possitions of each tree on terrains.
 *
 *  Independent
 */
[ExecuteInEditMode]
public class TerrainTreeMarker : MonoBehaviour
{
    [SerializeField] private bool generateMarkedTrees = false; //!< The button that triggers the tree mark generation process.

    [Header("Terrain Assets")]
    [SerializeField] private Terrain[] terrains; //!< The terrains.

    void OnValidate()
    {
        if (generateMarkedTrees)
        {
            generateMarkedTrees = false;

            GameObject markParent = new GameObject("Tree Marker Parent");
            markParent.transform.SetParent(gameObject.transform);

            int treeID = 0;

            foreach (Terrain terrain in terrains)
            {
                TerrainData data = terrain.terrainData;
                foreach (TreeInstance tree in data.treeInstances)
                {
                    float dataX = data.size.x;
                    float dataY = data.size.y;
                    float dataZ = data.size.z;

                    GameObject newTree = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                    newTree.transform.position = terrain.GetPosition() + new Vector3(tree.position.x * dataX, tree.position.y * dataY, tree.position.z * dataZ);
                    newTree.transform.localScale = new Vector3(tree.widthScale, tree.heightScale, tree.widthScale);
                    newTree.name = "Marked Tree " + treeID;
                    newTree.transform.SetParent(markParent.transform);

                    treeID++;
                }
            }
        }
    }
}
