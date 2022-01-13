using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how long an object will be active in the hierarchy for.
 *
 *  Independent
 */
public class DurationOfObjectKeep : MonoBehaviour
{
    [Header("Lifespan")]
    public float duration = 1f; //!< The amount of time in seconds that the object will be active for.

    void OnEnable()
    {
        StartCoroutine(DisableObjectAfterTime());
    }

    private IEnumerator DisableObjectAfterTime()
    {
        yield return new WaitForSeconds(duration);

        gameObject.SetActive(false);
    }
}
