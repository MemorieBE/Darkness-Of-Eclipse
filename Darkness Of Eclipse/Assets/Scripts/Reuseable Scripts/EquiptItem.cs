using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that changes the current equippable when interacting with the game object.
 *
 *  [Reusable Script]
 */
public class EquiptItem : MonoBehaviour
{
    public DropEquippable dropScript; //!< The equippable drop script.

    public int equippableID; //!< The equippableID.

    public float resetYPoint = 0f; //!< The Y position that will reset the equippable.

    void FixedUpdate()
    {
        if (gameObject.transform.position.y <= resetYPoint && !gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            dropScript.EquippableDrop(equippableID);

            Destroy(gameObject);
        }
    }

    /*!
     *  A method that is triggered on activation.
     */
    public void Activated()
    {
        if (CurrentEquippable.currentEquippable > 0) dropScript.EquippableDrop(CurrentEquippable.currentEquippable);

        CurrentEquippable.currentEquippable = equippableID;

        if (gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        if (!gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            if (SceneSavePoint.equippables == null)
                SceneSavePoint.equippables = new List<EquiptItem>();

            if (gameObject.GetComponent<Rigidbody>().isKinematic) SceneSavePoint.equippables.Add(this);
            Debug.Log("Add: " + gameObject.name);
        }
    }

    void OnDisable()
    {
        if (!gameObject.GetComponent<Rigidbody>().isKinematic)
        {
            if (SceneSavePoint.equippables == null)
                SceneSavePoint.equippables = new List<EquiptItem>();

            SceneSavePoint.equippables.Remove(this);
            Debug.Log("Remove: " + gameObject.name);
        }
    }
}
