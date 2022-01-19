using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ADED_Interactable))]

public class SinicWispCollect : MonoBehaviour
{
    [SerializeField] private SinicWispController controller;

    public void Activated()
    {
        controller.UpdateWispCount(1);

        Destroy(gameObject);
    }
}
