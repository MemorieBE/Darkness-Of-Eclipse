using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a plank breaking when colliding with an axe head.
 *
 *  [Mechanic Script]
 */
public class PlankScript : MonoBehaviour
{
    [Header("Assets")]
    public Collider axeCollider; //!< The axe head collider.
    public GameObject[] brokenPart = new GameObject[2]; //!< The broken plank part game objects.
    public AudioSource breakSound; //!< The audio source of the plank breaking audio.

    [Header("Inputs")]
    public bool isBroken; //!< A boolean that controls whether or not the plank is broken.
    public float popDistance = 100f; //!< The upwards force applied to the plank when broken.

    [Header("Optional")]
    public DoorScript[] doorScript; //!< The script that controls the door that the plank is blocking.
    public GameObject[] plankGroup; //!< The other planks that are blocking the same door.
    private bool plankGroupActive; //!< A boolean that determines whether or the other planks withing the group are active.

    void OnTriggerEnter(Collider collisionData)
    {
        if (collisionData == axeCollider)
        {
            isBroken = true;
        }
    }

    void Update()
    {
        if (isBroken)
        {
            for (int i = 0; i < brokenPart.Length; i++)
            {
                brokenPart[i].SetActive(true);
                brokenPart[i].GetComponent<Rigidbody>().isKinematic = false;
                brokenPart[i].GetComponent<Rigidbody>().AddForce(Vector3.up * popDistance);
            }

            for (int i = 0; i < plankGroup.Length; i++)
            {
                plankGroupActive = plankGroup[i].activeInHierarchy;

                if (plankGroup[i].activeInHierarchy) break;
            }

            for (int i = 0; i < doorScript.Length; i++) doorScript[i].locked = plankGroupActive;

            breakSound.Play();

            gameObject.SetActive(false);
        }
    }
}