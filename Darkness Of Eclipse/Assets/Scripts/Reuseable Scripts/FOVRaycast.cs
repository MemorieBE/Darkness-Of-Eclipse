using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*! \brief A script that controls how an equippable object sways.
 *
 *  [Reusable Script]
 */
public class FOVRaycast : MonoBehaviour
{
    [Header("Assets")]
    public GameObject playerHead; //!< The player head game object.
    public GameObject target; //!< The target game object.
    public int[] raycastIgnoreLayers; //!< The layers that the raycast will ignore.

    [Header("Inputs")]
    public Vector3 targetOffset; //!< The position offset for the target.
    public float maxRaycastDistance = 1000f; //!< The maximum distance of the raycast.

    [Header("Angle")]
    public float fOVOffset = 0f; //!< The number added to the camera field of view to calculate the raycast field of view.
    [HideInInspector] public float totalFieldOfView; //!< The raycast total field of view.

    [Header("Debug")]
    public bool debugRaycast = true; //!< A boolean that controls whether or not the raycast debug is visable in the editor at runtime.

    [Header("Read Only")]
    public bool targetInSight; //!< A boolean that determines whether or not the target is in the player's line of sight.
    public float fOVAngle; //!< The current angle from the player head forward direction to the player head target direction.

    private Camera playerHeadCamera; //!< The player head camera.

    void Start()
    {
        playerHeadCamera = playerHead.GetComponent<Camera>();
    }

    void Update()
    {
        // Creates a Vector3 of the player's head rotations without the z axis.
        Vector3 normalizedHead = playerHead.transform.rotation.eulerAngles;
        normalizedHead.z = 0;

        gameObject.transform.LookAt(target.transform.position + targetOffset);

        fOVAngle = Quaternion.Angle(gameObject.transform.rotation, Quaternion.Euler(normalizedHead));

        Ray raycast = new Ray(gameObject.transform.position, target.transform.position + targetOffset - gameObject.transform.position);
        RaycastHit hit;

        int layerMask = 1 << raycastIgnoreLayers[0];
        foreach (var unverIgnoredLayer in raycastIgnoreLayers.Skip(1))
        {
            layerMask = (layerMask | 1 << unverIgnoredLayer);
        }
        layerMask = ~layerMask;

        totalFieldOfView = playerHeadCamera.fieldOfView + fOVOffset;

        if ((Physics.Raycast(raycast, out hit, Vector3.Distance(gameObject.transform.position, target.transform.position + targetOffset), layerMask) || //< If the raycast hits a collider.
            (Vector3.Distance(gameObject.transform.position, target.transform.position) > maxRaycastDistance) || //< If the target is further than the maximum distance.
            (totalFieldOfView / 2 < fOVAngle))) //< If field of view radius is smaller than the angle.
        {
            // Raycast is not hitting target.

            targetInSight = false;

            if (debugRaycast) Debug.DrawLine(gameObject.transform.position, target.transform.position + targetOffset, Color.red);
        }
        else
        {
            // Raycast is hitting target.

            targetInSight = true;

            if (debugRaycast) Debug.DrawLine(gameObject.transform.position, target.transform.position + targetOffset, Color.green);
        }
    }
}
