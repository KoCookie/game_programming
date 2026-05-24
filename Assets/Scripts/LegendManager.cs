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

    public float itemSpacing = 42f;

    public void BuildLegend(LevelData levelData)
    {
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
            icon.color = GetColor(item);

            label.text = GetLabel(item);
            label.color = Color.white;
        }
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
            default: return "";
        }
    }
    Color GetColor(LegendItemType item)
    {
        switch (item)
        {
            case LegendItemType.Player:
                return new Color(0f, 0.7f, 1f);

            case LegendItemType.Obstacle:
                return Color.red;

            case LegendItemType.Key:
                return Color.yellow;

            case LegendItemType.Goal:
                return Color.green;

            case LegendItemType.Portal:
                return new Color(0.6f, 0f, 1f);

            case LegendItemType.Heart:
                return Color.red;

            default:
                return Color.white;
        }
    }
}