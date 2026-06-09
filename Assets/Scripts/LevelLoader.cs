using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [Header("Level Data")]
    public LevelData[] levels;

    [Header("Prefabs")]
    public GameObject tilePrefab;
    public GameObject obstaclePrefab;
    public GameObject keyPrefab;
    public GameObject goalPrefab;
    public GameObject heartPrefab;
    public GameObject portalPrefab;
    public GameObject trapObstaclePrefab;
    public GameObject memoryThiefPrefab;
    public GameObject playerPrefab;

    [Header("Scene Roots")]
    public Transform gridRoot;
    public Transform mapObjectsRoot;
    public float boardVerticalOffset = -0.45f;
    public PlayerController spawnedPlayer;
    public GameObject spawnedKey;
    public GameObject spawnedMemoryThief;

    private LevelData currentLevel;
    private bool mirrorApplied = false;

    public LevelData LoadCurrentLevel()
    {
        currentLevel = levels[LevelManager.selectedLevel - 1];
        mirrorApplied = false;

        ClearLevel();
        GenerateGrid();
        GenerateObjects();
        PlacePlayer();

        return currentLevel;
    }

    void ClearLevel()
    {
        foreach (Transform child in gridRoot)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in mapObjectsRoot)
        {
            Destroy(child.gameObject);
        }
    }

    void GenerateGrid()
    {
        float offsetX = -(currentLevel.width - 1) / 2f;
        float offsetY = -(currentLevel.height - 1) / 2f;

        for (int x = 0; x < currentLevel.width; x++)
        {
            for (int y = 0; y < currentLevel.height; y++)
            {
                Vector3 position = new Vector3(offsetX + x, offsetY + y + boardVerticalOffset, 0);
                Instantiate(tilePrefab, position, Quaternion.identity, gridRoot);
            }
        }
    }

    void GenerateObjects()
    {
        if (currentLevel.obstaclePositions != null)
        {
            foreach (Vector2Int obstaclePosition in currentLevel.obstaclePositions)
            {
                Instantiate(obstaclePrefab, GridToWorld(obstaclePosition), Quaternion.identity, mapObjectsRoot);
            }
        }

        if (currentLevel.heartPositions != null)
        {
            foreach (Vector2Int heartPosition in currentLevel.heartPositions)
            {
                Instantiate(heartPrefab, GridToWorld(heartPosition), Quaternion.identity, mapObjectsRoot);
            }
        }

        if (currentLevel.moveTraps != null)
        {
            foreach (MoveTrapData trap in currentLevel.moveTraps)
            {
                Instantiate(trapObstaclePrefab, GridToWorld(trap.startPosition), Quaternion.identity, mapObjectsRoot);
            }
        }

        if (currentLevel.hasPortal)
        {
            Instantiate(portalPrefab, GridToWorld(currentLevel.portalA), Quaternion.identity, mapObjectsRoot);
            Instantiate(portalPrefab, GridToWorld(currentLevel.portalB), Quaternion.identity, mapObjectsRoot);
        }

        spawnedKey = Instantiate(keyPrefab, GridToWorld(currentLevel.keyPosition), Quaternion.identity, mapObjectsRoot);
        Instantiate(goalPrefab, GridToWorld(currentLevel.goalPosition), Quaternion.identity, mapObjectsRoot);

        if (currentLevel.memoryThief != null && currentLevel.memoryThief.enabled && memoryThiefPrefab != null)
        {
            spawnedMemoryThief = Instantiate(
                memoryThiefPrefab,
                GridToWorld(currentLevel.memoryThief.startPosition),
                Quaternion.identity,
                mapObjectsRoot
            );
        }
        else
        {
            spawnedMemoryThief = null;
        }
    }

    void PlacePlayer()
    {
        GameObject playerObject = Instantiate(
            playerPrefab,
            GridToWorld(currentLevel.playerStart),
            Quaternion.identity
        );

        spawnedPlayer = playerObject.GetComponent<PlayerController>();
        spawnedPlayer.InitializeGrid(gridRoot);
    }

    public Vector3 GridToWorld(Vector2Int gridPosition)
    {
        if (mirrorApplied)
        {
            gridPosition = MirrorGridPosition(gridPosition);
        }

        float offsetX = -(currentLevel.width - 1) / 2f;
        float offsetY = -(currentLevel.height - 1) / 2f;

        return new Vector3(offsetX + gridPosition.x, offsetY + gridPosition.y + boardVerticalOffset, 0);
    }

    public Vector2Int MirrorGridPosition(Vector2Int gridPosition)
    {
        if (currentLevel == null)
            return gridPosition;

        if (currentLevel.mirrorMode == MirrorMode.Horizontal)
            return new Vector2Int(currentLevel.width - 1 - gridPosition.x, gridPosition.y);

        return new Vector2Int(gridPosition.x, currentLevel.height - 1 - gridPosition.y);
    }

    public Vector3 MirrorWorldPosition(Vector3 position)
    {
        if (currentLevel == null)
            return position;

        if (currentLevel.mirrorMode == MirrorMode.Horizontal)
            return new Vector3(-position.x, position.y, position.z);

        return new Vector3(position.x, 2f * boardVerticalOffset - position.y, position.z);
    }

    public void SetMirrorApplied(bool applied)
    {
        mirrorApplied = applied;
    }

    public LevelData GetCurrentLevel()
    {
        return currentLevel;
    }
}
