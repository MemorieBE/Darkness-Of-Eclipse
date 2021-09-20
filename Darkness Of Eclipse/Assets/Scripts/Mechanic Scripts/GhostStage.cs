using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*! \brief A script that controls how the ghost/unver goes through its stages when hunting the player.
 *
 *  [Mechanic Script]
 */
public class GhostStage : MonoBehaviour
{
    [Header("Unver Assets")]
    [SerializeField] private FOVRaycast raycastScript; //!< The player Unver raycast script.
    [SerializeField] private GameObject[] toggleAssets; //!< The Unver assets to set active and inactive with Unver deactivation state.
    [SerializeField] private Light ghostLight; //!< The Unver light.
    [SerializeField] private PlayerToGhostDetector detector; //!< The Unver player detection script.
    private Animator ghostAnimator; //!< The Unver animator.

    [Header("Player Head")]
    public Transform playerHead; //!< The player head transform.
    public Vector3 playerHeadOffset = new Vector3(0f, -2f, 0f); //!< The offset of the player's body from the player's head.

    [Header("Audio")]
    [SerializeField] private AudioSource stalkingAudio; //!< The stalking audio source.
    [SerializeField] private AudioSource chasingAudio; //!< The chasing audio source.

    [Header("Layer")]
    public int[] playerRaycastIgnoreLayers; //!< The layers that the player field of view raycast will ignore.
    public int floorLayer = 8; //!< The floor layer.
    private int playerRaycastLayerMask; //!< The player field of view raycast layer mask.

    [Header("Unver Inputs")]
    [Range (1, 10)] public int ghostStage = 1; //!< The Unver current stage. (From 1 to 10)
    public int ghostTier = 1; //!< The current tier/difficulty of the Unver stages.
    public int ghostMaxTier = 7; //!< The maximum Unver tier/difficulty.
    public int ghostSecondTierAnimation = 3; //!< The tier that needs to be hit in order to move on to the second animation state.
    public int ghostThirdTierAnimation = 5; //!< The tier that needs to be hit in order to move on to the third animation state.
    public int ghostStaredPushBack = 3; //!< The amount of stages the Unver would be pushed back when the player successfully stares at the Unver to deactivate it.
    public int ghostStageToAttemptLowest = 3; //!< The minimum possible stage the Unver needs to be at in order to start attempting to attack.
    public int ghostStageToAttemptHighest = 7; //!< The maximum possible stage the Unver needs to be at in order to start attempting to attack.
    public int attackAttemptProbability = 6; //!< The probability (1 / int) that the Unver will attempt an attack.
    public float ghostDistanceStageMin = 1f; //!< How close the Unver can be depending on the current stage.
    public float ghostDistanceStageMax = 80f; //!< How far away the Unver can be depending on the current stage.
    public float ghostChaseDistanceToDirect = 8f; //!< The flat distance the Unver needs to be from the player in order to fly directly towards the player in its chasing stage.
    public float ghostFloorClingeIntensity = 3f; //!< The intensity of the Unver clinging to the floor.
    public float ghostChaseDistance = 100f; //!< The distance away from the Unver will be from the player at the start of its chasing stage.
    public float ghostLightSetIntensity = 2f; //!< The light intensity based on the light game object and the Unver player distance.

    private float ghostSpawnFailSafe = 0f; //!< Used to prevent crashing when the Unver can't find an appropriete spawn position;

    private float ghostAngle; //!< A random angle used to spawn the Unver in a random location from the player.
    private float spawnAngle; //!< The angle converted from the ghost angle to find the angle from the player direction to the Unver spawn position.
    private float spawnDistance; //!< The distance from the player the Unver will spawn.
    private float ghostDistanceMin; //!< The minumum spawn distance value depending on the stage.
    private float ghostDistanceMax; //!< The maximum spawn distance value depending on the stage.

    [Header("Stages")]
    public bool ghostStagesActive; //!< A boolean that controls whether or not the Unver stages are active.
    public bool ghostStalkingStage = false; //!< A boolean that determines whether or not the Unver is stalking the player.
    public bool ghostChasingStage = false; //!< A boolean that determines whether or not the Unver is chasing the player.
    public bool ghostDeactivationStage = false; //!< A boolean that determines whether or not the Unver is temporarily deactivated.
    public bool playerStaring = false; //!< A boolean that determines whether or not the player is staring at the Unver.
    public bool playerPreStaring = false; //!< A boolean that determines whether or not the player is in the staring grace period.
    public bool ghostAttemptAttack = false; //!< A boolean that determines whether or not the Unver is attempting an attack.

