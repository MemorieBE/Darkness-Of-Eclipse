using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the current save point in the scene.
 *
 *  [Data Script]
 */
public class SceneSavePoint : MonoBehaviour
{
    public static bool hasSavePoint = false; //!< A boolean that controls whether or not the scene currently has a save point.

    [Header("References")]
    [SerializeField] private DropEquippable equippableScript; //!< The equippable script.
    [SerializeField] private GameObject player; //!< The player game object.
    [SerializeField] private GameObject[] fluidActiveStateObjects; //!< The game objects that can be switch active and inactive throughout the scene.
    [SerializeField] private bool organiseObjects = false; //!< A boolean that organised the fluid active state object array in alphabetical order on validate.
    [SerializeField] private DoorScript[] doorScripts; //!< All the door scripts in the scene.

    public static List<EquiptItem> equippables; //!< A list of all dropped equippables.

    public static Vector3 playerPosition; //!< The player position to save.
    public static Vector3 playerRotation; //!< The player rotation to save.
    public static int[] equippableID; //!< The equippable IDs to save.
    public static Vector3[] equippablePosition; //!< The equippable positions to save.
    public static Vector3[] equippableRotation; //!< The equippable rotations to save.
    public static bool[] objectActive; //!< The game object active states to save.
    public static bool[] doorLocked; //!< The door locked states to save.
    public static bool[] doorOpen; //!< The door open states to save.

    void Awake()
    {
        equippables = new List<EquiptItem>();
    }

    /*!
     *  A method that saves the scene data into the save point static variables.
     */
    public void ActivateSavePoint()
    {
        hasSavePoint = true;

        equippableID = new int[equippables.Count];
        equippablePosition = new Vector3[equippables.Count];
        equippableRotation = new Vector3[equippables.Count];

        for (int i = 0; i < equippables.Count; i++)
        {
            equippableID[i] = equippables[i].equippableID;
            equippablePosition[i] = equippables[i].gameObject.transform.position;
            equippableRotation[i] = equippables[i].gameObject.transform.rotation.eulerAngles;
        }

        objectActive = new bool[fluidActiveStateObjects.Length];

        for (int i = 0; i < fluidActiveStateObjects.Length; i++)
        {
            objectActive[i] = fluidActiveStateObjects[i].activeSelf;
        }

        doorLocked = new bool[doorScripts.Length];
        doorOpen = new bool[doorScripts.Length];

        for (int i = 0; i < doorScripts.Length; i++)
        {
            doorLocked[i] = doorScripts[i].locked;
            doorOpen[i] = doorScripts[i].open;
        }

        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation.eulerAngles;
    }

    /*!
     *  A method that resets the save point static variables.
     */
    public static void ResetSavePoint()
    {
        hasSavePoint = false;

        equippableID = null;
        equippablePosition = null;
        equippableRotation = null;

        objectActive = null;

        doorLocked = null;
        doorOpen = null;

        playerPosition = Vector3.zero;
        playerRotation = Vector3.zero;
    }

    /*!
     *  A method that loads the save point static variables into the scene data.
     */
    public void LoadSavePoint()
    {
        if (!hasSavePoint) { return; }

        for (int i = 0; i < equippableID.Length; i++)
        {
            GameObject instantiatedEquippable = Instantiate(equippableScript.droppedEquippable[i]);
            instantiatedEquippable.transform.position = equippablePosition[i];
            instantiatedEquippable.transform.rotation = Quaternion.Euler(equippableRotation[i]);
        }

        for (int i = 0; i < fluidActiveStateObjects.Length; i++)
        {
            fluidActiveStateObjects[i].SetActive(objectActive[i]);
        }

        for (int i = 0; i < doorScripts.Length; i++)
        {
            doorScripts[i].locked = doorLocked[i];
            doorScripts[i].open = doorOpen[i];
            //doorScripts[i].UpdateDoorAnimationDirect(); [Doesn't really need, but could be useful later.]
        }

        if (player.GetComponent<CharacterController>() != null)
        {
            player.GetComponent<CharacterController>().enabled = false;
        }

        player.transform.position = playerPosition;
        player.transform.rotation = Quaternion.Euler(playerRotation);

        if (player.GetComponent<CharacterController>() != null)
        {
            player.GetComponent<CharacterController>().enabled = true;
        }
    }

    void OnValidate()
    {
        if (organiseObjects)
        {
            organiseObjects = false;

            List<GameObject> objectSorter = new List<GameObject>();
            foreach (GameObject fluidObject in fluidActiveStateObjects)
            {
                if (fluidObject != null)
                {
                    objectSorter.Add(fluidObject);
                }
            }

            objectSorter.Sort((a, b) => a.name.CompareTo(b.name));

            fluidActiveStateObjects = new GameObject[objectSorter.Count];
            for (int i = 0; i < objectSorter.Count; i++)
            {
                fluidActiveStateObjects[i] = objectSorter[i];
            }
        }
    }
}
