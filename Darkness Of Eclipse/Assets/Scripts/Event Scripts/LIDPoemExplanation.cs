using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(EnableDisable))]
public class LIDPoemExplanation : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private float timePhase1 = 2.5f;
    [SerializeField] private float timePhase2 = 10f;
    [SerializeField] private float timePhase3 = 2.5f;
    [SerializeField] private Vector3Int subtitlesID;
    [SerializeField] [Range(0f, 1f)]  private float volumeStartWeight;
    [SerializeField] [Range(0f, 1f)] private float volumeMidWeight;
    [SerializeField] [Range(0f, 1f)] private float volumeEndWeight;


    [Header("References")]
    [SerializeField] private AudioSource lineAudio;
    [SerializeField] private Material skyboxMaterial;
    [SerializeField] private DialogueSubtitles substitlesScript;
    [SerializeField] private Volume newVolume;
    [SerializeField] private Volume oldVolume;
    [SerializeField] private InvokeFromFunction invokeScript;

    public void Activate()
    {
        StartCoroutine(EventActive());
    }

    private IEnumerator EventActive()
    {
        lineAudio.Play();
        substitlesScript.ActivateSubtitle(subtitlesID.x, subtitlesID.y, subtitlesID.z);

        newVolume.enabled = true;

        for (float t = 0; t < timePhase1; t += Time.deltaTime)
        {
            newVolume.weight = t / timePhase1 * volumeStartWeight;

            yield return null;
        }

        EnableDisable enableDisableScript = gameObject.GetComponent<EnableDisable>();
        Material oldSkybox = RenderSettings.skybox;
        newVolume.weight = 1f;
        oldVolume.weight = 0f;

        oldVolume.enabled = false;

        enableDisableScript.PositiveTrigger();
        RenderSettings.skybox = skyboxMaterial;

        float tp2Half = timePhase2 / 2f;

        for (float t = 0; t < tp2Half; t += Time.deltaTime)
        {
            newVolume.weight = 1f - (t / tp2Half * (1f - volumeMidWeight));

            yield return null;
        }

        for (float t = 0; t < tp2Half; t += Time.deltaTime)
        {
            newVolume.weight = volumeMidWeight + (t / tp2Half * (volumeEndWeight - volumeMidWeight));

            yield return null;
        }

        enableDisableScript.NegativeTrigger();
        RenderSettings.skybox = oldSkybox;
        newVolume.weight = 0f;
        oldVolume.weight = 1f;

        newVolume.enabled = false;
        oldVolume.enabled = true;

        yield return new WaitForSeconds(timePhase3);

        invokeScript.Invoke();
    }
}
