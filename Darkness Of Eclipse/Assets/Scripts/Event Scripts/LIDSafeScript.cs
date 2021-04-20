using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a plank breaking when colliding with an axe head.
 *
 *  [Event Script]
 */
public class LIDSafeScript : MonoBehaviour
{
    [Header("Assets")]
    public Collider hammerHead; //!< The hammer head collider.
    public GameObject safe; //!< The safe game object.
    public GameObject[] safeState; //!< The safe state game objects.
    public GameObject key; //!< The key inside the safe game object.

    [Header("Audio")]
    public AudioSource damageSound; //!< The audio source for the safe damage audio.
    public AudioSource breakingSound; //!< The audio source for the safe breaking audio.

    [Header("Particle")]
    public Transform particleParent; //!< The particle parent transform.
    public ParticleSystem sparkParticle; //!< The spark particle system.

    [Header("Inputs")]
    public float damageCooldown = 1f; //!< The amount of time in seconds the damage cooldown for the safe is.
    private float safeStateTimer; //!< A timer used for the damage cooldown.

    [HideInInspector] public int safeCurrentState; //!< The current state of the safe.

    void Start()
    {
        safeCurrentState = 0;
        safeStateTimer = damageCooldown;
    }

    void Update()
    {
        if (safeStateTimer < damageCooldown) safeStateTimer += Time.deltaTime;
        else safe.GetComponent<Animator>().SetBool("Stumble", false);
    }

    void OnTriggerEnter(Collider collisionData)
    {
        if (collisionData == hammerHead && safeCurrentState < safeState.Length - 1 && safeStateTimer >= damageCooldown)
        {
            safeCurrentState ++;

            safe.GetComponent<Animator>().SetBool("Stumble", true);

            particleParent.position = collisionData.ClosestPoint(gameObject.transform.position);
            particleParent.rotation = Quaternion.LookRotation((collisionData.ClosestPoint(gameObject.transform.position) - safe.transform.position).normalized, Vector3.up);
            sparkParticle.Play();

            for (int i = 0; i < safeState.Length; i++)
            {
                if (i == safeCurrentState) safeState[i].SetActive(true);
                else safeState[i].SetActive(false);
            }

            if (safeCurrentState >= safeState.Length - 1)
            {
                breakingSound.Play();
                key.SetActive(true);

                safe.GetComponent<Collider>().enabled = false;
                safe.GetComponent<Animator>().SetBool("DoorOpen", true);
            }
            else damageSound.Play();

            safeStateTimer = 0f;
        }
    }
}
