using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the player camera and mouse sensitivity.
 *
 *  [Vital Script]
 */
public class FirstPersonCameraController : MonoBehaviour
{
    [Header("Player Head")]
    public Transform playerHead; //!< The player head game object.

    private float xRotation; //!< A float used to clamp the player head x rotation.

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * SettingsValues.mouseSensitivity * Time.fixedDeltaTime * 3f / GameRules.timeScaleMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * SettingsValues.mouseSensitivity * Time.fixedDeltaTime * 3f / GameRules.timeScaleMultiplier;

        if (!PlayerControllerCC.allowPlayerInputs)
        {
            mouseX = 0f;
            mouseY = 0f;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        gameObject.transform.Rotate(Vector3.up * mouseX);
    }
}
