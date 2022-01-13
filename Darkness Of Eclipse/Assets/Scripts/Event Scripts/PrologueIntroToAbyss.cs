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
    [SerializeField] private DoorScript doorScript1; //!< The script that controls the first door.
    [SerializeField] private DoorScript doorScript2; //!< The script that controls the second door.
    [SerializeField] private SmartLineController lineController; //!< The smart line controller script.
    [SerializeField] private GameObject smallAbyss; //!< The small abyss game object.

    [Header("Inputs")]
    [SerializeField] private float eventDuration = 10f; //!< The amount of time in seconds that the event will last for.

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        StartCoroutine(StartEvent());

        doorScript1.open = false;
        doorScript1.locked = true;
        doorScript1.UpdateDoorAnimation();

        doorScript2.open = false;
        doorScript2.locked = true;
        doorScript2.UpdateDoorAnimation();

        lineController.ActivateLine(0);
        lineController.ActivateLine(1);
        lineController.ActivateLine(2);
        lineController.ActivateLine(3);
    }

    /*!
     *  A coroutine that activates the event.
     */
    IEnumerator StartEvent()
    {
        yield return new WaitForSeconds(eventDuration);

        doorScript1.open = true;
        doorScript1.locked = false;
        doorScript1.UpdateDoorAnimation();

        doorScript2.open = true;
        doorScript2.locked = false;
        doorScript2.UpdateDoorAnimation();

        smallAbyss.SetActive(true);
    }
}
