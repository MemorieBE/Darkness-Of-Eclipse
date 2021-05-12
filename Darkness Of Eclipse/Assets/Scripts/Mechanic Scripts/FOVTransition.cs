using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script changes a camera FOV to transition.
 *
 *  [Mechanic Script]
 */
public class FOVTransition : MonoBehaviour
{
    [Header("Camera")]
    public Camera targetCamera;

    [Header("Inputs")]
    public float targetFOV;
    private float originalFOV;
    public float timeIn;
    public float timeOut;
    private float timer;
    private bool hasTeleported;
    public float exponentialMultiplier = 1f; //!< A value that controls how exponential the field of view transition is. (1 = linear)

    void Start()
    {
        originalFOV = targetCamera.fieldOfView;
    }

    void Update()
    {
        if (timer < timeIn)
        {
            targetCamera.fieldOfView = originalFOV - (originalFOV - targetFOV) * (Mathf.Pow(timer, exponentialMultiplier) / Mathf.Pow(timeIn, exponentialMultiplier));
        }

        if (timer >= timeIn && !hasTeleported)
        {
            if (gameObject.GetComponent<TeleportAreaMethodCC>() != null)
            {
                gameObject.GetComponent<TeleportAreaMethodCC>().Teleport();
            }
            else if (gameObject.GetComponent<TeleportBasicMethodCC>() != null)
            {
                gameObject.GetComponent<TeleportBasicMethodCC>().Teleport();
            }

            hasTeleported = true;
        }

        if (timer > timeIn)
        {
            targetCamera.fieldOfView = originalFOV - (originalFOV - targetFOV) * (Mathf.Pow(timeOut - (timer - timeIn), exponentialMultiplier) / Mathf.Pow(timeOut, exponentialMultiplier));
        }

        timer += Time.deltaTime;

        if (timer >= timeIn + timeOut)
        {
            timer = 0f;
            gameObject.SetActive(false);
        }
    }
}
