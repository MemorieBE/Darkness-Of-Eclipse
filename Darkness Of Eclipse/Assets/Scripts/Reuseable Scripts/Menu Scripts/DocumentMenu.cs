using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*! \brief A script that controls a document menu containing pages and chapters.
 *
 *  Independent
 */
public class DocumentMenu : MonoBehaviour
{
    public static int[] chapter; //!< The chapters for each document.
    public static int[] page; //!< The pages for each document.
    public static bool[][] chapterStates; //!< The chapter states.
    public static bool[] previousButtonState; //!< The previous button state.
    public static bool[] nextButtonState; //!< The next button state.

    [Header("Document")]
    public int documentID = 0; //!< The document ID.

    [Header("Chapters and Pages")]
    public GameObject[] chapters; //!< The chapter game objects.
    private GameObject[][] pages; //!< The page game objects.

    [Header("Previous and Next Buttons")]
    [SerializeField] private GameObject previousPageButton; //!< The previous page button game object.
    [SerializeField] private GameObject nextPageButton; //!< The next page button game object.

    [Header("Debug")]
    [SerializeField] private bool updateChapters = false; //!< A boolean that triggers the update chapter states function.

    void Start()
    {
        pages = new GameObject[chapters.Length][];

        if (chapter == null || chapter.Length <= documentID)
        {
            chapter = new int[documentID + 1];
            page = new int[documentID + 1];
            chapterStates = new bool[documentID + 1][];
            previousButtonState = new bool[documentID + 1];
            nextButtonState = new bool[documentID + 1];
        }

        if (chapterStates[documentID] == null || chapterStates[documentID].Length != chapters.Length)
        {
            chapterStates[documentID] = new bool[chapters.Length];
        }

        bool debugStart = false;

        for (int i = 0; i < chapters.Length; i++)
        {
            pages[i] = new GameObject[chapters[i].transform.childCount];

            for (int j = 0; j < pages[i].Length; j++)
            {
                pages[i][j] = chapters[i].transform.GetChild(j).gameObject;
            }

            if (chapters[i].activeSelf) 
            { 
                chapterStates[documentID][i] = true;

                debugStart = true;
            }
            else if (chapterStates[documentID][i]) 
            { 
                chapters[i].SetActive(true);
            }
        }

        pages[chapter[documentID]][page[documentID]].SetActive(true);

        previousPageButton.SetActive(previousButtonState[documentID]);
        nextPageButton.SetActive(nextButtonState[documentID]);

        if (debugStart) { UpdateChapterStates(); }
    }

    /*!
     *  A method that goes to the previous page.
     */
    public void PreviousPage()
    {
        if (page[documentID] <= 0)
        {
            int prevChapter = chapter[documentID];

            for (int i = prevChapter - 1; i >= 0; i--)
            {
                if (chapters[chapter[documentID]].activeSelf)
                {
                    prevChapter = i;
                    break;
                }
            }

            if (prevChapter == chapter[documentID])
            {
                previousPageButton.SetActive(false);
                previousButtonState[documentID] = false;

                return;
            }

            chapters[chapter[documentID]].SetActive(false);
            pages[chapter[documentID]][page[documentID]].SetActive(false);

            chapter[documentID] = prevChapter;
            page[documentID] = 0;

            chapters[chapter[documentID]].SetActive(true);
            pages[chapter[documentID]][page[documentID]].SetActive(true);
        }
        else
        {
            pages[chapter[documentID]][page[documentID]].SetActive(false);

            page[documentID]--;

            pages[chapter[documentID]][page[documentID]].SetActive(true);
        }

        if (page[documentID] > 0)
        {
            previousPageButton.SetActive(true);
            previousButtonState[documentID] = true;
        }
        else
        {
            bool isPrev = false;

            for (int i = 0; i < chapter[documentID]; i++)
            {
                if (chapters[i].activeSelf)
                {
                    isPrev = true;
                    break;
                }
            }

            if (isPrev)
            {
                previousPageButton.SetActive(true);
                previousButtonState[documentID] = true;
            }
            else
            {
                previousPageButton.SetActive(false);
                previousButtonState[documentID] = false;
            }
        }

        nextPageButton.SetActive(true);
        nextButtonState[documentID] = true;
    }

    /*!
     *  A method that goes to the next page.
     */
    public void NextPage()
    {
        if (page[documentID] >= pages[chapter[documentID]].Length - 1)
        {
            int nextChapter = chapter[documentID];

            for (int i = nextChapter + 1; i < chapters.Length; i++)
            {
                if (chapters[chapter[documentID]].activeSelf)
                {
                    nextChapter = i;
                    break;
                }
            }

            if (nextChapter == chapter[documentID])
            {
                nextPageButton.SetActive(false);
                nextButtonState[documentID] = false;

                return;
            }

            chapters[chapter[documentID]].SetActive(false);
            pages[chapter[documentID]][page[documentID]].SetActive(false);

            chapter[documentID] = nextChapter;
            page[documentID] = 0;

            chapters[chapter[documentID]].SetActive(true);
            pages[chapter[documentID]][page[documentID]].SetActive(true);
        }
        else
        {
            pages[chapter[documentID]][page[documentID]].SetActive(false);

            page[documentID]++;

            pages[chapter[documentID]][page[documentID]].SetActive(true);
        }

        if (page[documentID] < pages[chapter[documentID]].Length - 1)
        {
            nextPageButton.SetActive(true);
            nextButtonState[documentID] = true;
        }
        else
        {
            bool isNext = false;

            for (int i = chapter[documentID] + 1; i < chapters.Length; i++)
            {
                if (chapters[i].activeSelf)
                {
                    isNext = true;
                    break;
                }
            }

            if (isNext)
            {
                nextPageButton.SetActive(true);
                nextButtonState[documentID] = true;
            }
            else
            {
                nextPageButton.SetActive(false);
                nextButtonState[documentID] = false;
            }
        }

        previousPageButton.SetActive(true);
        previousButtonState[documentID] = true;
    }

    public void UpdateChapterStates()
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            if (chapters[i].activeSelf) { chapterStates[documentID][i] = true; }
            else { chapterStates[documentID][i] = false; }
        }

        if (page[documentID] > 0)
        {
            previousPageButton.SetActive(true);
            previousButtonState[documentID] = true;
        }
        else
        {
            bool isPrev = false;

            for (int i = 0; i < chapter[documentID]; i++)
            {
                if (chapters[i].activeSelf)
                {
                    isPrev = true;
                    break;
                }
            }

            if (isPrev)
            {
                previousPageButton.SetActive(true);
                previousButtonState[documentID] = true;
            }
            else
            {
                previousPageButton.SetActive(false);
                previousButtonState[documentID] = false;
            }
        }

        if (page[documentID] < pages[chapter[documentID]].Length - 1)
        {
            nextPageButton.SetActive(true);
            nextButtonState[documentID] = true;
        }
        else
        {
            bool isNext = false;

            for (int i = chapter[documentID] + 1; i < chapters.Length; i++)
            {
                if (chapters[i].activeSelf)
                {
                    isNext = true;
                    break;
                }
            }

            if (isNext)
            {
                nextPageButton.SetActive(true);
                nextButtonState[documentID] = true;
            }
            else
            {
                nextPageButton.SetActive(false);
                nextButtonState[documentID] = false;
            }
        }
    }

    void OnValidate()
    {
        if (updateChapters)
        {
            updateChapters = false;

            UpdateChapterStates();
        }
    }
}
