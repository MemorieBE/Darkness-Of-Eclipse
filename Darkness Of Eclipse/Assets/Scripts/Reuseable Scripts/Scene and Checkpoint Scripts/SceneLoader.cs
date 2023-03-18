using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that loads a certain scene.
 *
 *  References: DataLoader, DataSaveMaster, GameRules, SceneCheckpoints.
 */
public class SceneLoader : MonoBehaviour
{
    [Header("Loading Asset")]
    [SerializeField] private GameObject loadingObject; //!< The loading sprite game object.

    void Awake()
    {
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex > SceneCheckpoints.levelCount) { DataSaveMaster.ResetAllKeptData(); }
    }

    /*!
     *  A method that loads the saved scene.
     */
    public void LoadSavedScene()
    {
        DataLoader.LoadAllData();

        int scene = 0;
        if (SceneCheckpoints.savedScene < 1 || SceneCheckpoints.savedScene > SceneCheckpoints.levelCount) { scene = 1; }
        else { scene = SceneCheckpoints.savedScene; }

        LoadNewScene(scene);
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
    public void LoadNewScene(int scene)
    {
        SceneCheckpoints.savedScene = scene;

        GameRules.CancelAllInput();

        Time.timeScale = 0f;
        loadingObject.SetActive(true);

        SceneManager.LoadSceneAsync(scene);
    }
}
