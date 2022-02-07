using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InvokeFromFunction))]

/*! \brief A script that changes a camera FOV to transition.
 *
 *  References: InvokeFromFunction.
 */
public class FOVTransition : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera targetCamera; //!< The target camera.

    [Header("Inputs")]
    [SerializeField] private float targetFOV; //!< A value that controls the target field of view.
    [SerializeField] private float timeIn; //!< A value that controls how long it will take to transition in.
    [SerializeField] private float timeOut; //!< A value that controls how long it will take to transition out.
    [SerializeField] private float exponentialMultiplier = 1f; //!< A value that controls how exponential the field of view transition is. (1 = linear)

    private bool transitioning = false; //!< A boolean that determines whether or not the transition coroutine is active.

    private float originalFOV; //!< The original field of view.

    /*!
     *  A method that starts the FOV transition.
     */
    public void StartTransition()
    {
        if (transitioning) { return; }

        originalFOV = targetCamera.fieldOfView;

        StartCoroutine(Transition());
    }

    /*!
     *  A coroutine that transitions using the camera's field of view.
     */
    private IEnumerator Transition()
    {
        transitioning = true;

        for (float transInTime = 0f; transInTime < timeIn; transInTime += Time.deltaTime)
        {
            if (transInTime > timeIn) { transInTime = timeIn; }

            targetCamera.fieldOfView = originalFOV - (originalFOV - targetFOV) * (Mathf.Pow(transInTime, exponentialMultiplier) / Mathf.Pow(timeIn, exponentialMultiplier));
            yield return null;
        }

        gameObject.GetComponent<InvokeFromFunction>().Invoke();

        for (float transOutTime = 0f; transOutTime < timeOut; transOutTime += Time.deltaTime)
        {
            if (transOutTime > timeOut) { transOutTime = timeOut; }

            targetCamera.fieldOfView = originalFOV - (originalFOV - targetFOV) * (Mathf.Pow(timeOut - transOutTime, exponentialMultiplier) / Mathf.Pow(timeOut, exponentialMultiplier));

            yield return null;
        }

        transitioning = false;
    }
}
