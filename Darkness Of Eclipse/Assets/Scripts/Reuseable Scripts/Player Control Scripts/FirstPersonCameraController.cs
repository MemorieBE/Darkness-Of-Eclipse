using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*! \brief A script that controls the player camera and mouse sensitivity.
 *
 *  References: GameRules, SettingsValues.
 */
public class FirstPersonCameraController : MonoBehaviour
{
    [Header("Player Head")]
    [SerializeField] private Transform playerHead; //!< The player head game object.

    [Header("Action")]
    [SerializeField] private InputActionReference lookAction; //!< The look action.

    private float xRotation; //!< A float used to clamp the player head x rotation.

    private Vector2 look; //!< The look delta value.

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        lookAction.action.Enable();
    }

    void OnDisable()
    {
        lookAction.action.Disable();
    }

    void Update()
    {
        look = lookAction.action.ReadValue<Vector2>();

        Vector2 mouse = look * SettingsValues.mouseSensitivity * Time.deltaTime * 0.1f / GameRules.timeScaleMultiplier;

        look = Vector2.zero;

        if (!PlayerControllerCC.allowPlayerInputs) { mouse = Vector2.zero; }

        xRotation -= mouse.y;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        gameObject.transform.Rotate(Vector3.up * mouse.x);
    }
}
