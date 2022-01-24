using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls all the keys to collect in an Unver level.
 *
 *  [Mechanic Script]
 */
public class GlobalUnverKeyScript : MonoBehaviour
{
    [Header("Keys")]
    public static int keyCount = 0; //!< The amount of keys that have been collected so far.
    public int totalKeys = 7; //!< The total amount of keys in the level.
    [SerializeField] private int visualKeyCount; //!< The keycount visualised in the inspector.
    [SerializeField] private bool addKey = false; //!< A boolean that adds a key to the total key count when true.

    [Header("Ghost Stage Script")]
    [SerializeField] private GhostStage ghostStageScript; //!< The script that controls the Unver stages.

    [Header("Achiement")] // (The targeted achievement will be achieved when the player collects the keys in a specific order.)
    public Achievements achievementScript; //!< The script that controls the achievements. (Can be left null.)
    public int achievementID; //!< The ID of the targeted achievement.
    public static int keyCountForAchievement = 0; //!< An integer that counts up when collected the correct next key.

    void Update()
    {
        if (addKey)
        {
            addKey = false;
            if (keyCount < totalKeys) keyCount++;
        }
    }

    public void UpdateKeyCount()
    {
        ghostStageScript.ghostStagesActive = true;
        ghostStageScript.GhostSpawn();

        if (keyCount == 0) ghostStageScript.ghostTier = 1;
        else ghostStageScript.ghostTier = keyCount;

        if (keyCountForAchievement == totalKeys)
        {
            if (achievementScript == null) { return; }
            achievementScript.AchievementUnlocked(achievementID);
        }

        visualKeyCount = keyCount;
    }
}
