using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls the sensitivity settings slider.
 *
 *  [Vital Script]
 */
public class SettingsValues : MonoBehaviour
{
    public static float mouseSensitivity = 100f; //!< The mouse sensitivity setting.
    public static bool autoSave = true; //!< The autosave setting.

    [Header("Sensitivity")]
    [SerializeField] private Text sliderValueText; //!< The slider value text.
    [SerializeField] private Slider sensitivitySlider; //!< The sensitivity slider.

    void Awake()
    {
        {
            if (!PlayerPrefs.HasKey("Sensitivity"))
            {
                PlayerPrefs.SetFloat("Sensitivity", 100f);
            }

            mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");

            sensitivitySlider.value = mouseSensitivity;
        }

        {
            if (!PlayerPrefs.HasKey("Auto Save"))
            {
                PlayerPrefs.SetInt("Auto Save", 1);
            }

            autoSave = PlayerPrefs.GetInt("Auto Save") == 1;
        }
    }

    /*!
     *  A method that updates the sensitivity setting.
     */
    public void SensitivityChange()
    {
        mouseSensitivity = sensitivitySlider.value;
        sliderValueText.text = mouseSensitivity.ToString();

        PlayerPrefs.SetFloat("Sensitivity", mouseSensitivity);
    }

    /*!
     *  A method that updates the auto save setting.
     */
    public void AutoSaveChange()
    {
        PlayerPrefs.SetInt("Auto Save", autoSave ? 1 : 0);
    }
}
