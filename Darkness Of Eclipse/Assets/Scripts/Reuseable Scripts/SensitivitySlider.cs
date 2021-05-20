using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that controls the sensitivity settings slider.
 *
 *  [Reusable Script]
 */
public class SensitivitySlider : MonoBehaviour
{
    public static float mouseSensitivity = 100f; //!< The mouse sensitivity variable.

    public Text sliderValueText; //!< The slider value text.
    public Slider sensitivitySlider; //!< The sensitivity slider.

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Sensitivity"))
        {
            PlayerPrefs.SetFloat("Sensitivity", 100f);
        }

        mouseSensitivity = PlayerPrefs.GetFloat("Sensitivity");

        sensitivitySlider.value = mouseSensitivity;
    }

    /*!
     *  A method that updates the sensitivity.
     */
    public void SensitivityChange()
    {
        mouseSensitivity = sensitivitySlider.value;
        sliderValueText.text = mouseSensitivity.ToString();

        PlayerPrefs.SetFloat("Sensitivity", mouseSensitivity);
    }
}
