using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class LightColourByDistanceLocal : MonoBehaviour
{
    [SerializeField] private Collider mainCollider;
    [SerializeField] private LightColourByDistanceController controller;

    public Color colour;
    public float minRadius = 0;
    public float maxRadius = 1;

    void OnTriggerEnter(Collider collider)
    {
        if (collider == mainCollider)
        {
            controller.colourChangers.Add(this);
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider == mainCollider)
        {
            controller.colourChangers.Remove(this);
        }
    }

    void OnValidate()
    {
        SphereCollider collider = gameObject.GetComponent<SphereCollider>();

        collider.isTrigger = true;
        collider.radius = maxRadius / Mathf.Max(gameObject.transform.lossyScale.x, gameObject.transform.lossyScale.y, gameObject.transform.lossyScale.z);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = colour;

        Gizmos.DrawWireSphere(gameObject.transform.position, minRadius);
        Gizmos.DrawWireSphere(gameObject.transform.position, maxRadius);
    }
}
