using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneLoader))]
[RequireComponent(typeof(TeleportCCBasic))]

/*! \brief A script that saves and loads certain scenes and checkpoints.
 *
 *  References: ContinuousSavedData, DataSaveMaster, SceneLoader, SceneSavePoint, TeleportCCBasic.
 */
public class SceneCheckpoints : MonoBehaviour
{
    public static int savedScene = 0; //!< The currently saved scene.
    public static int sceneCheckpoint = 0; //!< The current checkpoint.

    public static int levelCount = 5; //!< The amount of playable levels with checkpoints.

    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint; //!< The spawn point transform.

    [Header("Checkpoint Assets")]
    [SerializeField] private GameObject[] checkpointObjectGroups; //!< The object group game objects for certain checkpoints.
    [SerializeField] private Transform[] checkpointSpawnPoints; //!< The spawn point transforms for certain checkpoints.

    private SceneLoader sceneLoader; //!< The scene loader script.
    private TeleportCCBasic teleporter; //!< The teleporting script.

    void Awake()
    {
        sceneLoader = gameObject.GetComponent<SceneLoader>();
        teleporter = gameObject.GetComponent<TeleportCCBasic>();
    }

    void Start()
    {
        if (sceneCheckpoint > 0 && savedScene > 0 && savedScene == SceneManager.GetActiveScene().buildIndex) { ActivateCheckpoint(); }
    }

    /*!
     *  A method that loads the set checkpoint and scene.
     * 
     *  \param checkpoint The set checkpoint.
     *  \param scene The set scene.
     */
    public void LoadCheckpoint(int scene, int checkpoint, bool reload = true)
    {
        if (scene == 0) { savedScene = SceneManager.GetActiveScene().buildIndex; }
        else if (scene > levelCount) { savedScene = levelCount; }
        else { savedScene = scene; }

        if (scene >= SceneManager.sceneCountInBuildSettings)
        {
            scene = SceneManager.sceneCountInBuildSettings - 1;
        }

        sceneCheckpoint = checkpoint;

        DataSaveMaster.ResetSceneBasedSavePoints();
        ContinuousSavedData.ActivateContinuousData();

        Debug.Log("Load Point: Scene " + SceneManager.GetActiveScene().buildIndex + ", Checkpoint " + sceneCheckpoint);

        if (savedScene == SceneManager.GetActiveScene().buildIndex)
        {
            if (reload)
            {
                sceneLoader.ReloadScene();
            }
            else
            {
                ActivateCheckpoint();
            }
        }
        else
        {
            sceneLoader.LoadNewScene(scene);
        }
    }

    /*!
     *  A method that activates the current scene checkpoint.
     */
    private void ActivateCheckpoint()
    {
        if (sceneCheckpoint >= checkpointObjectGroups.Length)
        {
            sceneCheckpoint = checkpointObjectGroups.Length - 1;

            if (sceneCheckpoint == 0) { return; }
        }

        for (int i = 0; i < checkpointObjectGroups.Length; i++)
        {
            if (i == sceneCheckpoint) checkpointObjectGroups[i].SetActive(true);
            else checkpointObjectGroups[i].SetActive(false);
        }

        spawnPoint.position = checkpointSpawnPoints[sceneCheckpoint].position;
        spawnPoint.rotation = checkpointSpawnPoints[sceneCheckpoint].rotation;

        if (!SceneSavePoint.hasSavePoint) { teleporter.Activated(); }
    }

    /*!
     *  A method that resets the scene and checkpoint values.
     */
    public static void ResetCheckpoint()
    {
        savedScene = 0;
        sceneCheckpoint = 0;
    }
}