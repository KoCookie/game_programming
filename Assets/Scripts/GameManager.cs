using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    [Header("Intro UI")]
    public GameObject introPanel;
    public TMP_Text introTitleText;
    public TMP_Text introBodyText;
    public Button introStartButton;

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
    private LevelData loadedLevel;
    public LegendManager legendManager;
    public TrapManager trapManager;
    public MemoryThiefManager memoryThiefManager;
    public MirrorManager mirrorManager;
    public bool HasKey => hasKey;

    void Start()
    {
        if (mirrorManager == null)
        {
            mirrorManager = GetComponent<MirrorManager>();

            if (mirrorManager == null)
                mirrorManager = gameObject.AddComponent<MirrorManager>();
        }

        if (memoryThiefManager == null)
        {
            memoryThiefManager = GetComponent<MemoryThiefManager>();

            if (memoryThiefManager == null)
                memoryThiefManager = gameObject.AddComponent<MemoryThiefManager>();
        }

        mirrorManager.levelLoader = levelLoader;
        memoryThiefManager.levelLoader = levelLoader;
        memoryThiefManager.gameManager = this;

        loadedLevel = levelLoader.LoadCurrentLevel();
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
        if (introPanel != null) introPanel.SetActive(false);

        if (introStartButton != null)
        {
            introStartButton.onClick.RemoveListener(StartLevelAfterIntro);
            introStartButton.onClick.AddListener(StartLevelAfterIntro);
        }

        CreateLifeIcons();
        UpdateLifeUI();

        if (loadedLevel.showIntro)
            ShowIntroPanel(loadedLevel);
        else
            StartCoroutine(ObservationPhase());
    }

    void ShowIntroPanel(LevelData level)
    {
        playerController.DisableMovement();

        if (introPanel == null)
            introPanel = CreateIntroPanel();

        ResolveIntroReferences();

        if (introTitleText != null)
            introTitleText.text = level.introTitle;

        if (introBodyText != null)
            introBodyText.text = level.introBody;

        introPanel.SetActive(true);
    }

    void ResolveIntroReferences()
    {
        if (introPanel == null)
            return;

        Transform content = introPanel.transform.Find("Content");
        Transform searchRoot = content != null ? content : introPanel.transform;

        if (introTitleText == null)
        {
            Transform title = searchRoot.Find("TitleText");

            if (title != null)
                introTitleText = title.GetComponent<TMP_Text>();
        }

        if (introBodyText == null)
        {
            Transform body = searchRoot.Find("BodyText");

            if (body != null)
                introBodyText = body.GetComponent<TMP_Text>();
        }

        if (introStartButton == null)
        {
            Transform button = searchRoot.Find("StartButton");

            if (button != null)
                introStartButton = button.GetComponent<Button>();
        }

        if (introStartButton != null)
        {
            introStartButton.onClick.RemoveListener(StartLevelAfterIntro);
            introStartButton.onClick.AddListener(StartLevelAfterIntro);
        }
    }

    GameObject CreateIntroPanel()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        GameObject overlay = new GameObject("LevelIntroPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        overlay.transform.SetParent(canvas.transform, false);

        RectTransform overlayRect = overlay.GetComponent<RectTransform>();
        overlayRect.anchorMin = Vector2.zero;
        overlayRect.anchorMax = Vector2.one;
        overlayRect.offsetMin = Vector2.zero;
        overlayRect.offsetMax = Vector2.zero;

        Image overlayImage = overlay.GetComponent<Image>();
        overlayImage.color = new Color(0f, 0f, 0f, 0.72f);

        GameObject content = new GameObject("Content", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
        content.transform.SetParent(overlay.transform, false);

        RectTransform contentRect = content.GetComponent<RectTransform>();
        contentRect.anchorMin = new Vector2(0.5f, 0.5f);
        contentRect.anchorMax = new Vector2(0.5f, 0.5f);
        contentRect.anchoredPosition = Vector2.zero;
        contentRect.sizeDelta = new Vector2(620f, 380f);

        Image contentImage = content.GetComponent<Image>();
        contentImage.color = new Color(0.08f, 0.08f, 0.1f, 0.94f);

        CreateIntroText(content.transform, "TitleText", new Vector2(0f, 120f), new Vector2(540f, 70f), 34f, TextAlignmentOptions.Center);
        CreateIntroText(content.transform, "BodyText", new Vector2(0f, 10f), new Vector2(520f, 170f), 22f, TextAlignmentOptions.TopLeft);

        GameObject buttonObject = new GameObject("StartButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        buttonObject.transform.SetParent(content.transform, false);

        RectTransform buttonRect = buttonObject.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.anchoredPosition = new Vector2(0f, -140f);
        buttonRect.sizeDelta = new Vector2(220f, 56f);

        Image buttonImage = buttonObject.GetComponent<Image>();
        buttonImage.color = new Color(0.86f, 0.88f, 0.94f, 1f);

        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(StartLevelAfterIntro);

        TMP_Text buttonText = CreateIntroText(buttonObject.transform, "Text", Vector2.zero, new Vector2(200f, 44f), 22f, TextAlignmentOptions.Center);
        buttonText.text = "START";
        buttonText.color = new Color(0.08f, 0.08f, 0.1f, 1f);

        overlay.SetActive(false);
        return overlay;
    }

    TMP_Text CreateIntroText(Transform parent, string name, Vector2 position, Vector2 size, float fontSize, TextAlignmentOptions alignment)
    {
        GameObject textObject = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        textObject.transform.SetParent(parent, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        TMP_Text text = textObject.GetComponent<TMP_Text>();
        text.text = "";
        text.fontSize = fontSize;
        text.alignment = alignment;
        text.color = Color.white;
        text.enableWordWrapping = true;

        return text;
    }

    public void StartLevelAfterIntro()
    {
        if (introPanel != null)
            introPanel.SetActive(false);

        StartCoroutine(ObservationPhase());
    }

    IEnumerator ObservationPhase()
    {
        playerController.DisableMovement();
        phaseText.text = "OBSERVATION PHASE";

        LevelData level = levelLoader.GetCurrentLevel();
        bool shouldMirror = level != null && level.mirrorAtEndOfObservation && mirrorManager != null;
        float mirrorDuration = shouldMirror ? Mathf.Max(0.05f, level.mirrorAnimationDuration) : 0f;
        float timeLeft = shouldMirror ? Mathf.Max(0f, observationTime - mirrorDuration) : observationTime;

        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft + mirrorDuration).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        if (shouldMirror)
        {
            phaseText.text = "MIRROR SHIFT";
            timerText.text = Mathf.CeilToInt(mirrorDuration).ToString();
            yield return StartCoroutine(mirrorManager.PlayMirrorShift());
        }

        timerText.text = "";

        HideMapObjects();

        if (legendPanel != null)
            legendPanel.SetActive(false);
        
        if (trapManager != null)
            trapManager.ActivateTraps();

        if (memoryThiefManager != null)
            memoryThiefManager.ActivateMemoryThief();

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
        playerController.DisableMovement();
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

        playerController.EnableMovement();
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

    public void MemoryThiefAteKey()
    {
        if (gameEnded || hasKey) return;

        if (levelLoader.spawnedKey != null)
            levelLoader.spawnedKey.SetActive(false);

        gameEnded = true;
        phaseText.text = "KEY STOLEN";
        playerController.DisableMovement();

        if (losePanel != null)
            losePanel.SetActive(true);
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
