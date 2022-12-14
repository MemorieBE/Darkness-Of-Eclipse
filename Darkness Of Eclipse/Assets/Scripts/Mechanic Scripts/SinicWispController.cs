﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SinicWispController : MonoBehaviour
{
    public static int wispCount = 10;

    [Header("Wisp")]
    public bool wispsActive = true;
    [SerializeField] private GameObject wispProjectile;
    [SerializeField] private Vector3 wispSpawnOffset;
    [SerializeField] private Text wispCountText;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Result")]
    public ResultType result;
    public Transform[] possibleDestinations;
    public string promptString;
    [SerializeField] private Text promptAsset;

    [Header("Input")]
    [SerializeField] private InputActionReference wispAction;

    [Header("Delay")]
    [SerializeField] private float delayTime = 2.5f;
    private bool delaying = false;

    public enum ResultType
    {
        destination,
        prompt
    }

    void Start()
    {
        wispCountText.text = wispCount.ToString();
    }

    void OnEnable()
    {
        wispAction.action.performed += UseWisp;
        wispAction.action.Enable();
    }

    void OnDisable()
    {
        wispAction.action.performed -= UseWisp;
        wispAction.action.Disable();
    }

    public void UpdateWispCount(int amount)
    {
        wispCount += amount;

        wispCountText.text = wispCount.ToString();
    }

    private void UseWisp(InputAction.CallbackContext ctx)
    {
        if (delaying || !wispsActive || wispCount <= 0 || GameRules.cancelInputOverride > 0) { return; }

        Delay(delayTime);
        
        UpdateWispCount(-1);

        switch (result)
        {
            case ResultType.destination:
                DestinationResult();
                break;
            case ResultType.prompt:
                PromptResult();
                break;
        }
    }

    private void DestinationResult()
    {
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

        NavMeshAgent nma = newWisp.GetComponentInChildren<NavMeshAgent>();

        nma.Warp(player.position + wispSpawnOffset);
        nma.SetDestination(destination);
    }

    private void PromptResult()
    {
        promptAsset.text = promptString;
        promptAsset.gameObject.SetActive(true);
    }

    private IEnumerator Delay(float time)
    {
        delaying = true;
        yield return new WaitForSeconds(time);
        delaying = false;
    }
}
