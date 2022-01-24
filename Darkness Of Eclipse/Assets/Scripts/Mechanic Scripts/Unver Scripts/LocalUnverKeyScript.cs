using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls one of the keys to collect in an Unver level.
 *
 *  [Mechanic Script]
 */
public class LocalUnverKeyScript : MonoBehaviour
{
    [Header("Key")]
    [SerializeField] private int keyID; //!< The ID for the key.

    [Header("Assets")]
    [SerializeField] private DataSaveMaster saveScript; //!< The saving script.
    [SerializeField] private GlobalUnverKeyScript globalKeyScript; //!< The global key script.

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        GlobalUnverKeyScript.keyCount ++;
        if (keyID == GlobalUnverKeyScript.keyCount) GlobalUnverKeyScript.keyCountForAchievement++;
        else GlobalUnverKeyScript.keyCountForAchievement = 0;

        globalKeyScript.UpdateKeyCount();

        if (saveScript != null)
        {
            saveScript.ActivateSavePoints();
        }
    }
}
