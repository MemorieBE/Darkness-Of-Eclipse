using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEquippable : MonoBehaviour
{
    public Transform playerTransform;

    public float dropDistance = 1f;

    public GameObject[] droppedEquippable;

    public void EquippableDrop(int equippableID)
    {
        GameObject instantiatedEquippable = Instantiate(droppedEquippable[equippableID]);

        instantiatedEquippable.transform.position = playerTransform.TransformPoint(Vector3.forward * dropDistance);
        instantiatedEquippable.transform.rotation = Quaternion.Euler(0f, playerTransform.transform.rotation.eulerAngles.y, 0f);

        instantiatedEquippable.SetActive(true);
    }
}
