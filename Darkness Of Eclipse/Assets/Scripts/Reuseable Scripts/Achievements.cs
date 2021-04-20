using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the achievements throughout the game.
 *
 *  [Reusable Script]
 */
public class Achievements : MonoBehaviour
{
    [Header("Achievements")]
    public string[] achievementName; //!< The achievement names.
    public bool[] achievementState; //!< Booleans that control whether or not an achievement has been achieved.

    private bool fullCompletionCheck; //!< A boolean that checks whether or not all achievements have been achieved.
    public bool fullCompletion; //!< A boolean that determines whether or not all achievements have been achieved.

    [Header("Descriptions")]
    public string[] achievementDescription; //!< The achievement descriptions.
    public bool[] achievementHidden; //!< Booleans that control whether or not an achievement is hidden.

    void Awake()
    {
        AchievementSync();
    }

    void Start()
    {
        FullCompletionCheck();
    }

    //! A method that is activated to unlock an achievement.
    /*!
     *  \param The achievement ID that has been unlocked.
     */
    public void AchievementUnlocked(int unlockedAchievement)
    {
        achievementState[unlockedAchievement] = true;
        PlayerPrefs.SetInt("Achievement: " + achievementName[unlockedAchievement], (achievementState[unlockedAchievement] ? 1 : 0));

        FullCompletionCheck();
    }

    //! A method that is activated to sync the achievement states with the PlayerPrefs stored data.
    private void AchievementSync()
    {
        for (int i = 0; i < achievementName.Length; i++)
        {
            if (!PlayerPrefs.HasKey("Achievement: " + achievementName[i])) PlayerPrefs.SetInt("Achievement: " + achievementName[i], 0);
            else achievementState[i] = (PlayerPrefs.GetInt("Achievement: " + achievementName[i]) != 0);
        }
    }

    //! A method that is activated to check if all achievements have been achieved.
    private void FullCompletionCheck()
    {
        for (int i = 0; i < achievementName.Length; i++)
        {
            if (achievementState[i])
            {
                fullCompletionCheck = true;
            }
            else
            {
                fullCompletionCheck = false;
                break;
            }
        }

        if (fullCompletionCheck) fullCompletion = true;
    }
}
