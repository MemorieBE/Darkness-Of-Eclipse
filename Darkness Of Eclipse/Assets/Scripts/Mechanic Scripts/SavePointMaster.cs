using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that controls all save point scripts.
 *
 *  [Mechanic Script]
 */
public class SavePointMaster : MonoBehaviour
{
    private SceneSavePoint sceneSavePoint; //!< The scene save point script.
    private LIDSavePoint lIDSavePoint; //!< The Locked In Despair save point script.

    void Start()
    {
        sceneSavePoint = gameObject.GetComponent<SceneSavePoint>();
        lIDSavePoint = gameObject.GetComponent<LIDSavePoint>();
    }

    /*!
     *  A method that saves the scene data into the save point static variables.
     */
    public void ActivateSavePoints()
    {
        sceneSavePoint.ActivateSavePoint();

        if (SceneManager.GetActiveScene().buildIndex == lIDSavePoint.scene)
            lIDSavePoint.ActivateSavePoint();
        else
            lIDSavePoint.ResetSavePoint();
    }

    /*!
     *  A method that resets the save point static variables.
     */
    public void ResetSavePoints()
    {
        sceneSavePoint.ResetSavePoint();

        lIDSavePoint.ResetSavePoint();
    }

    /*!
     *  A method that loads the save point static variables into the scene data.
     */
    public void LoadSavePoints()
    {
        sceneSavePoint.LoadSavePoint();

        if (SceneManager.GetActiveScene().buildIndex == lIDSavePoint.scene)
            lIDSavePoint.LoadSavePoint();
    }
}
