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
     *  A method that collects the key.
     */
    public void CollectKey()
    {
        GlobalUnverKeyScript.UpdateKeyCountFromInventory();

        if (keyID + 1 == GlobalUnverKeyScript.keyCount) { GlobalUnverKeyScript.keyCountForAchievement++; }
        else { GlobalUnverKeyScript.keyCountForAchievement = 0; }

        globalKeyScript.ReactToKeyCount();

        if (saveScript != null)
        {
            saveScript.ActivateSavePoints();
        }
    }
}
