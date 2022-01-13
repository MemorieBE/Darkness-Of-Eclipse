using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the game object moves towards a transform.
 *
 *  Independent
 */
public class GoToTransform : MonoBehaviour
{
    [Header("Target")]
    public Transform target; //!< The target transform to move towards.

    [Header("Inputs")]
    public Vector3 targetOffset; //!< The position offset to the target transform.
    public float speed = 2f; //!< The speed to move at.

    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position + targetOffset, speed * Time.fixedDeltaTime);
    }
}
