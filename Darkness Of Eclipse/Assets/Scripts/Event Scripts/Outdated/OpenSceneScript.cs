using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*! \brief A script that controls the cutscene before the prologue.
 *  (Outdated)
 *
 *  [Event Script]
 */
public class OpenSceneScript : MonoBehaviour
{
    [Header("UI Elements")]
    public Text seconds; //!< The seconds text UI element.
    public Text minutes; //!< The seconds text UI element.
    public Text timeText; //!< The time text UI element.
    public Text dateText; //!< The date text UI element.
    public Image blackBG; //!< The black background image UI element.

    [Header("Start Time")]
    [Range(0, 59)] public int secondsInput; //!< The seconds that the scene wil start on.
    [Range(0, 59)] public int minutesInput; //!< The minutes that the scene wil start on.

    [Header("Stop Time")]
    [Range(0, 59)] public int secondStop; //!< The seconds that the scene wil end on.
    [Range(0, 59)] public int minuteStop; //!< The minutes that the scene wil end on.

    [Header("Scene")]
    public int nextScene; //!< The integer of the next scene.

    [Header("Inputs")]
    [Range(0f, 1f)] public float secondOffset; //!< The amount of time in seconds the timer will start on to offset the first second.
    public float timeFadeOffset; //!< The amount of time in seconds the time UI will start fading in.
    public float dateFadeOffset; //!< The amount of time in seconds the date UI will start fading in.
    public float fadeSpeed; //!< The speed at which the UI elements will fade in.
    public float textEnd; //!< The time in seconds the text UI elements will stay visable after freezing.
    public float sceneEnd; //!< The time in seconds the scene will pause at the end before moving on to the next scene.

    private float timer = 0f; //!< The timer used throughout the scene.
    private float fadeTimer = 0f; //!< The timer used to fade UI elements.
    private bool timeFaded = false; //!< A boolean that determines whether or not the time UI element has faded.
    private bool dateFaded = false; //!< A boolean that determines whether or not the time UI element has faded.
    private bool timerReset1 = false; //!< A boolean that determines whether or not the timer has reset once.
    private bool timerReset2 = false; //!< A boolean that determines whether or not the timer has reset twice.

    private bool mainEnd = false; //!< A boolean that determines whether or not the minutes and seconds have reached the stopping point.
    private bool miniEnd = false; //!< A boolean that determines whether or not the minutes and seconds have vanished.

    private Color colourTime; //!< The time UI colour.
    private Color colourDate; //!< The date UI colour.
    private Color colourSeconds; //!< The seconds UI colour.
    private Color colourMinutes; //!< The minutes UI colour.
    private Color colourBlackBG; //!< The background UI colour.

    void Start()
    {
        seconds.text = secondsInput.ToString();
        minutes.text = minutesInput.ToString();

        timer += secondOffset;

        colourTime = timeText.color;
        colourDate = dateText.color;
        colourSeconds = seconds.color;
        colourMinutes = minutes.color;
        colourBlackBG = blackBG.color;

        colourTime.a = 0f;
        colourDate.a = 0f;
        colourSeconds.a = 0f;
        colourMinutes.a = 0f;
        colourBlackBG.a = 1f;
    }

    void Update()
    {
        if (!timeFaded)
        {
            if (fadeTimer >= timeFadeOffset)
            {
                if (colourTime.a >= 1f)
                {
                    colourTime.a = 1f;
                    colourSeconds.a = 1f;
                    colourMinutes.a = 1f;

                    fadeTimer = 0f;
                    timeFaded = true;
                }
                else
                {
                    colourTime.a += Time.deltaTime * fadeSpeed;
                    colourSeconds.a += Time.deltaTime * fadeSpeed;
                    colourMinutes.a += Time.deltaTime * fadeSpeed;
                }
            }
            else
            {
                fadeTimer += Time.deltaTime;
            }
        }

        if (!dateFaded)
        {
            if (fadeTimer >= dateFadeOffset)
            {
                if (colourDate.a >= 1f)
                {
                    colourDate.a = 1f;

                    dateFaded = true;
                }
                else
                {
                    colourDate.a += Time.deltaTime * fadeSpeed;
                }
            }
            else
            {
                fadeTimer += Time.deltaTime;
            }
        }

        if ((timer >= 1f) && (!mainEnd) && ((secondsInput < secondStop) || (minutesInput < minuteStop)))
        {
            secondsInput ++;

            if (secondsInput >= 60)
            {
                secondsInput = 0;
                minutesInput ++;
            }

            if (secondsInput < 10) seconds.text = "0" + secondsInput.ToString();
            else seconds.text = secondsInput.ToString();

            if (minutesInput < 10) minutes.text = "0" + minutesInput.ToString();
            else minutes.text = minutesInput.ToString();

            timer = 0f;
        }
        else if ((secondsInput >= secondStop) && (minutesInput >= minuteStop))
        {
            mainEnd = true;
        }

        if (mainEnd)
        {
            if (!timerReset1)
            {
                timer = 0f;
                timerReset1 = true;
            }
            else if (!miniEnd)
            {
                if (timer >= textEnd)
                {
                    colourTime.a = 0f;
                    colourDate.a = 0f;
                    colourSeconds.a = 0f;
                    colourMinutes.a = 0f;

                    miniEnd = true;
                }
            }
            else
            {
                if (!timerReset2)
                {
                    timer = 0f;
                    timerReset2 = true;
                }
                else if (timer >= sceneEnd) SceneManager.LoadScene(nextScene);
            }
        }

        timeText.color = colourTime;
        dateText.color = colourDate;
        seconds.color = colourSeconds;
        minutes.color = colourMinutes;
        blackBG.color = colourBlackBG;

        timer += Time.deltaTime * 1.003f;
    }
}
