using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [Header("Instruction UI")]
    public Button instructionsButton;
    public GameObject instructionPanel;
    public Button closeInstructionButton;
    public TMP_Text instructionTitleText;
    public TMP_Text instructionBodyText;

    void Start()
    {
        ResolveSceneReferences();

        if (instructionPanel != null)
            instructionPanel.SetActive(false);

        if (instructionsButton == null)
            instructionsButton = CreateInstructionButton();
        else
            instructionsButton.onClick.AddListener(ShowInstructions);

        if (closeInstructionButton != null)
            closeInstructionButton.onClick.AddListener(HideInstructions);

        ApplyInstructionText();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }

    public void ShowInstructions()
    {
        if (instructionPanel == null)
        {
            ResolveSceneReferences();
        }

        if (instructionPanel == null)
            instructionPanel = CreateInstructionPanel();
        else if (closeInstructionButton == null || instructionTitleText == null || instructionBodyText == null)
            ResolveInstructionReferences();

        ApplyInstructionText();
        instructionPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        if (instructionPanel != null)
            instructionPanel.SetActive(false);
    }

    Button CreateInstructionButton()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        if (canvas == null)
            return null;

        GameObject buttonObject = new GameObject("InstructionsButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        buttonObject.transform.SetParent(canvas.transform, false);

        RectTransform rect = buttonObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = new Vector2(0f, -230f);
        rect.sizeDelta = new Vector2(280f, 58f);

        Image image = buttonObject.GetComponent<Image>();
        image.color = new Color(0.86f, 0.88f, 0.94f, 1f);

        Button button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(ShowInstructions);

        TMP_Text text = CreateText(buttonObject.transform, "Text", "INSTRUCTIONS", Vector2.zero, new Vector2(250f, 44f), 22f, TextAlignmentOptions.Center);
        text.color = new Color(0.08f, 0.08f, 0.1f, 1f);

        return button;
    }

    GameObject CreateInstructionPanel()
    {
        Canvas canvas = FindObjectOfType<Canvas>();

        GameObject overlay = new GameObject("InstructionPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
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
        contentRect.sizeDelta = new Vector2(680f, 430f);

        Image contentImage = content.GetComponent<Image>();
        contentImage.color = new Color(0.08f, 0.08f, 0.1f, 0.94f);

        CreateText(content.transform, "TitleText", "HOW TO PLAY", new Vector2(0f, 155f), new Vector2(580f, 70f), 36f, TextAlignmentOptions.Center);
        CreateText(content.transform, "BodyText", "", new Vector2(0f, 15f), new Vector2(570f, 260f), 21f, TextAlignmentOptions.TopLeft);

        GameObject closeButton = new GameObject("CloseButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
        closeButton.transform.SetParent(content.transform, false);

        RectTransform buttonRect = closeButton.GetComponent<RectTransform>();
        buttonRect.anchorMin = new Vector2(0.5f, 0.5f);
        buttonRect.anchorMax = new Vector2(0.5f, 0.5f);
        buttonRect.anchoredPosition = new Vector2(0f, -165f);
        buttonRect.sizeDelta = new Vector2(200f, 54f);

        Image buttonImage = closeButton.GetComponent<Image>();
        buttonImage.color = new Color(0.86f, 0.88f, 0.94f, 1f);

        Button button = closeButton.GetComponent<Button>();
        button.onClick.AddListener(HideInstructions);

        TMP_Text buttonText = CreateText(closeButton.transform, "Text", "CLOSE", Vector2.zero, new Vector2(180f, 40f), 22f, TextAlignmentOptions.Center);
        buttonText.color = new Color(0.08f, 0.08f, 0.1f, 1f);

        overlay.SetActive(false);
        ResolveInstructionReferences();
        ApplyInstructionText();
        return overlay;
    }

    void ResolveInstructionReferences()
    {
        if (instructionPanel == null)
            return;

        Transform content = instructionPanel.transform.Find("Content");
        Transform searchRoot = content != null ? content : instructionPanel.transform;

        if (instructionTitleText == null)
        {
            Transform title = searchRoot.Find("TitleText");

            if (title != null)
                instructionTitleText = title.GetComponent<TMP_Text>();
        }

        if (instructionBodyText == null)
        {
            Transform body = searchRoot.Find("BodyText");

            if (body != null)
                instructionBodyText = body.GetComponent<TMP_Text>();
        }

        if (closeInstructionButton == null)
        {
            Transform closeButton = searchRoot.Find("CloseButton");

            if (closeButton != null)
                closeInstructionButton = closeButton.GetComponent<Button>();
        }

        if (closeInstructionButton != null)
        {
            closeInstructionButton.onClick.RemoveListener(HideInstructions);
            closeInstructionButton.onClick.AddListener(HideInstructions);
        }
    }

    void ResolveSceneReferences()
    {
        if (instructionsButton == null)
        {
            GameObject buttonObject = GameObject.Find("InstructionsButton");

            if (buttonObject != null)
                instructionsButton = buttonObject.GetComponent<Button>();
        }

        if (instructionPanel == null)
        {
            GameObject panelObject = GameObject.Find("InstructionPanel");

            if (panelObject != null)
                instructionPanel = panelObject;
        }

        ResolveInstructionReferences();
    }

    void ApplyInstructionText()
    {
        if (instructionTitleText != null)
            instructionTitleText.text = "HOW TO PLAY";

        if (instructionBodyText != null)
        {
            instructionBodyText.text =
                "OBSERVE\nMemorize the map before it disappears.\n\n" +
                "MOVE\nUse WASD or arrow keys to move one tile at a time.\n\n" +
                "KEY & GOAL\nCollect the key first, then reach the goal.\n\n" +
                "VIEW MAP\nSome levels allow one short reveal. You cannot move while viewing the map.\n\n" +
                "NEW MECHANICS\nLater levels introduce portals, hidden traps, moving obstacles, a key thief, and mirrored maps.";
        }
    }

    TMP_Text CreateText(Transform parent, string name, string content, Vector2 position, Vector2 size, float fontSize, TextAlignmentOptions alignment)
    {
        GameObject textObject = new GameObject(name, typeof(RectTransform), typeof(CanvasRenderer), typeof(TextMeshProUGUI));
        textObject.transform.SetParent(parent, false);

        RectTransform rect = textObject.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(0.5f, 0.5f);
        rect.anchorMax = new Vector2(0.5f, 0.5f);
        rect.anchoredPosition = position;
        rect.sizeDelta = size;

        TMP_Text text = textObject.GetComponent<TMP_Text>();
        text.text = content;
        text.fontSize = fontSize;
        text.alignment = alignment;
        text.color = Color.white;
        text.enableWordWrapping = true;

        return text;
    }
}
