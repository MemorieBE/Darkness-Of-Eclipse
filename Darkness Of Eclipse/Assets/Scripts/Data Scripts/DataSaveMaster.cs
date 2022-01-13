using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ContinuousSavedData))]
[RequireComponent(typeof(SceneSavePoint))]
[RequireComponent(typeof(LIDSavePoint))]

/*! \brief A script that controls all save point scripts.
 *
 *  [Data Script]
 */
public class DataSaveMaster : MonoBehaviour
{
    private SceneSavePoint sceneSavePoint; //!< The scene save point script.
    private LIDSavePoint lIDSavePoint; //!< The Locked In Despair save point script.

    void Awake()
    {
        sceneSavePoint = gameObject.GetComponent<SceneSavePoint>();
        lIDSavePoint = gameObject.GetComponent<LIDSavePoint>();

        LoadSavePoints();
    }

    void Start()
    {
        if (SettingsValues.autoSave) { SaveSystem.SaveGame(); }
    }

    /*!
     *  A method that saves the scene data into the save point static variables.
     */
    public void ActivateSavePoints()
    {
        ContinuousSavedData.ActivateContinuousData();

        sceneSavePoint.ActivateSavePoint();

        if (SceneManager.GetActiveScene().buildIndex == lIDSavePoint.scene) { lIDSavePoint.ActivateSavePoint(); }
        else { LIDSavePoint.ResetSavePoint(); }

        if (SettingsValues.autoSave) { SaveSystem.SaveGame(); }
    }

    /*!
     *  A method that resets the scene based save point static variables for loading a new scene or checkpoint.
     */
    public static void ResetSceneBasedSavePoints()
    {
        SceneSavePoint.ResetSavePoint();

        LIDSavePoint.ResetSavePoint();
    }

    /*!
     *  A method that resets all the stored static variables.
     */
    public static void ResetAllKeptData()
    {
        SceneCheckpoints.ResetCheckpoint();

        ContinuousSavedData.ResetContinuousData();

        SceneSavePoint.ResetSavePoint();

        LIDSavePoint.ResetSavePoint();
    }

    /*!
     *  A method that loads the save point static variables into the scene data.
     */
    public void LoadSavePoints()
    {
        ContinuousSavedData.LoadContinuousData();

        sceneSavePoint.LoadSavePoint();

        if (SceneManager.GetActiveScene().buildIndex == lIDSavePoint.scene) { lIDSavePoint.LoadSavePoint(); }
    }
}
