using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*! \brief A script that converts a text asset into working subtitles.
 *
 *  References: SettingsValues.
 */
public class DialogueSubtitles : MonoBehaviour
{
    [Header("Assets")]
    [SerializeField] private TextAsset transcript; //!< The transcript text asset.
    [SerializeField] private Text subtitles; //!< The subtitles text UI element.

    private string[] transcriptLines; //!< The individual lines in the transcript.

    private int lastScene; //!< The last scene used.
    private int lastInteraction; //!< The last interaction used.
    private int rememberedSceneLine; //!< The line index of the last scene used.
    private int rememberedInteractionLine; //!< The line index of the last interaction used.

    void Awake()
    {
        transcriptLines = transcript.text.Split('\n');
    }

    /*!
     *  A method that activates a subtitle.
     *  
     *  \param scene The subtitle scene.
     *  \param interaction The subtitle interaction.
     *  \param line The subtitle line.
     */
    public void ActivateSubtitle(int scene, int interaction, int line)
    {
        if (!SettingsValues.subtitlesActive) { return; }

        bool correctInteraction = false;

        int lineHeadstart = 0;

        if (scene == lastScene && interaction == lastInteraction)
        {
            lineHeadstart = rememberedInteractionLine;
        }
        else if (scene == lastScene)
        {
            lineHeadstart = rememberedSceneLine;
        }

        for (int i = 0 + lineHeadstart; i < transcriptLines.Length; i++)
        {
            string trimmedLine = transcriptLines[i].Trim();

            if (correctInteraction)
            {
                if (trimmedLine.StartsWith("[" + scene + "." + interaction + "." + line + "] "))
                {
                    lastScene = scene;
                    lastInteraction = interaction;

                    correctInteraction = true;

                    StopAllCoroutines();

                    subtitles.text = "";

                    StartCoroutine(PlaySubtitle(i));

                    return;
                }
            }
            else
            {
                if (trimmedLine == ">" + scene + ".0")
                {
                    rememberedSceneLine = i;
                }

                if (trimmedLine == ">" + scene + "." + interaction)
                {
                    rememberedInteractionLine = i;
                    correctInteraction = true;
                }
            }
        }
    }

    /*!
     *  A coroutine that plays a subtitle acording to its prefixes.
     *  
     *  \param line The subtitle line.
     */
    private IEnumerator PlaySubtitle(int line)
    {
        bool hidden = false;

        string lineText = transcriptLines[line].Trim().Remove(0, 8);

        if (lineText.StartsWith("h/ "))
        {
            lineText = lineText.Remove(0, 3);
            hidden = true;
        }

        if (lineText.StartsWith("</"))
        {
            lineText = lineText.Remove(0, 2);
        }

        string[] splitSubtitles = lineText.Split(new string[] { "</" }, System.StringSplitOptions.None);

        for (int i = 0; i < splitSubtitles.Length; i++)
        {
            float pauseTime = 0f;
            float playTime = 0f;
            string subtitlesString = "";

            string subtitlesName = "";
            bool hasName = false;

            string[] separatedString = splitSubtitles[i].Split(new string[] { "/>" }, System.StringSplitOptions.None);

            if (separatedString.Length > 0)
            {
                if (float.TryParse(separatedString[0].Split('|')[0], out float pauseParse))
                {
                    pauseTime = pauseParse;
                }
                else
                {
                    Debug.LogError("Subtitles parse failed.");
                }

                if (float.TryParse(separatedString[0].Split('|')[1], out float playParse))
                {
                    playTime = playParse;
                }
                else
                {
                    Debug.LogError("Subtitles parse failed.");
                }

                subtitlesString = separatedString[1].Trim();

                if (subtitlesString.StartsWith("n/"))
                {
                    subtitlesString = subtitlesString.Remove(0, 2);

                    subtitlesName = subtitlesString.Split(new string[] { ": " }, System.StringSplitOptions.None)[0];

                    subtitlesString = subtitlesString.Remove(0, subtitlesName.Length + 2);

                    hasName = true;
                }
            }

            {
                yield return new WaitForSeconds(pauseTime);

                if (!hidden || SettingsValues.subtitlesShowHidden)
                {
                    if (SettingsValues.subtitlesShowNames && hasName)
                    {
                        subtitles.text = subtitlesName + ": " + subtitlesString;
                    }
                    else
                    {
                        subtitles.text = subtitlesString;
                    }
                }

                yield return new WaitForSeconds(playTime);

                subtitles.text = "";
            }
        }
    }
}
