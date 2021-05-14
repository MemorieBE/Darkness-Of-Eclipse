using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the player camera and mouse sensitivity.
 *
 *  [Reusable Script]
 */
public class FirstPersonCameraController : MonoBehaviour
{
    public static float mouseSensitivity = 100f; //!< The mouse sensitivity variable.

    [Header("Player Head")]
    public Transform playerHead; //!< The player head game object.

    private float xRotation; //!< A float used to clamp the player head x rotation.

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime * 3f / StaticVars.timeScaleMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime * 3f / StaticVars.timeScaleMultiplier;

        if (!StaticVars.allowPlayerInputs)
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
