using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls the ambience sound looper.
 *
 *  Independent
 */
public class AmbienceSoundLooper : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource[] ambienceSoundFirst = new AudioSource[1]; //!< The ambience sound firsts.
    [SerializeField] private AudioSource[] ambienceSoundSecond = new AudioSource[1]; //!< The ambience sound seconds.
    public int currentBackgroundAmbience = 0; //!< The current ambience sound.
    public bool activeSounds = true; //!< A boolean that controls whether or not the ambience sounds are active.
    public bool directStart = false; //!< A boolean that controls whether or not the ambience sounds start at full volume.

    [Header("Inputs")]
    public float masterVolume = 1f; //!< The master volume.
    [SerializeField] private float[] isolatedVolume = new float[1]; //!< The set volume for each audio source.
    [SerializeField] private float fadeTime = 1f; //!< The fading time in seconds.
    [SerializeField] private float autoFader = 2f; //!< Multiplied by the set fade time to determine the minumum length of the audio clip in seconds before using an automatic fading setting.
    [SerializeField] private float fadeOffset = 0f; //!< The amount of seconds to cut off the end of the audioclip.

    private float[] ambienceSoundTimer; //!< A timer used to determine whether or not the sounds should transition to first/second sounds.
    private bool[] isFirst; //!< A boolean that determines whether or not the ambience looper is currently on the first sounds.

    void OnEnable()
    {
        ambienceSoundTimer = new float[ambienceSoundFirst.Length];
        isFirst = new bool[ambienceSoundFirst.Length];

        for (int i = 0; i < ambienceSoundFirst.Length; i++)
        {
            if (i == currentBackgroundAmbience)
            {
                if (activeSounds && directStart) ambienceSoundFirst[i].volume = isolatedVolume[i] * masterVolume;
                else ambienceSoundFirst[i].volume = 0f;
            }
            else ambienceSoundFirst[i].volume = 0f;

            ambienceSoundSecond[i].volume = 0f;

            ambienceSoundTimer[i] = 0f;

            isFirst[i] = true;
        }
    }

    void Update()
    {
        currentBackgroundAmbience = Mathf.Clamp(currentBackgroundAmbience, 0, ambienceSoundFirst.Length - 1);
        if (currentBackgroundAmbience < 0) currentBackgroundAmbience = 0;
        if (currentBackgroundAmbience > ambienceSoundFirst.Length - 1) currentBackgroundAmbience = ambienceSoundFirst.Length - 1;

        if (autoFader < 1) autoFader = 1;

        for (int i = 0; i < ambienceSoundFirst.Length; i++)
        {
            if (isFirst[i]) //If first.
            {
                if (i == currentBackgroundAmbience && activeSounds)
                {
                    if (ambienceSoundFirst[i].volume < isolatedVolume[i] * masterVolume)
                    {
                        if (ambienceSoundFirst[i].volume > isolatedVolume[i] * masterVolume - (isolatedVolume[i] * masterVolume / fadeTime) * Time.deltaTime) ambienceSoundFirst[i].volume = isolatedVolume[i] * masterVolume; //Fades in ambience first.
                        else ambienceSoundFirst[i].volume += isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;
                    }
                }
                else
                {
                    if (ambienceSoundFirst[i].volume > 0f)
                    {
                        if (ambienceSoundFirst[i].volume < isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime) ambienceSoundFirst[i].volume = 0f; //Fades out ambience first.
                        else ambienceSoundFirst[i].volume -= isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;
                    }
                }
            }
            else //If second.
            {
                if (i == currentBackgroundAmbience && activeSounds)
                {
                    if (ambienceSoundSecond[i].volume < isolatedVolume[i] * masterVolume)
                    {
                        if (ambienceSoundSecond[i].volume > isolatedVolume[i] * masterVolume - (isolatedVolume[i] * masterVolume / fadeTime) * Time.deltaTime) ambienceSoundSecond[i].volume = isolatedVolume[i] * masterVolume; //Fades in ambience second.
                        else ambienceSoundSecond[i].volume += isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;
                    }
                }
                else
                {
                    if (ambienceSoundSecond[i].volume > 0f)
                    {
                        if (ambienceSoundSecond[i].volume < isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime) ambienceSoundSecond[i].volume = 0f; //Fades out ambience second.
                        else ambienceSoundSecond[i].volume -= isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;
                    }
                }
            }

            if (ambienceSoundFirst[i].clip.length - fadeOffset <= fadeTime * autoFader) //If the clip's length is too short, use auto fader setting instead (First).
            {
                if ((ambienceSoundTimer[i] >= (ambienceSoundFirst[i].clip.length - fadeOffset) / autoFader) || (!isFirst[i]))
                {
                    if (ambienceSoundFirst[i].volume < isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime) ambienceSoundFirst[i].volume = 0f; //Fades the ambience out when nearing the end of the audio clip (From Auto).
                    else ambienceSoundFirst[i].volume -= isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;

                    if (isFirst[i])
                    {
                        ambienceSoundTimer[i] = 0f;
                        ambienceSoundSecond[i].Play();
                        isFirst[i] = !isFirst[i];
                    }
                }
            }
            else
            {
                if ((ambienceSoundTimer[i] >= ambienceSoundFirst[i].clip.length - fadeOffset - fadeTime) || (!isFirst[i]))
                {
                    if (ambienceSoundFirst[i].volume < isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime) ambienceSoundFirst[i].volume = 0f; //Fades the ambience out when nearing the end of the audio clip (From Normal).
                    else ambienceSoundFirst[i].volume -= isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;

                    if (isFirst[i])
                    {
                        ambienceSoundTimer[i] = 0f;
                        ambienceSoundSecond[i].Play();
                        isFirst[i] = !isFirst[i];
                    }
                }
            }

            if (ambienceSoundSecond[i].clip.length - fadeOffset <= fadeTime * autoFader) //If the clip's length is too short, use auto fader setting instead (Second).
            {
                if ((ambienceSoundTimer[i] >= (ambienceSoundSecond[i].clip.length - fadeOffset) / autoFader) || (isFirst[i]))
                {
                    if (ambienceSoundSecond[i].volume < isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime) ambienceSoundSecond[i].volume = 0f; //Fades the ambience out when nearing the end of the audio clip (From Auto).
                    else ambienceSoundSecond[i].volume -= isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;

                    if (!isFirst[i])
                    {
                        ambienceSoundTimer[i] = 0f;
                        ambienceSoundFirst[i].Play();
                        isFirst[i] = !isFirst[i];
                    }
                }
            }
            else
            {
                if ((ambienceSoundTimer[i] >= ambienceSoundSecond[i].clip.length - fadeOffset - fadeTime) || (isFirst[i]))
                {
                    if (ambienceSoundSecond[i].volume < isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime) ambienceSoundSecond[i].volume = 0f; //Fades the ambience out when nearing the end of the audio clip (From Normal).
                    else ambienceSoundSecond[i].volume -= isolatedVolume[i] * masterVolume / fadeTime * Time.deltaTime;

                    if (!isFirst[i])
                    {
                        ambienceSoundTimer[i] = 0f;
                        ambienceSoundFirst[i].Play();
                        isFirst[i] = !isFirst[i];
                    }
                }
            }

            if (isFirst[i]) ambienceSoundTimer[i] = ambienceSoundFirst[i].time;
            else ambienceSoundTimer[i] = ambienceSoundSecond[i].time;
        }
    }
}
