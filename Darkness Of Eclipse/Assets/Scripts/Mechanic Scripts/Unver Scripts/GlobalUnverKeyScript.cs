using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnableDisable))]
/*! \brief A script that controls all the keys to collect in an Unver level.
 *
 *  [Mechanic Script]
 */

public class GlobalUnverKeyScript : MonoBehaviour
{
    [Header("Keys")]
    [SerializeField] private int[] serializedKeyItemIDs = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

    public static int keyCount = 0; //!< The amount of keys that have been collected so far.
    public static int[] keyItemIDs; //!< The key item IDs.

    [Header("References")]
    [SerializeField] private GhostStage ghostStageScript; //!< The script that controls the Unver stages.

    [Header("Achiement")] // (The targeted achievement will be achieved when the player collects the keys in a specific order.)
    public Achievements achievementScript; //!< The script that controls the achievements. (Can be left null.)
    public int achievementID; //!< The ID of the targeted achievement.
    public static int keyCountForAchievement = 0; //!< An integer that counts up when collected the correct next key.

    [Header("Debug")]
    [SerializeField] private bool updateKeyCount; //!< A boolean that updates thev key count when true.
    [SerializeField] private int visualKeyCount; //!< The keycount visualised in the inspector.

    private bool promptAvailable = true;

    void Awake()
    {
        keyItemIDs = serializedKeyItemIDs;
    }

    public static void UpdateKeyCountFromInventory()
    {
        int c = 0;
        for (int i = 0; i < keyItemIDs.Length; i++)
        {
            if (InventoryScript.inventoryItemStates[keyItemIDs[i]]) { c++; }
        }
        keyCount = c;
    }

    public void ReactToKeyCount()
    {
        ghostStageScript.ghostStagesActive = true;
        ghostStageScript.GhostSpawn();

        if (keyCount == 0) { ghostStageScript.ghostTier = 1; }
        else { ghostStageScript.ghostTier = keyCount; }

        if (promptAvailable)
        {
            gameObject.GetComponent<EnableDisable>().PositiveTrigger();
            promptAvailable = false;
        }

        if (keyCountForAchievement == keyItemIDs.Length)
        {
            if (achievementScript == null) { return; }
            achievementScript.AchievementUnlocked(achievementID);
        }

        visualKeyCount = keyCount;
    }

    void OnValidate()
    {
        if (updateKeyCount)
        {
            updateKeyCount = false;

            UpdateKeyCountFromInventory();
            ReactToKeyCount();
        }
    }
}
