using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SinicWispController : MonoBehaviour
{
    public static int wispCount = 0;

    [Header("Wisp")]
    public bool wispsActive = true;
    [SerializeField] private GameObject wispProjectile;
    [SerializeField] private Vector3 wispSpawnOffset;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("NavMesh")]
    [SerializeField] private Transform[] possibleDestinations;

    public static string keyBind = "y";

    void Update()
    {
        if (Input.GetKeyDown(keyBind) && wispsActive && wispCount > 0)
        {
            UseWisp();
        }
    }

    public void UpdateWispCount(int amount)
    {
        wispCount += amount;
    }

    private void UseWisp()
    {
        UpdateWispCount(-1);

        GameObject newWisp;

        newWisp = Instantiate(wispProjectile, player.position + wispSpawnOffset, Quaternion.Euler(Vector3.up * player.rotation.y));

        Vector3 destination = Vector3.zero;

        bool activeDestination = false;
        int firstActiveDestination = 0;

        for (int i = 0; i < possibleDestinations.Length; i++)
        {
            if (possibleDestinations[i].gameObject.activeSelf || possibleDestinations[i] == null)
            {
                activeDestination = true;
                firstActiveDestination = i;
                break;
            }
        }

        if (possibleDestinations.Length == 1 && activeDestination)
        {
            destination = possibleDestinations[0].position;
        }
        else if (possibleDestinations.Length > 1 && activeDestination)
        {
            int shortestDestination = firstActiveDestination;

            for (int i = firstActiveDestination + 1; i < possibleDestinations.Length; i++)
            {
                if ((possibleDestinations[i].gameObject.activeSelf || possibleDestinations[i] == null) && Vector3.Distance(possibleDestinations[i].position, player.position + wispSpawnOffset) < Vector3.Distance(possibleDestinations[shortestDestination].position, player.position + wispSpawnOffset))
                {
                    shortestDestination = i;
                }
            }

            destination = possibleDestinations[shortestDestination].position;
        }
        else
        {
            destination = player.position + wispSpawnOffset;
        }

        newWisp.GetComponent<NavMeshAgent>().Warp(player.position + wispSpawnOffset);
        newWisp.GetComponent<NavMeshAgent>().SetDestination(destination);
    }
}
