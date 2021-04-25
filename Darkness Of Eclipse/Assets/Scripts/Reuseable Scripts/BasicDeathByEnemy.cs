using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how the player dies from an enemy.
 *
 *  [Reusable Script]
 */
public class BasicDeathByEnemy : MonoBehaviour
{
    [Header("Jumpscare")]
    public bool enableJumpscare = false; //!< A boolean that controls whether or not the death involves a jumpscare.
    public GameObject canvas; //!< The canvas game object that holds the jumpscare script.
    public int jumpscareID = 0; //!< The jumpscare ID.

    /*!
     *  A method that is activated to kill the player with an enemy.
     */
    public void DeathByEnemy()
    {
        if (enableJumpscare)
        {
            canvas.GetComponent<Jumpscare>().JumpScare(jumpscareID);
            Debug.Log("Death By Enemy: " + gameObject.name);
        }

        if (!PlayerPrefs.HasKey("Death Counter")) PlayerPrefs.SetInt("Death Counter", 0);
        PlayerPrefs.SetInt("Death Counter", PlayerPrefs.GetInt("Death Counter") + 1);
    }
}
