using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the event that occurs before entering the abyss in the prologue.
 *
 *  [Event Script]
 */
public class PrologueIntroToAbyss : MonoBehaviour
{
    [Header("Assets")]
    public TriggerDetectionEnter triggerScript; //!< The script that controls the trigger.
    public DoorScript doorScript1; //!< The script that controls the first door.
    public DoorScript doorScript2; //!< The script that controls the second door.
    public GameObject smallAbyss; //!< The small abyss game object.

    [Header("Inputs")]
    public float eventDuration = 10f; //!< The amount of time in seconds that the event will last for.
    private float timer = 0f; //!< The event timer.
    private bool eventStarted = false; //!< A boolean that determines whether or not the event has started.

    void Update()
    {
        if (triggerScript.activated)
        {
            eventStarted = true;

            doorScript1.open = false;
            doorScript1.locked = true;
            doorScript2.open = false;
            doorScript2.locked = true;

            timer = 0f;
        }

        if (eventStarted)
        {
            if (timer >= eventDuration)
            {
                eventStarted = false;

                doorScript1.open = true;
                doorScript1.locked = false;
                doorScript2.open = true;
                doorScript2.locked = false;

                smallAbyss.SetActive(true);
            }

            timer += Time.deltaTime;
        }
    }
}
