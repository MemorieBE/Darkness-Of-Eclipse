using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls the sensitivity settings slider.
 *
 *  Independent
 */
public class SettingsValues : MonoBehaviour
{
    [Header("Sensitivity")]
    [SerializeField] private Text sensitivityValueText; //!< The slider value text.
    [SerializeField] private Slider sensitivitySlider; //!< The sensitivity slider.
    //
    public static float mouseSensitivity = 100f; //!< The mouse sensitivity value.

    [Header("Auto Save")]
    [SerializeField] private Toggle autoSaveToggle; //!< The auto save toggle UI.
    //
    public static bool autoSave = true; //!< A boolean that controls whether or not auto save is active.

    [Header("Subtitles")]
    [SerializeField] private Toggle subtitlesToggle; //!< The subtitles toggle UI.
    //
    public static bool subtitlesActive = false; //!< A boolean that controls whether or not subtitles are active.
    public static bool subtitlesShowNames = false; //!< A boolean that controls whether or not subtitles names are shown.
    public static bool subtitlesShowHidden = false; //!< A boolean that controls whether or not hidden subtitles are shown.

    void Awake()
    {
        {
            if (!PlayerPrefs.HasKey("Sensitivity"))
            {
                PlayerPrefs.SetFloat("Sensitivity", 100f);

                mouseSensitivity = 100f;
            }
            else
            {
                mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");
            }

            sensitivitySlider.value = mouseSensitivity;
        }

        {
            if (!PlayerPrefs.HasKey("Auto Save"))
            {
                PlayerPrefs.SetInt("Auto Save", 1);

                autoSave = true;
            }
            else
            {
                autoSave = PlayerPrefs.GetInt("Auto Save") == 1;
            }

            autoSaveToggle.isOn = autoSave;
        }

        {
            if (!PlayerPrefs.HasKey("Subtitles Active"))
            {
                PlayerPrefs.SetInt("Subtitles Active", 0);

                subtitlesActive = false;
            }
            else
            {
                subtitlesActive = PlayerPrefs.GetInt("Subtitles Active") == 1;
            }

            subtitlesToggle.isOn = subtitlesActive;
        }

        {
            if (!PlayerPrefs.HasKey("Subtitles Show Names"))
            {
                PlayerPrefs.SetInt("Subtitles Show Names", 0);

                subtitlesShowNames = false;
            }
            else
            {
                subtitlesShowNames = PlayerPrefs.GetInt("Subtitles Show Names") == 1;
            }
        }

        {
            if (!PlayerPrefs.HasKey("Subtitles Show Hidden"))
            {
                PlayerPrefs.SetInt("Subtitles Show Hidden", 0);

                subtitlesShowHidden = false;
            }
            else
            {
                subtitlesShowHidden = PlayerPrefs.GetInt("Subtitles Show Hidden") == 1;
            }
        }
    }

    /*!
     *  A method that updates the sensitivity UI.
     */
    public void SensitivityUIUpdate()
    {
        sensitivityValueText.text = sensitivitySlider.value.ToString();
    }

    /*!
     *  A method that updates and applies all settings based on the UI values.
     */
    public void ApplyAllChanges()
    {
        {
            mouseSensitivity = sensitivitySlider.value;

            autoSave = autoSaveToggle.isOn;

            subtitlesActive = subtitlesToggle.isOn;
        }

        {
            PlayerPrefs.SetFloat("Sensitivity", mouseSensitivity);

            PlayerPrefs.SetInt("Auto Save", autoSave ? 1 : 0);

            PlayerPrefs.SetInt("Subtitles Active", subtitlesActive ? 1 : 0);
            PlayerPrefs.SetInt("Subtitles Show Names", subtitlesShowNames ? 1 : 0);
            PlayerPrefs.SetInt("Subtitles Show Hidden", subtitlesShowHidden ? 1 : 0);
        }
    }

    /*!
     *  A method that reverts all changes made to the settings UI values.
     */
    public void RevertAllChanges()
    {
        sensitivitySlider.value = mouseSensitivity;

        autoSaveToggle.isOn = autoSave;
    }
}
