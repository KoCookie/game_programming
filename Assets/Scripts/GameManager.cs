using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Level Objects")]
    public GameObject gridLevel1;
    public GameObject mapObjectsLevel1;
    public GameObject gridLevel2;
    public GameObject mapObjectsLevel2;

    private GameObject currentGrid;
    private GameObject currentMapObjects;

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text phaseText;
    public GameObject viewMapButton;
    public GameObject keyStatusIcon;
    public GameObject[] lifeIcons;

    [Header("Player")]
    public PlayerController playerController;

    [Header("Settings")]
    public float observationTime = 5f;
    public int lives = 3;
    public int viewMapUses = 1;
    public float viewMapDuration = 3f;

    private bool hasKey = false;
    private bool gameEnded = false;

    [Header("Result Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    void Start()
    {
        SetupLevel();

        hasKey = false;
        gameEnded = false;

        if (keyStatusIcon != null)
            keyStatusIcon.SetActive(false);

        if (viewMapButton != null)
            viewMapButton.SetActive(false);

        if (winPanel != null)
            winPanel.SetActive(false);

        if (losePanel != null)
            losePanel.SetActive(false);

        UpdateLifeUI();

        StartCoroutine(ObservationPhase());
    }

    void SetupLevel()
    {
        gridLevel1.SetActive(false);
        mapObjectsLevel1.SetActive(false);
        gridLevel2.SetActive(false);
        mapObjectsLevel2.SetActive(false);

        if (LevelManager.selectedLevel == 1)
        {
            currentGrid = gridLevel1;
            currentMapObjects = mapObjectsLevel1;
        }
        else if (LevelManager.selectedLevel == 2)
        {
            currentGrid = gridLevel2;
            currentMapObjects = mapObjectsLevel2;
        }

        currentGrid.SetActive(true);
        currentMapObjects.SetActive(true);

        playerController.gridRoot = currentGrid.transform;
    }

    IEnumerator ObservationPhase()
    {
        playerController.DisableMovement();
        phaseText.text = "OBSERVATION PHASE";

        float timeLeft = observationTime;

        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "0";

        HideMapObjects();

        phaseText.text = "ACTION PHASE";
        playerController.EnableMovement();

        if (viewMapButton != null && viewMapUses > 0)
            viewMapButton.SetActive(true);

        Debug.Log("Action Phase Started");
    }

    void HideMapObjects()
    {
        SpriteRenderer[] renderers = currentMapObjects.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    void ShowMapObjects()
    {
        SpriteRenderer[] renderers = currentMapObjects.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = true;
        }
    }

    public void ViewMap()
    {
        if (gameEnded) return;
        if (viewMapUses <= 0) return;

        viewMapUses--;

        if (viewMapButton != null)
            viewMapButton.SetActive(false);

        StartCoroutine(ViewMapCoroutine());
    }

    IEnumerator ViewMapCoroutine()
    {
        ShowMapObjects();

        float timeLeft = viewMapDuration;

        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "0";

        HideMapObjects();
    }

    public void LoseLife()
    {
        if (gameEnded) return;

        lives--;
        Debug.Log("Lives Left: " + lives);

        UpdateLifeUI();

        if (lives <= 0)
            GameOver();
    }

    public void GetKey()
    {
        hasKey = true;

        if (keyStatusIcon != null)
            keyStatusIcon.SetActive(true);

        Debug.Log("Key Collected");
    }

    public void ReachGoal()
    {
        if (gameEnded) return;

        if (hasKey)
        {
            gameEnded = true;
            phaseText.text = "YOU WIN";
            playerController.DisableMovement();

            if (winPanel != null)
                winPanel.SetActive(true);

            Debug.Log("Player Wins");
        }
        else
        {
            Debug.Log("Need Key First");
        }
    }

    void GameOver()
    {
        gameEnded = true;
        phaseText.text = "GAME OVER";
        playerController.DisableMovement();

        if (losePanel != null)
            losePanel.SetActive(true);

        Debug.Log("Game Over");
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].SetActive(i < lives);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void BackToLevelSelect()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectScene");
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        LevelManager.selectedLevel++;

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}