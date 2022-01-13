using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the jumpscare game objects.
 *
 *  Independent
 */
public class Jumpscare : MonoBehaviour
{
    [SerializeField] private GameObject[] jumpScare; //!< The jumpscare game objects.

    /*!
     *  A method that is activated to jumpscare the player.
     * 
     *  \param jumpscareID The jumpscare ID.
     */
    public void JumpScare(int jumpscareID)
    {
        for (int i = 0; i < jumpScare.Length; i++)
        {
            if (i == jumpscareID) jumpScare[i].SetActive(true);
        }
    }
}
