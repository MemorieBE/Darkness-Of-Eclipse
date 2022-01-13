using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how long an object will exist before being destroyed.
 *
 *  Independent
 */
public class DurationOfObjectDestroy : MonoBehaviour
{
    [Header("Lifespan")]
    public float duration = 1f; //!< The amount of time in seconds that the object will be active for.

    void OnEnable()
    {
        StartCoroutine(DestroyObjectAfterTime());
    }

    private IEnumerator DestroyObjectAfterTime()
    {
        yield return new WaitForSeconds(duration);

        Destroy(gameObject);
    }
}
