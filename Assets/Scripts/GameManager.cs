using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Level Loader")]
    public LevelLoader levelLoader;

    [Header("UI")]
    public GameObject legendPanel;
    public TMP_Text timerText;
    public TMP_Text phaseText;
    public GameObject viewMapButton;
    public GameObject keyStatusIcon;
    public Transform lifePanel;
    public GameObject lifeIconPrefab;
    private GameObject[] lifeIcons;

    [Header("Result Panels")]
    public GameObject winPanel;
    public GameObject losePanel;

    [Header("Player")]
    public PlayerController playerController;

    [Header("Settings")]
    public float observationTime = 5f;
    public int lives = 3;
    public int maxLives = 5;
    public int viewMapUses = 1;
    public float viewMapDuration = 3f;

    private bool hasKey = false;
    private bool gameEnded = false;
    public LegendManager legendManager;
    public TrapManager trapManager;

    void Start()
    {
        LevelData loadedLevel = levelLoader.LoadCurrentLevel();
        playerController = levelLoader.spawnedPlayer;
        legendManager.BuildLegend(loadedLevel);

        observationTime = loadedLevel.observationTime;
        lives = loadedLevel.lives;
        viewMapUses = loadedLevel.viewMapUses;
        viewMapDuration = loadedLevel.viewMapDuration;

        hasKey = false;
        gameEnded = false;

        if (keyStatusIcon != null) keyStatusIcon.SetActive(false);
        if (viewMapButton != null) viewMapButton.SetActive(false);
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

        CreateLifeIcons();
        UpdateLifeUI();

        StartCoroutine(ObservationPhase());
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

        timerText.text = "";

        HideMapObjects();

        if (legendPanel != null)
            legendPanel.SetActive(false);
        
        if (trapManager != null)
            trapManager.ActivateTraps();

        phaseText.text = "ACTION PHASE";
        playerController = levelLoader.spawnedPlayer;
        playerController.EnableMovement();

        if (viewMapButton != null && viewMapUses > 0)
            viewMapButton.SetActive(true);
    }

    void HideMapObjects()
    {
        SpriteRenderer[] renderers = levelLoader.mapObjectsRoot.GetComponentsInChildren<SpriteRenderer>(true);

        foreach (SpriteRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    void ShowMapObjects()
    {
        SpriteRenderer[] renderers = levelLoader.mapObjectsRoot.GetComponentsInChildren<SpriteRenderer>(true);

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

        if (legendPanel != null)
            legendPanel.SetActive(true);

        float timeLeft = viewMapDuration;

        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "";
        HideMapObjects();
        if (legendPanel != null)
            legendPanel.SetActive(false);
    }

    public void LoseLife()
    {
        if (gameEnded) return;

        lives--;
        UpdateLifeUI();

        if (lives <= 0)
            GameOver();
    }

    public void GetKey()
    {
        hasKey = true;

        if (keyStatusIcon != null)
            keyStatusIcon.SetActive(true);
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
        }
    }

    void GameOver()
    {
        gameEnded = true;
        phaseText.text = "GAME OVER";
        playerController.DisableMovement();

        if (losePanel != null)
            losePanel.SetActive(true);
    }

    void UpdateLifeUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            UnityEngine.UI.Image iconImage = lifeIcons[i].GetComponent<UnityEngine.UI.Image>();

            if (iconImage != null)
            {
                iconImage.enabled = i < lives;
            }
        }
    }

    public void RestartLevel()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    public void BackToLevelSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelectScene");
    }

    public void LoadNextLevel()
    {
        LevelManager.selectedLevel++;

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
    public void GainLife()
    {
        if (gameEnded) return;

        lives++;
        UpdateLifeUI();
    }
    void CreateLifeIcons()
    {
        foreach (Transform child in lifePanel)
        {
            Destroy(child.gameObject);
        }

        lifeIcons = new GameObject[lives];

        for (int i = 0; i < lives; i++)
        {
            GameObject icon = Instantiate(lifeIconPrefab, lifePanel);
            lifeIcons[i] = icon;
        }
    }

    public void UsePortal(Vector3 currentPosition)
    {
        LevelData level = levelLoader.GetCurrentLevel();

        if (!level.hasPortal) return;

        Vector3 portalAWorld = levelLoader.GridToWorld(level.portalA);
        Vector3 portalBWorld = levelLoader.GridToWorld(level.portalB);

        float distanceToA = Vector3.Distance(currentPosition, portalAWorld);
        float distanceToB = Vector3.Distance(currentPosition, portalBWorld);

        if (distanceToA < distanceToB)
        {
            playerController.TeleportTo(portalBWorld);
        }
        else
        {
            playerController.TeleportTo(portalAWorld);
        }
    }
}
