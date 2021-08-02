using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the current save point in the scene.
 *
 *  [Reusable Script]
 */
public class SceneSavePoint : MonoBehaviour
{
    public static bool hasSavePoint = false; //!< A boolean that controls whether or not the scene currently has a save point.

    [Header("References")]
    public DropEquippable equippableScript; //!< The equippable script.
    public GameObject player; //!< The player game object.
    public GameObject[] fluidActiveStateObject; //!< The game objects that can be switch active and inactive throughout the scene.
    [SerializeField] private bool organiseObjects = false; //!< A boolean that organised the fluid active state object array in alphabetical order on validate.
    public DoorScript[] doorScript; //!< All the door scripts in the scene.

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

        if (hasSavePoint)
        {
            for (int i = 0; i < equippableID.Length; i++)
            {
                Debug.Log("Instantiated");

                GameObject instantiatedEquippable = Instantiate(equippableScript.droppedEquippable[i]);
                instantiatedEquippable.transform.position = equippablePosition[i];
                instantiatedEquippable.transform.rotation = Quaternion.Euler(equippableRotation[i]);
            }

            for (int i = 0; i < fluidActiveStateObject.Length; i++)
            {
                fluidActiveStateObject[i].SetActive(objectActive[i]);
            }

            for (int i = 0; i < doorScript.Length; i++)
            {
                doorScript[i].locked = doorLocked[i];
                doorScript[i].open = doorOpen[i];
            }
        }
    }

    void Start()
    {
        if (hasSavePoint)
        {
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

        objectActive = new bool[fluidActiveStateObject.Length];

        for (int i = 0; i < fluidActiveStateObject.Length; i++)
        {
            objectActive[i] = fluidActiveStateObject[i].activeSelf;
        }

        doorLocked = new bool[doorScript.Length];
        doorOpen = new bool[doorScript.Length];

        for (int i = 0; i < doorScript.Length; i++)
        {
            doorLocked[i] = doorScript[i].locked;
            doorOpen[i] = doorScript[i].open;
        }

        playerPosition = player.transform.position;
        playerRotation = player.transform.rotation.eulerAngles;
    }

    /*!
     *  A method that resets the save point static variables.
     */
    public void ResetSavePoint()
    {
        hasSavePoint = false;

        equippableID = new int[0];
        equippablePosition = new Vector3[0];
        equippableRotation = new Vector3[0];

        objectActive = new bool[0];

        doorLocked = new bool[0];
        doorOpen = new bool[0];

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
            Debug.Log("Instantiated");

            GameObject instantiatedEquippable = Instantiate(equippableScript.droppedEquippable[i]);
            instantiatedEquippable.transform.position = equippablePosition[i];
            instantiatedEquippable.transform.rotation = Quaternion.Euler(equippableRotation[i]);
        }

        for (int i = 0; i < fluidActiveStateObject.Length; i++)
        {
            fluidActiveStateObject[i].SetActive(objectActive[i]);
        }

        for (int i = 0; i < doorScript.Length; i++)
        {
            doorScript[i].locked = doorLocked[i];
            doorScript[i].open = doorOpen[i];
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
            foreach (GameObject fluidObject in fluidActiveStateObject)
            {
                if (fluidObject != null)
                {
                    objectSorter.Add(fluidObject);
                }
            }

            objectSorter.Sort((a, b) => a.name.CompareTo(b.name));

            fluidActiveStateObject = new GameObject[objectSorter.Count];
            for (int i = 0; i < objectSorter.Count; i++)
            {
                fluidActiveStateObject[i] = objectSorter[i];
            }
        }
    }
}
