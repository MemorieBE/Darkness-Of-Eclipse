using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that saves and loads certain scenes and checkpoints.
 *
 *  [Vital Script]
 */
public class SceneCheckpoints : MonoBehaviour
{
    public static int savedScene = 0; //!< The currently saved scene.
    public static int sceneCheckpoint = 0; //!< The current checkpoint.

    const int levelCount = 5; //!< The amount of playable levels with checkpoints.

    [Header("Spawn Point")]
    [SerializeField] private Transform spawnPoint; //!< The spawn point transform.

    [Header("Checkpoint Assets")]
    [SerializeField] private GameObject[] checkpointObjectGroups; //!< The object group game objects for certain checkpoints.
    [SerializeField] private Transform[] checkpointSpawnPoints; //!< The spawn point transforms for certain checkpoints.

    [Header("Loading Asset")]
    [SerializeField] private GameObject loadingObject; //!< The loading sprite game object.

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        if (sceneCheckpoint > 0 && SceneManager.GetActiveScene().buildIndex > 0) { ActivateCheckpoint(); }
    }

    /*!
     *  A method that loads the set checkpoint and scene.
     * 
     *  \param checkpoint The set checkpoint.
     *  \param scene The set scene.
     */
    public void LoadCheckpoint(int scene, int checkpoint)
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

        if (savedScene == SceneManager.GetActiveScene().buildIndex)
        {
            ActivateCheckpoint();

            Debug.Log("Load Point: Scene " + SceneManager.GetActiveScene().buildIndex + ", Checkpoint " + sceneCheckpoint);
        }
        else
        {
            LoadNewScene(scene);
        }
    }

    /*!
     *  A method that loads the saved scene.
     */
    public void LoadSavedScene()
    {
        int scene = 0;
        if (savedScene < 1 || savedScene > levelCount) { scene = 1; }
        else { scene = savedScene; }

        LoadNewScene(scene);
    }

    /*!
     *  A method that loads the first level.
     */
    public void LoadFirstLevel()
    {
        LoadNewScene(1);
    }

    /*!
     *  A method that reloads the current scene.
     */
    public void ReloadScene()
    {
        LoadNewScene(SceneManager.GetActiveScene().buildIndex);
    }

    /*!
     *  A method that loads a scene.
     */
    private void LoadNewScene(int scene)
    {
        GameRules.CancelAllInput();

        Time.timeScale = 0f;
        loadingObject.SetActive(true);

        SceneManager.LoadSceneAsync(scene);
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

        gameObject.GetComponent<TeleportCCBasic>().Activated();
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