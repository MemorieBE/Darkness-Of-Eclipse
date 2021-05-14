using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that saves and loads certain scenes and checkpoints.
 *
 *  [Reusable Script]
 */
public class SceneCheckpoints : MonoBehaviour
{
    public static bool autoSave = true; //!< A boolean that controls whether or not the game autosaves scenes and checkpoints.
    public static int sceneCheckpoint = 0; //!< The current/target checkpoint that isn't biased towards the PlayerPrefs saved data.

    [Header("Saving")]
    public bool savableScene = true; //!< A boolean that controls whether or not the current scene is savable.

    [Header("Spawn Point")]
    public Transform spawnPoint; //!< The spawn point transform.

    [Header("Checkpoint Assets")]
    public GameObject[] checkpointObjectGroups; //!< The object group game objects for certain checkpoints.
    public Transform[] checkpointSpawnPoints; //!< The spawn point transforms for certain checkpoints.

    [Header("Loading Asset")]
    public GameObject loadingObject; //!< The loading sprite game object.

    void Awake()
    {
        Time.timeScale = 1f;
    }

    void Start()
    {
        CheckSavedData();

        if (sceneCheckpoint > 0) LoadCheckpoint(sceneCheckpoint, SceneManager.GetActiveScene().buildIndex);
        else if (autoSave && savableScene) SaveCheckpoint();
    }

    /*!
     *  A method that creates PlayerPref variables if they are missing.
     */
    private void CheckSavedData()
    {
        if (!PlayerPrefs.HasKey("SavedScene"))
        {
            PlayerPrefs.SetInt("SavedScene", 1);
        }

        if (!PlayerPrefs.HasKey("SceneCheckpoint"))
        {
            PlayerPrefs.SetInt("SceneCheckpoint", 0);
        }
    }

    /*!
     *  A method that resets PlayerPrefs variables.
     */
    public void ResetSavedCheckpoint()
    {
        PlayerPrefs.SetInt("SavedScene", 1);
        PlayerPrefs.SetInt("SceneCheckpoint", 0);
    }

    /*!
     *  A method that loads the saved scene and checkpoint from the PlayerPrefs.
     */
    public void LoadSavedCheckpoint()
    {
        CheckSavedData();

        sceneCheckpoint = PlayerPrefs.GetInt("SceneCheckpoint");
        SceneManager.LoadScene(PlayerPrefs.GetInt("SavedScene"));
    }

    /*!
     *  A method that loads the set checkpoint and scene.
     * 
     *  \param checkpoint the set checkpoint.
     *  \param scene the set scene.
     */
    public void LoadCheckpoint(int checkpoint, int scene)
    {
        sceneCheckpoint = checkpoint;

        if (scene == SceneManager.GetActiveScene().buildIndex || scene == 0)
        {
            for (int i = 0; i < checkpointObjectGroups.Length; i++)
            {
                if (i == checkpoint) checkpointObjectGroups[i].SetActive(true);
                else checkpointObjectGroups[i].SetActive(false);
            }

            spawnPoint.position = checkpointSpawnPoints[checkpoint].position;
            spawnPoint.rotation = checkpointSpawnPoints[checkpoint].rotation;

            if (autoSave) SaveCheckpoint();
        }
        else
        {
            PauseScript.isPausable = false;

            Time.timeScale = 0f;
            loadingObject.SetActive(true);

            SceneManager.LoadSceneAsync(scene);
        }
    }

    /*!
     *  A method that reloads the current scene.
     */
    public void ReloadScene()
    {
        PauseScript.isPausable = false;

        Time.timeScale = 0f;
        loadingObject.SetActive(true);

        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    /*!
     *  A method that saves the checkpoint and scene to the PlayerPrefs.
     */
    public void SaveCheckpoint()
    {
        PlayerPrefs.SetInt("SceneCheckpoint", sceneCheckpoint);
        PlayerPrefs.SetInt("SavedScene", SceneManager.GetActiveScene().buildIndex);
    }
}