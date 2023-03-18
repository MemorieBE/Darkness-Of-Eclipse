using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*! \brief A script that loads a checkpoint on activation.
 *
 *  References: SceneCheckpoints.
 */
public class LoadCheckpoint : MonoBehaviour
{
    [Header("Checkpoint Script")]
    [SerializeField] private SceneCheckpoints checkpointScript; //!< The checkpoint script.

    [Header("Inputs")]
    [SerializeField] private int scene = 0; //!< The scene to load.
    [SerializeField] private int checkpoint = 0; //!< The checkpoint to load.
    [SerializeField] private bool reload = true; //!< A boolean that determines whether or not the scene will reload before loading a checkpoint.

    /*!
     *  A method that loads the checkpoint.
     */
    public void Load()
    {
        bool sceneChange = (scene != SceneManager.GetActiveScene().buildIndex && scene > 0);

        checkpointScript.LoadCheckpoint(scene, checkpoint, reload);
    }
}