    [Header("Base Tier Inputs")]
    public float ghostSpeed = 2f; //!< The speed of the Unver in its chasing stage.
    public float setStalkTime = 10f; //!< The duration of each stalking stage in seconds.
    public float setDeactivationTime = 15f; //!< The duration of the deactivation stage in seconds.
    public float setStareTimeMin = 3f; //!< The minimum duration for how long the player needs to stare at the Unver to deactivate it in seconds.
    public float setStareTimeMax = 8f; //!< The maximum duration for how long the player needs to stare at the Unver to deactivate it in seconds.
    public float setPreStareTime = 0.5f; //!< The duration of the grace period before the stare timer in seconds.
    public float setChaseTime = 40f; //!< The duration of the Unver chasing stage in seconds.

    private float stalkTimer = 0f; //!< A timer used for how long each stalking stage will last.
    private float stareTimer = 0f; //!< A timer used for how long the player needs to stare at the ghost before it deactivates.
    private float preStareTimer = 0f; //!< A timer used for how long the grace period before the stareTimer lasts.
    private float chaseTimer = 0f; //!< A timer used for how long the chasing stage will last.
    private float deactivationTimer = 0f; //!< A timer used for hoe long the ghost will be deactivated for.
    private float setStareTime; //!< A generated number between setStareTimeMin and setStareTimeMax.

    private bool getStareRandomNumberOnce = true; //!< A boolean that determines whether or not the set stare duration has been assigned a random number in the current instance.

    [Header("Highest Tier Inputs")]
    public float ghostSpeedTopTier = 6f; //!< The maximum amount for the Unver speed depending on the Unver tier.
    public float ghostStalkTimeTopTier = 5f; //!< The minimum amount for the Unver stalk time depending on the Unver tier.
    public float ghostDeactivationTimeTopTier = 5f; //!< The minimum amount for the Unver deactivation time depending on the Unver tier.
    public float ghostStareTimeMinTopTier = 5f; //!< The maximum amount for the player's set minimum stare time depending on the Unver tier.
    public float ghostStareTimeMaxTopTier = 15f; //Defines the max amount for the player's set maximum stare time depending on the ghost's tier.
    public float ghostChaseTimeTopTier = 60f; //Defines the max amount for the ghost's chase time depending on the ghost's tier.

    [Header("Raycast")]
    public float raycastRange = 1000f; //!< The duration of the maximum raycast length.

    [Header("Hard Mode")]
    public bool hardMode; //!< A boolean that controls whether or not hard mode is acticated.
    public float hardModeMultiplier = 2.5f; //!< The amount multiplied by the normal mode when in hard mode.
    private float normalMode = 1f; //!< The difficulty of the normal mode.
    private float modeMultiplier; //!< The number multiplied by the difficulty multiplier when in hard.

    [Header("Debugs")]
    [SerializeField] private bool ghostFloorRaycastDebug = false; //!< A boolean that visulizes the floor detecting raycast's maximum height for the ghost.
    [SerializeField] private bool playerFloorRaycastDebug = false; //!< A boolean that visulizes the floor detecting raycast's maximum height for the player.
    [SerializeField] private bool ghostSpawnDebug = false; //!< A boolean that activates the GhostSpawn() method.

    [Header("Console")]
    [SerializeField] private bool consoleGhostSpawn; //!< A boolean that shows the GhostSpawn() information.
    [SerializeField] private bool consoleStages; //!< A boolean that shows the current stage's information.

    [Header("Timer")]
    public bool pauseTimer = false; //!< A boolean that controls whether or not the current time is paused.
    public bool resetTimer = false; //!< A boolean that resets the current timer.
    [SerializeField] private string currentTimerName = "Timer"; //!< The name of the current timer used in whatever stage the Unver is in.
    [SerializeField] private float currentTimerValue = 0f; //!< The value of the current timer used in whatever stage the Unver is in.

    void Start()
    {
        ghostAnimator = gameObject.GetComponent<Animator>();

        stalkingAudio.volume = 0f;
        chasingAudio.volume = 0f;
    }

