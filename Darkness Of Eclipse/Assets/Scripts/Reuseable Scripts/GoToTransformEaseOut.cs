using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a game object to move towards a transform with outward easing.
 *
 *  Independent
 */
public class GoToTransformEaseOut : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target; //!< The target transform to move towards.

    [Header("Inputs")]
    [SerializeField] private Vector3 targetOffset; //!< The position offset to the target transform.
    [SerializeField] private float speed = 2f; //!< The speed to move at.
    [SerializeField] private float easeOutDistance = 2f; //!< The distance from the target where the game object will be slowed.

    void FixedUpdate()
    {
        float distance = Vector3.Distance(gameObject.transform.position, target.position + targetOffset);
        float speedOutcome;

        if (distance < easeOutDistance) { speedOutcome = speed * (distance / easeOutDistance); }
        else { speedOutcome = speed; }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position + targetOffset, speedOutcome * Time.fixedDeltaTime);
    }
}
