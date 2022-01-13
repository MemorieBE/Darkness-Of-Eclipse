using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the achievements throughout the game.
 *
 *  Independent
 */
public class Achievements : MonoBehaviour
{
    public static bool[] achievementState; //!< Booleans that control whether or not an achievement has been achieved.

    public static bool fullCompletion; //!< A boolean that determines whether or not all achievements have been achieved.

    [Header("Achievements")]
    [SerializeField] private string[] achievementName; //!< The achievement names.
    [SerializeField] private string[] achievementDescription; //!< The achievement descriptions.
    [SerializeField] private bool[] achievementHidden; //!< Booleans that control whether or not an achievement is hidden.

    void Awake()
    {
        LoadAchievementData();
    }

    void Start()
    {
        FullCompletionCheck();
    }

    /*!
     *  A method that loads the achievement data from the local drive.
     */
    private void LoadAchievementData()
    {
        achievementState = new bool[achievementName.Length];
    }

    /*!
     *  A method that is activated to unlock an achievement.
     *  
     *  \param unlockedAchievement The achievement ID that has been unlocked.
     */
    public void AchievementUnlocked(int unlockedAchievement)
    {
        achievementState[unlockedAchievement] = true;

        FullCompletionCheck();
    }

    /*!
     *  A method that is activated to check if all achievements have been achieved.
     */
    private void FullCompletionCheck()
    {
        bool fullCompletionCheck = false;

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
