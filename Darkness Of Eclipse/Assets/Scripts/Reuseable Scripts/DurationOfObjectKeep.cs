using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how long an object will be active in the hierarchy for.
 *
 *  [Reusable Script]
 */
public class DurationOfObjectKeep : MonoBehaviour
{
    [Header("Lifespan")]
    public float duration = 1f; //!< The amount of time in seconds that the object will be active for.

    private float timer = 0f; //!< A timer used to calculate how long the object has been active for.

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        if (timer >= duration)
        {
            gameObject.SetActive(false);
            timer = 0f;
        }
        else timer += Time.deltaTime;
    }
}
