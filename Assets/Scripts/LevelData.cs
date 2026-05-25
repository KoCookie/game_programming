using UnityEngine;

public enum LegendItemType
{
    Player,
    Obstacle,
    Key,
    Goal,
    Portal,
    Heart
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Afterimage/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelNumber;

    public int width = 7;
    public int height = 5;

    public Vector2Int playerStart;
    public Vector2Int keyPosition;
    public Vector2Int goalPosition;

    public Vector2Int[] obstaclePositions;
    public Vector2Int[] heartPositions;

    public bool hasPortal = false;
    public Vector2Int portalA;
    public Vector2Int portalB;

    public LegendItemType[] legendItems;

    public float observationTime = 5f;
    public int lives = 3;
    public int viewMapUses = 1;
    public float viewMapDuration = 3f;
}