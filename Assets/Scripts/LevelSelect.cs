using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [Header("Page UI")]
    public GameObject[] pages;
    public Button previousPageButton;
    public Button nextPageButton;

    private int currentPage;

    void Start()
    {
        ResolvePageReferences();
        BindPageButtons();

        if (pages != null && pages.Length > 0)
        {
            ShowPage(currentPage);
        }
        else
        {
            UpdatePageButtons();
        }
    }

    void ResolvePageReferences()
    {
        if (pages == null || pages.Length == 0)
        {
            List<GameObject> foundPages = new List<GameObject>();

            for (int i = 1; i <= 9; i++)
            {
                GameObject page = GameObject.Find("Page" + i);
                if (page == null)
                {
                    page = GameObject.Find("LevelPage" + i);
                }

                if (page == null)
                {
                    if (foundPages.Count > 0)
                    {
                        break;
                    }

                    continue;
                }

                foundPages.Add(page);
            }

            if (foundPages.Count > 0)
            {
                pages = foundPages.ToArray();
            }
        }

        if (previousPageButton == null)
        {
            previousPageButton = FindButton("PreviousPageButton");
            if (previousPageButton == null)
            {
                previousPageButton = FindButton("PrevPageButton");
            }
        }

        if (nextPageButton == null)
        {
            nextPageButton = FindButton("NextPageButton");
        }
    }

    Button FindButton(string objectName)
    {
        GameObject buttonObject = GameObject.Find(objectName);
        return buttonObject != null ? buttonObject.GetComponent<Button>() : null;
    }

    void BindPageButtons()
    {
        if (previousPageButton != null)
        {
            previousPageButton.onClick.RemoveListener(ShowPreviousPage);
            previousPageButton.onClick.AddListener(ShowPreviousPage);
        }

        if (nextPageButton != null)
        {
            nextPageButton.onClick.RemoveListener(ShowNextPage);
            nextPageButton.onClick.AddListener(ShowNextPage);
        }
    }

    public void ShowPreviousPage()
    {
        if (pages == null || pages.Length == 0)
        {
            return;
        }

        ShowPage(currentPage - 1);
    }

    public void ShowNextPage()
    {
        if (pages == null || pages.Length == 0)
        {
            return;
        }

        ShowPage(currentPage + 1);
    }

    public void ShowPage(int pageIndex)
    {
        if (pages == null || pages.Length == 0)
        {
            return;
        }

        currentPage = Mathf.Clamp(pageIndex, 0, pages.Length - 1);

        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] != null)
            {
                pages[i].SetActive(i == currentPage);
            }
        }

        UpdatePageButtons();
    }

    void UpdatePageButtons()
    {
        bool hasPages = pages != null && pages.Length > 0;

        if (previousPageButton != null)
        {
            previousPageButton.interactable = hasPages && currentPage > 0;
        }

        if (nextPageButton != null)
        {
            nextPageButton.interactable = hasPages && currentPage < pages.Length - 1;
        }
    }

    public void LoadLevel(int levelNumber)
    {
        LevelManager.selectedLevel = levelNumber;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadLevel1()
    {
        LoadLevel(1);
    }

    public void LoadLevel2()
    {
        LoadLevel(2);
    }

    public void LoadLevel3()
    {
        LoadLevel(3);
    }
    public void LoadLevel4()
    {
        LoadLevel(4);
    }

    public void LoadLevel5()
    {
        LoadLevel(5);
    }
    public void LoadLevel6()
    {
        LoadLevel(6);
    }
    public void LoadLevel7()
    {
        LoadLevel(7);
    }
    public void LoadLevel8()
    {
        LoadLevel(8);
    }
    public void LoadLevel9()
    {
        LoadLevel(9);
    }
    public void LoadLevel10()
    {
        LoadLevel(10);
    }
}
