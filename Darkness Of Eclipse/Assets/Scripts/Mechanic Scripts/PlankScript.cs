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
    public GameObject[] brokenParts = new GameObject[2]; //!< The broken plank part game objects.
    public AudioSource breakSound; //!< The audio source of the plank breaking audio.

    [Header("Inputs")]
    public bool isBroken; //!< A boolean that controls whether or not the plank is broken.
    [HideInInspector] public bool skipBreak; //!< A boolean that determines whether or not the breaking segment will be skipped.
    public float popDistance = 100f; //!< The upwards force applied to the plank when broken.

    [Header("Optional")]
    public DoorScript[] doorScripts; //!< The script that controls the door that the plank is blocking.
    public GameObject[] plankGroups; //!< The other planks that are blocking the same door.
    private bool plankGroupActive; //!< A boolean that determines whether or the other planks within the group are active.

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
            if (skipBreak)
            {
                gameObject.SetActive(false);

                return;
            }

            for (int i = 0; i < brokenParts.Length; i++)
            {
                brokenParts[i].SetActive(true);
                brokenParts[i].GetComponent<Rigidbody>().isKinematic = false;
                brokenParts[i].GetComponent<Rigidbody>().AddForce(Vector3.up * popDistance);
            }

            for (int i = 0; i < plankGroups.Length; i++)
            {
                plankGroupActive = plankGroups[i].activeInHierarchy;

                if (plankGroups[i].activeInHierarchy) break;
            }

            for (int i = 0; i < doorScripts.Length; i++) doorScripts[i].locked = plankGroupActive;

            breakSound.Play();

            gameObject.SetActive(false);
        }
    }
}