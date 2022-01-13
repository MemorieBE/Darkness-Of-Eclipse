using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*! \brief A script that controls how an equippable object sways.
 *
 *  Independent
 */
public class FOVRaycast : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private Transform target; //!< The target transform.
    [SerializeField] private Transform subject; //!< The subject transform.
    [SerializeField] private int[] raycastIgnoreLayers; //!< The layers that the raycast will ignore.

    [Header("Inputs")]
    public float maxRaycastDistance = 1000f; //!< The maximum distance of the raycast.
    [SerializeField] private bool invert = false; //!< A boolen that controls whether or not the raycast is inverted.

    [Header("Angle")]
    public float fieldOfView = 60f; //!< The raycast total field of view.

    [Header("Debug")]
    [SerializeField] private bool debugRaycast = true; //!< A boolean that controls whether or not the raycast debug is visable in the editor at runtime.

    [Header("Read Only")]
    public bool targetInSight; //!< A boolean that determines whether or not the target is in the player's line of sight.
    public float fOVAngle; //!< The current angle from the player head forward direction to the player head target direction.

    void Update()
    {
        gameObject.transform.LookAt(target.position);

        if (!invert)
        {
            Vector3 normalizedSubject = subject.rotation.eulerAngles;
            normalizedSubject.z = 0;

            fOVAngle = Quaternion.Angle(gameObject.transform.rotation, Quaternion.Euler(normalizedSubject));
        }
        else
        {
            Vector3 normalizedTarget = target.rotation.eulerAngles;
            normalizedTarget.z = 0;

            fOVAngle = Quaternion.Angle((gameObject.transform.rotation * Quaternion.AngleAxis(180f, Vector3.up)), Quaternion.Euler(normalizedTarget));
        }

        Ray raycast;
        if (!invert) { raycast = new Ray(gameObject.transform.position, target.position - gameObject.transform.position); }
        else { raycast = new Ray(target.position, gameObject.transform.position - target.position); }
        RaycastHit hit;

        int layerMask = 1 << raycastIgnoreLayers[0];
        foreach (var unverIgnoredLayer in raycastIgnoreLayers.Skip(1))
        {
            layerMask = (layerMask | 1 << unverIgnoredLayer);
        }
        layerMask = ~layerMask;

        if ((Physics.Raycast(raycast, out hit, Vector3.Distance(gameObject.transform.position, target.position), layerMask) || //< If the raycast hits a collider.
            (Vector3.Distance(gameObject.transform.position, target.position) > maxRaycastDistance) || //< If the target is further than the maximum distance.
            (fieldOfView / 2 < fOVAngle))) //< If field of view radius is smaller than the angle.
        {
            // Raycast is not hitting target.

            targetInSight = false;

            if (debugRaycast) Debug.DrawLine(gameObject.transform.position, target.position, Color.red);
        }
        else
        {
            // Raycast is hitting target.

            targetInSight = true;

            if (debugRaycast) Debug.DrawLine(gameObject.transform.position, target.position, Color.green);
        }
    }
}
