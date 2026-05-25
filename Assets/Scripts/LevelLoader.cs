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

    [Header("Scene Roots")]
    public Transform gridRoot;
    public Transform mapObjectsRoot;
    public GameObject playerPrefab;
    public PlayerController spawnedPlayer;

    private LevelData currentLevel;

    public LevelData LoadCurrentLevel()
    {
        currentLevel = levels[LevelManager.selectedLevel - 1];

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
                Vector3 position = new Vector3(offsetX + x, offsetY + y, 0);
                Instantiate(tilePrefab, position, Quaternion.identity, gridRoot);
            }
        }
    }

    void GenerateObjects()
    {
        foreach (Vector2Int obstaclePosition in currentLevel.obstaclePositions)
        {
            Instantiate(obstaclePrefab, GridToWorld(obstaclePosition), Quaternion.identity, mapObjectsRoot);
        }

        foreach (Vector2Int heartPosition in currentLevel.heartPositions)
        {
            Instantiate(heartPrefab, GridToWorld(heartPosition), Quaternion.identity, mapObjectsRoot);
        }

        if (currentLevel.hasPortal)
        {
            Instantiate(portalPrefab, GridToWorld(currentLevel.portalA), Quaternion.identity, mapObjectsRoot);
            Instantiate(portalPrefab, GridToWorld(currentLevel.portalB), Quaternion.identity, mapObjectsRoot);
        }

        Instantiate(keyPrefab, GridToWorld(currentLevel.keyPosition), Quaternion.identity, mapObjectsRoot);
        Instantiate(goalPrefab, GridToWorld(currentLevel.goalPosition), Quaternion.identity, mapObjectsRoot);
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
        float offsetX = -(currentLevel.width - 1) / 2f;
        float offsetY = -(currentLevel.height - 1) / 2f;

        return new Vector3(offsetX + gridPosition.x, offsetY + gridPosition.y, 0);
    }

    public LevelData GetCurrentLevel()
    {
        return currentLevel;
    }
}