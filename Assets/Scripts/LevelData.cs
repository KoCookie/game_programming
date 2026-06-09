using UnityEngine;

public enum LegendItemType
{
    Player,
    Obstacle,
    Key,
    Goal,
    Portal,
    Heart,
    MemoryThief
}

public enum MirrorMode
{
    Horizontal,
    Vertical
}

[System.Serializable]
public class SpawnTrapData
{
    public Vector2Int appearPosition;
}

[System.Serializable]
public class MoveTrapData
{
    public Vector2Int startPosition;
    public Vector2Int endPosition;
}

[System.Serializable]
public class MemoryThiefData
{
    public bool enabled = false;
    public Vector2Int startPosition;
    public Vector2Int[] pathToKey;
    public float moveInterval = 1.2f;
}

[CreateAssetMenu(fileName = "LevelData", menuName = "Afterimage/Level Data")]
public class LevelData : ScriptableObject
{
    public int levelNumber;

    [Header("Intro")]
    public bool showIntro = false;
    public string introTitle;
    [TextArea(3, 8)]
    public string introBody;

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

    [Header("Mirror Mechanic")]
    public bool mirrorAtEndOfObservation = false;
    public MirrorMode mirrorMode = MirrorMode.Horizontal;
    public float mirrorAnimationDuration = 1f;

    public SpawnTrapData[] spawnTraps;
    public MoveTrapData[] moveTraps;

    public MemoryThiefData memoryThief;
}