    void Update()
    {
        ghostStage = Mathf.Clamp(ghostStage, 1, 10); //< To clamp the ghostStage variable into a spectrum from 1 to 10.
        if (ghostStage < 1) ghostStage = 1;
        if (ghostStage > 10) ghostStage = 10;

        ghostTier = Mathf.Clamp(ghostTier, 1, ghostMaxTier); //< To clamp the ghostTier variable into a spectrum from 1 to the ghostMaxTier.
        if (ghostTier < 1) ghostTier = 1;
        if (ghostTier > ghostMaxTier) ghostTier = ghostMaxTier;

        if (ghostFloorRaycastDebug) //< To visualize the floor detecting raycast for the ghost.
        {
            Debug.DrawLine(gameObject.transform.position + new Vector3(0, -raycastRange, 0), gameObject.transform.position + new Vector3(0, raycastRange, 0), Color.magenta);
        }

        if (playerFloorRaycastDebug) //< To visualize the floor detecting raycast for the player.
        {
            Debug.DrawLine(playerHead.position + new Vector3(0, -raycastRange, 0), playerHead.position + new Vector3(0, raycastRange, 0), Color.magenta);
        }

        ghostLight.intensity = ghostLightSetIntensity * ((1 / ghostDistanceStageMax) * Vector3.Distance(gameObject.transform.position, playerHead.position)); //< Sets the light's intensity.

        if (hardMode) modeMultiplier = hardModeMultiplier * normalMode;
        else modeMultiplier = normalMode;

        if (!playerStaring && !playerPreStaring && !ghostStalkingStage && !ghostChasingStage && !ghostDeactivationStage) 
        {
            currentTimerName = "Timer";
            currentTimerValue = 0;
            resetTimer = false;
        }
        else if (playerStaring)
        {
            if (resetTimer) stareTimer = 0;
            currentTimerName = "Stare Timer";
            currentTimerValue = stareTimer;
            resetTimer = false;
        }
        else if (playerPreStaring)
        {
            if (resetTimer) preStareTimer = 0;
            currentTimerName = "Pre Stare Timer";
            currentTimerValue = preStareTimer;
            resetTimer = false;
        }
        else if (ghostStalkingStage)
        {
            if (resetTimer) stalkTimer = 0;
            currentTimerName = "Stalk Timer";
            currentTimerValue = stalkTimer;
            resetTimer = false;
        }
        else if (ghostChasingStage)
        {
            if (resetTimer) chaseTimer = 0;
            currentTimerName = "Chase Timer";
            currentTimerValue = chaseTimer;
            resetTimer = false;
        }
        else if (ghostDeactivationStage)
        {
            if (resetTimer) deactivationTimer = 0;
            currentTimerName = "Deactivation Timer";
            currentTimerValue = deactivationTimer;
            resetTimer = false;
        }
        else
        {
            resetTimer = false;
        }

        if (ghostChasingStage) ghostAnimator.SetInteger("Stage", 4);
        else
        {
            if (ghostTier >= 1 && ghostTier < ghostSecondTierAnimation)
            {
                ghostAnimator.SetInteger("Stage", 1);
            }
            if (ghostTier >= ghostSecondTierAnimation && ghostTier < ghostThirdTierAnimation)
            {
                ghostAnimator.SetInteger("Stage", 2);
            }
            if (ghostTier >= ghostThirdTierAnimation && ghostTier <= ghostMaxTier)
            {
                ghostAnimator.SetInteger("Stage", 3);
            }
        }

        if (ghostStagesActive)
        {
            if (detector.playerDetected) //< If the player touches the Unver, go into the deactivation stage and deactivate the ghost prefab.
            {
                ghostStalkingStage = true;
                ghostDeactivationStage = false;
                ghostChasingStage = false;
                playerPreStaring = false;
                playerStaring = false;
                ghostAttemptAttack = false;

                ghostStage = 0;
            }

            if (!playerStaring && !playerPreStaring && !ghostChasingStage && !ghostDeactivationStage) ghostStalkingStage = true;

            if ((raycastScript.targetInSight) && (!ghostChasingStage || ghostAttemptAttack) && !ghostDeactivationStage) //< If the player saw the Unver whilst in the stalking stage.
            {
                ghostChasingStage = false;
                ghostDeactivationStage = false;
                ghostStalkingStage = false;

                if (preStareTimer >= setPreStareTime) //< Determines whether the player is in the staring stage or the grace period before the staring stage.
                {
                    playerStaring = true;
                    playerPreStaring = false;
                    ghostAttemptAttack = false;
                }
                else
                {
                    playerPreStaring = true;
                    playerStaring = false;
                }

                if (playerPreStaring) //< If the player is in the grace period before the staring stage.
                {
                    if (!pauseTimer) preStareTimer += 1 * Time.deltaTime;
                }
                else if (playerStaring) //< If the player is in the staring stage.
                {
                    if (getStareRandomNumberOnce) //< Generates a number for the set stare time.
                    {
                        getStareRandomNumberOnce = false;
                        setStareTime = Random.Range(((setStareTimeMin + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostStareTimeMinTopTier - setStareTimeMin)))) * modeMultiplier, (setStareTimeMax + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostStareTimeMaxTopTier - setStareTimeMax)))) * modeMultiplier;
                        if (consoleStages) Debug.Log("Set Stare Time = " + setStareTime + " (" + ((setStareTimeMin + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostStareTimeMinTopTier - setStareTimeMin))) * modeMultiplier) + "," + ((setStareTimeMax + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostStareTimeMaxTopTier - setStareTimeMax))) * modeMultiplier) + ")");
                    }

                    Ray raycastDown = new Ray(gameObject.transform.position + new Vector3(0, raycastRange, 0), Vector3.down);
                    RaycastHit floorChaseHit;

                    if (Physics.Raycast(raycastDown, out floorChaseHit, raycastRange * 2, (1 << floorLayer))) //< Looks for floor.
                    {
                        if (Mathf.Abs(raycastRange - floorChaseHit.distance) < ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * ghostFloorClingeIntensity * Time.deltaTime) gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (raycastRange - floorChaseHit.distance), gameObject.transform.position.z); //If at floor.
                        else if (raycastRange - floorChaseHit.distance >= 0) gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * Time.deltaTime, gameObject.transform.position.z); //If below floor.
                        else gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * Time.deltaTime, gameObject.transform.position.z); //If above floor.
                    }

                    if (stareTimer >= setStareTime)
                    {
                        ghostDeactivationStage = true; //< Unver deactivated.
                        stareTimer = 0;
                        if (consoleStages) Debug.Log("Ghost deactivated for " + ((setDeactivationTime + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostDeactivationTimeTopTier - setDeactivationTime)))) / modeMultiplier + " seconds.");

                        if (ghostStage <= ghostStaredPushBack) ghostStage = 1;
                        else ghostStage -= ghostStaredPushBack;
                    }
                    else
                    {
                        if (!pauseTimer) stareTimer += 1 * Time.deltaTime;
                    }
                }
            }
            else
            {
                if (playerStaring && !ghostDeactivationStage) //< If the player looks away too early whilst in the staring stage.
                {
                    ghostChasingStage = true;
                    GhostSpawn();
                }
                else if (playerPreStaring) //< If the player looks away whilst in the grace period before the staring stage.
                {
                    ghostStalkingStage = true;
                    ghostAttemptAttack = false;
                    GhostSpawn();
                }

                getStareRandomNumberOnce = true;
                playerStaring = false;
                playerPreStaring = false;
                preStareTimer = 0;
            }
            
            if (ghostChasingStage) //< If the Unver is in the chasing stage.
            {
                playerStaring = false;
                ghostDeactivationStage = false;
                ghostStalkingStage = false;

                stalkingAudio.volume = 0f;
                chasingAudio.volume = 1f;

                if (chaseTimer >= ((setChaseTime + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostChaseTimeTopTier - setChaseTime)))) * modeMultiplier)
                {
                    ghostChasingStage = false;
                    ghostDeactivationStage = true;

                    if (consoleStages) Debug.Log("Ghost deactivated for " + (setDeactivationTime + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostDeactivationTimeTopTier - setDeactivationTime))) * modeMultiplier + " seconds.");
                }
                else
                {
                    ghostDeactivationStage = false;

                    Vector3 flattenedPlayerHead = playerHead.position;
                    flattenedPlayerHead.y = 0;
                    Vector3 flattenedGhostPrefab = gameObject.transform.position;
                    flattenedGhostPrefab.y = 0;

                    if (Vector3.Distance(flattenedPlayerHead, flattenedGhostPrefab) <= ghostChaseDistanceToDirect)
                    {
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, playerHead.position + playerHeadOffset, ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * Time.deltaTime); //< Moves the Unver towards the player.
                    }
                    else
                    {
                        Ray raycastDown = new Ray(gameObject.transform.position + new Vector3(0, raycastRange, 0), Vector3.down);
                        RaycastHit floorChaseHit;

                        if (Physics.Raycast(raycastDown, out floorChaseHit, raycastRange * 2, (1 << floorLayer))) //< Looks for floor.
                        {
                            if (Mathf.Abs(raycastRange - floorChaseHit.distance) < ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * ghostFloorClingeIntensity * Time.deltaTime) gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + (raycastRange - floorChaseHit.distance), gameObject.transform.position.z); //< If at floor.
                            else if (raycastRange - floorChaseHit.distance >= 0) gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * Time.deltaTime, gameObject.transform.position.z); //< If below floor.
                            else gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * Time.deltaTime, gameObject.transform.position.z); //< If above floor.
                        }

                        Vector3 targetPosition = new Vector3(playerHead.position.x + playerHeadOffset.x, gameObject.transform.position.y, playerHead.position.z + playerHeadOffset.z);
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, ((ghostSpeed + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostSpeedTopTier - ghostSpeed))) * modeMultiplier) * Time.deltaTime); //< Moves the Unver towards the player.
                    }

                    if (!ghostAttemptAttack && !pauseTimer) chaseTimer += 1 * Time.deltaTime;
                }
            }
            else
            {
                chaseTimer = 0;
            }

            if (ghostDeactivationStage) //< If the Unver is temporarily deactivated.
            {
                playerStaring = false;
                ghostChasingStage = false;
                ghostStalkingStage = false;

                stalkingAudio.volume = 0f;
                chasingAudio.volume = 0f;

                for (int i = 0; i < toggleAssets.Length; i++)
                {
                    toggleAssets[i].SetActive(false);
                }

                if (deactivationTimer >= ((setDeactivationTime + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostDeactivationTimeTopTier - setDeactivationTime)))) / modeMultiplier)
                {
                    ghostDeactivationStage = false;
                    ghostStalkingStage = true;

                    GhostSpawn();
                }
                else
                {
                    ghostStalkingStage = false;

                    if (!pauseTimer) deactivationTimer += 1 * Time.deltaTime;
                }
            }
            else
            {
                deactivationTimer = 0;

                for (int i = 0; i < toggleAssets.Length; i++)
                {
                    toggleAssets[i].SetActive(true);
                }
            }

            if (ghostStalkingStage) //< If the Unver is stalking the player.
            {
                playerStaring = false;
                ghostChasingStage = false;
                ghostDeactivationStage = false;

                stalkingAudio.volume = 1f;
                chasingAudio.volume = 0f;

                if (ghostSpawnDebug) //< For testing the spawning system.
                {
                    GhostSpawn();
                    ghostSpawnDebug = false;
                    stalkTimer = 0;
                }

                if (stalkTimer >= (setStalkTime + (((ghostTier - 1) / (ghostMaxTier - 1)) * (ghostStalkTimeTopTier - setStalkTime))) / modeMultiplier)
                {
                    if (ghostStage < 10)
                    {
                        ghostStage += 1;
                        if ((ghostStage >= ghostStageToAttemptLowest) && (ghostStage <= ghostStageToAttemptHighest) && (Random.Range(1, attackAttemptProbability) == 1))
                        {
                            ghostAttemptAttack = true;
                            ghostChasingStage = true;
                        }
                        else GhostSpawn();
                    }
                    else
                    {
                        ghostChasingStage = true;
                        ghostStalkingStage = false;
                    }

                    stalkTimer = 0;
                }
                else
                {
                    if (!pauseTimer && !ghostAttemptAttack) stalkTimer += 1 * Time.deltaTime;
                }
            }
            else
            {
                stalkTimer = 0;
                ghostSpawnDebug = false;
            }
        }
        else
        {
            playerStaring = false;
            playerPreStaring = false;
            ghostChasingStage = false;
            ghostDeactivationStage = false;
            ghostStalkingStage = false;
        }
    }

    /*!
     *  A method that spawns the Unver.
     */
    public void GhostSpawn()
    {
        ghostDistanceMax = ghostDistanceStageMax - ((ghostDistanceStageMax - ghostDistanceStageMin) / 10) * (ghostStage - 1); //< To generate the maximum Unver distance value.
        ghostDistanceMin = ghostDistanceStageMax - ((ghostDistanceStageMax - ghostDistanceStageMin) / 10) * ghostStage; //< To generate the minimum Unver distance value.

        Vector3 normalizedPlayerHead = playerHead.rotation.eulerAngles;
        normalizedPlayerHead.z = 0; //< Creates a Vector3 of the player's head rotations without the z axis.

        ghostAngle = Random.Range(0, 360);
        if (ghostChasingStage && !ghostAttemptAttack) spawnDistance = ghostChaseDistance;
        else spawnDistance = Random.Range(ghostDistanceMin, ghostDistanceMax);
        Vector3 spawnPosition = new Vector3(Mathf.Sin(ghostAngle) * spawnDistance, 0, Mathf.Cos(ghostAngle) * spawnDistance) + playerHead.position; //< Generates a Vector3 for the spawn position. (Shares y axis with the player's head.)

        Ray raycastDown = new Ray(spawnPosition + new Vector3(0, raycastRange, 0), Vector3.down);
        RaycastHit floorSpawnHit;

        if (Physics.Raycast(raycastDown, out floorSpawnHit, raycastRange * 2, (1 << floorLayer)))
        {
            spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y + (raycastRange - floorSpawnHit.distance), spawnPosition.z);
        }

        spawnAngle = Quaternion.Angle(Quaternion.LookRotation(spawnPosition - playerHead.position, Vector3.up), Quaternion.Euler(normalizedPlayerHead)); //< Gets the angle from the player's head position to the spawn position and to where the player's head is facing.

        Ray borrowedRaycast = new Ray(playerHead.position, spawnPosition - playerHead.position);
        RaycastHit borrowedHit;

        if (playerRaycastIgnoreLayers.Length == 0)
        {
            playerRaycastLayerMask = ~0;
        }
        else
        {
            playerRaycastLayerMask = 1 << playerRaycastIgnoreLayers[0];
            foreach (var raycastIgnoredLayer in playerRaycastIgnoreLayers.Skip(1))
            {
                playerRaycastLayerMask = (playerRaycastLayerMask | 1 << raycastIgnoredLayer);
            }
            playerRaycastLayerMask = ~playerRaycastLayerMask;
        }

        if ((!Physics.Raycast(raycastDown, out floorSpawnHit, raycastRange * 2, (1 << floorLayer)) || //< Checks if there's a floor.
            ((!Physics.Raycast(borrowedRaycast, out borrowedHit, Vector3.Distance(playerHead.position, spawnPosition), playerRaycastLayerMask)) && //< Checks if the player's line of sight is in the way. (Part 1: Raycast)
            (Vector3.Distance(raycastScript.gameObject.transform.position, playerHead.transform.position) <= raycastScript.maxRaycastDistance) && //< (Part 2: Distance)
            ((raycastScript.totalFieldOfView * 1.5) / 2 >= spawnAngle))) && //< (Part 3: Angle comparison)
            (ghostSpawnFailSafe < 10000f)) //< Cancels the spawn when it has relocated 10,000 times.
        {
            if (consoleGhostSpawn) Debug.Log("Relocating");
            ghostSpawnFailSafe += 1;
            GhostSpawn(); //< Relocates the spawn position.

            return;
        }
        else
        {
            if (ghostSpawnFailSafe >= 1000f) Debug.LogError("Ghost Spawn Failed"); //< Creates an error when the spawn position has relocated 1000 times.
            else
            {
                if (consoleGhostSpawn)
                {
                    Debug.Log("Ghost Spawn Distance = " + spawnDistance + " (" + ghostDistanceMin + "," + ghostDistanceMax + ")"); //< Debugs the spawn distance.
                    Debug.Log("Ghost Spawn Angle = " + spawnAngle + " (0, 360)"); //< Debugs the spawn angle.
                    Debug.Log("Spawn Position = " + spawnPosition); //< Debugs the spawn position.
                }

                gameObject.transform.position = spawnPosition; //< Teleports the Unver to the spawn position.
            }

            ghostSpawnFailSafe = 0f; //< Resets the fail safe.
        }
    }
}
