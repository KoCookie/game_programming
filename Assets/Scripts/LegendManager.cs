using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LegendManager : MonoBehaviour
{
    public Transform legendRoot;
    public GameObject legendItemPrefab;

    public Sprite playerSprite;
    public Sprite obstacleSprite;
    public Sprite keySprite;
    public Sprite goalSprite;
    public Sprite portalSprite;
    public Sprite heartSprite;
    public Sprite memoryThiefSprite;
    public Sprite gateBlockerSprite;

    [Header("Responsive Layout")]
    public float itemSpacing = 42f;
    public float boardGap = 70f;
    public float referencePixelsPerTile = 95f;
    public float minScale = 0.85f;
    public float maxScale = 1.1f;
    public float screenMargin = 160f;

    private LevelData currentLevel;
    private LevelLoader levelLoader;
    private RectTransform legendPanelRect;
    private RectTransform legendRootRect;
    private RectTransform canvasRect;
    private Canvas canvas;

    public void BuildLegend(LevelData levelData)
    {
        if (levelData == null || legendRoot == null || legendItemPrefab == null || levelData.legendItems == null)
            return;

        currentLevel = levelData;
        CacheLayoutReferences();

        foreach (Transform child in legendRoot)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < levelData.legendItems.Length; i++)
        {
            LegendItemType item = levelData.legendItems[i];

            GameObject entry = Instantiate(legendItemPrefab, legendRoot);

            RectTransform rect = entry.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, -i * itemSpacing);

            Image icon = entry.transform.Find("Icon").GetComponent<Image>();
            TMP_Text label = entry.transform.Find("Label").GetComponent<TMP_Text>();

            icon.sprite = GetSprite(item);
            icon.color = Color.white;

            label.text = GetLabel(item);
            label.color = Color.white;
        }

        CenterLegendItems(levelData.legendItems.Length);
    }

    public void ConfigureResponsiveLayout(LevelData levelData, LevelLoader loader)
    {
        currentLevel = levelData;
        levelLoader = loader;
        CacheLayoutReferences();
        UpdateResponsiveLayout();
    }

    void LateUpdate()
    {
        UpdateResponsiveLayout();
    }

    void CacheLayoutReferences()
    {
        if (legendRoot != null && legendRootRect == null)
            legendRootRect = legendRoot as RectTransform;

        if (legendRootRect != null && legendPanelRect == null)
            legendPanelRect = legendRootRect.parent as RectTransform;

        if (canvas == null && legendRoot != null)
            canvas = legendRoot.GetComponentInParent<Canvas>();

        if (canvasRect == null && canvas != null)
            canvasRect = canvas.transform as RectTransform;
    }

    void CenterLegendItems(int itemCount)
    {
        if (legendRootRect == null || itemCount <= 0)
            return;

        float contentHeight = (itemCount - 1) * itemSpacing;
        legendRootRect.anchoredPosition = new Vector2(60f, contentHeight * 0.5f);
    }

    void UpdateResponsiveLayout()
    {
        if (currentLevel == null || levelLoader == null)
            return;

        CacheLayoutReferences();

        Camera camera = Camera.main;
        if (camera == null || canvasRect == null || legendPanelRect == null)
            return;

        Vector3 boardCenterWorld = GetBoardCenterWorld();
        Vector3 boardLeftWorld = boardCenterWorld + Vector3.left * currentLevel.width * 0.5f;
        Vector3 boardNextTileWorld = boardLeftWorld + Vector3.right;

        Vector2 boardLeftCanvas = WorldToCanvasPoint(boardLeftWorld, camera);
        Vector2 boardCenterCanvas = WorldToCanvasPoint(boardCenterWorld, camera);

        float pixelsPerTile = Mathf.Abs(
            camera.WorldToScreenPoint(boardNextTileWorld).x
            - camera.WorldToScreenPoint(boardLeftWorld).x
        );

        float scale = Mathf.Clamp(pixelsPerTile / referencePixelsPerTile, minScale, maxScale);
        legendPanelRect.localScale = new Vector3(scale, scale, 1f);

        float panelHalfWidth = legendPanelRect.rect.width * scale * 0.5f;
        float leftSafeX = -canvasRect.rect.width * 0.5f + panelHalfWidth + screenMargin;
        float noOverlapX = boardLeftCanvas.x - boardGap - panelHalfWidth;
        float targetX = leftSafeX <= noOverlapX ? leftSafeX : noOverlapX;

        legendPanelRect.anchoredPosition = new Vector2(
            targetX,
            boardCenterCanvas.y
        );
    }

    Vector3 GetBoardCenterWorld()
    {
        Vector2Int bottomLeft = Vector2Int.zero;
        Vector2Int topRight = new Vector2Int(currentLevel.width - 1, currentLevel.height - 1);
        return (levelLoader.GridToWorld(bottomLeft) + levelLoader.GridToWorld(topRight)) * 0.5f;
    }

    Vector2 WorldToCanvasPoint(Vector3 worldPosition, Camera camera)
    {
        Vector2 screenPoint = camera.WorldToScreenPoint(worldPosition);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPoint,
            canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay ? canvas.worldCamera : null,
            out Vector2 localPoint
        );
        return localPoint;
    }

    Sprite GetSprite(LegendItemType item)
    {
        switch (item)
        {
            case LegendItemType.Player: return playerSprite;
            case LegendItemType.Obstacle: return obstacleSprite;
            case LegendItemType.Key: return keySprite;
            case LegendItemType.Goal: return goalSprite;
            case LegendItemType.Portal: return portalSprite;
            case LegendItemType.Heart: return heartSprite;
            case LegendItemType.MemoryThief: return memoryThiefSprite;
            case LegendItemType.GateBlocker: return gateBlockerSprite;
            default: return null;
        }
    }

    string GetLabel(LegendItemType item)
    {
        switch (item)
        {
            case LegendItemType.Player: return "PLAYER";
            case LegendItemType.Obstacle: return "OBSTACLE";
            case LegendItemType.Key: return "KEY";
            case LegendItemType.Goal: return "GOAL";
            case LegendItemType.Portal: return "PORTAL";
            case LegendItemType.Heart: return "LIFE";
            case LegendItemType.MemoryThief: return "THIEF";
            case LegendItemType.GateBlocker: return "BLOCKER";
            default: return "";
        }
    }
}
