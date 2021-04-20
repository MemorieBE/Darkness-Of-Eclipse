using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls how long an object will exist before being destroyed.
 *
 *  [Reusable Script]
 */
public class DurationOfObjectDestroy : MonoBehaviour
{
    [Header("Lifespan")]
    public float duration = 1f; //!< The amount of time in seconds that the object will exist for.

    private float timer = 0f; //!< A timer used to calculate how long the object has existed for.

    void Start()
    {
        timer = 0f;
    }

    void Update()
    {
        if (timer >= duration)
        {
            Destroy(gameObject);
            timer = 0f;
        }
        else timer += Time.deltaTime;
    }
}
