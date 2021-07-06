using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the current equippable.
 *
 *  [Reusable Script]
 */
public class CurrentEquippable : MonoBehaviour
{
    [Header("Equippables")]
    public static int currentEquippable = 0; //!< The current equippable ID.
    private int equippableUpdate = 0; //!< The integer of the current equippable in the last frame.
    public GameObject[] equippable; //!< The equippable game objects.

    private float timer; //!< The animation pause timer.

    void Awake()
    {
        UpdateEquippable();
    }

    void Update()
    {
        if (equippableUpdate != currentEquippable)
        {
            UpdateEquippable();
        }

        if (currentEquippable > 0)
        {
            if (equippable[currentEquippable].GetComponent<EquippableController>().isActive)
            {
                equippable[0].GetComponent<FlashlightController>().isRight = false;

                timer = 0f;
            }
            else
            {
                if (timer >= equippable[currentEquippable].GetComponent<EquippableController>().animationStartPause) equippable[0].GetComponent<FlashlightController>().isRight = true;

                timer += Time.deltaTime;
            }
        }
        else
        {
            equippable[0].GetComponent<FlashlightController>().isRight = true;
        }
    }

    /*!
     *  A method that updates the current equippable.
     */
    public void UpdateEquippable()
    {
        if (currentEquippable >= equippable.Length)
        {
            currentEquippable = equippable.Length - 1;
        }

        EquippableController newEquippable = equippable[currentEquippable].GetComponent<EquippableController>();
        EquippableController oldEquippable = equippable[equippableUpdate].GetComponent<EquippableController>();

        if (equippableUpdate > 0 && currentEquippable > 0)
        newEquippable.isActive = oldEquippable.isActive;

        for (int i = 1; i < equippable.Length; i++)
        {
            if (i == currentEquippable) equippable[i].SetActive(true);
            else
            {
                equippable[i].GetComponent<EquippableController>().isActive = false;
                equippable[i].SetActive(false);
            }
        }

        equippableUpdate = currentEquippable;
    }
}
