using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = false;
    public float tileSize = 1f;
    public Transform gridRoot;
    public float objectCheckDistance = 0.15f;
    private GameManager gameManager;

    private List<Vector3> tileCenters = new List<Vector3>();
    private Dictionary<Vector3, SpriteRenderer> tileRenderers = new Dictionary<Vector3, SpriteRenderer>();
    private HashSet<Vector3> disappearedTileCenters = new HashSet<Vector3>();
    private Vector3 currentTileCenter;
    private bool disappearingTilesEnabled;
    private float disappearedTileAlpha = 0.18f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

    }

    void Update()
    {
        if (!canMove || tileCenters.Count == 0) return;

        Vector3 direction = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            direction = Vector3.up;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            direction = Vector3.down;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            direction = Vector3.left;
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            direction = Vector3.right;

        if (direction != Vector3.zero)
            TryMove(direction);
    }

    void TryMove(Vector3 direction)
    {
        Vector3 previousTileCenter = currentTileCenter;
        Vector3 targetPosition = currentTileCenter + direction * tileSize;
        Vector3 closestTile = FindClosestTile(targetPosition);

        if (Vector3.Distance(targetPosition, closestTile) < 0.15f && !disappearedTileCenters.Contains(closestTile))
        {
            currentTileCenter = closestTile;
            transform.position = currentTileCenter;

            CheckTileObject();

            if (disappearingTilesEnabled)
                DisappearTile(previousTileCenter);

            if (gameManager != null)
                gameManager.OnPlayerMovedOneStep();
        }
    }

    Vector3 FindClosestTile(Vector3 position)
    {
        Vector3 closest = tileCenters[0];
        float shortestDistance = Vector3.Distance(position, closest);

        foreach (Vector3 tileCenter in tileCenters)
        {
            float distance = Vector3.Distance(position, tileCenter);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = tileCenter;
            }
        }

        return closest;
    }

    public void EnableMovement()
    {
        if (tileCenters.Count == 0 && gridRoot != null)
        {
            InitializeGrid(gridRoot);
        }

        canMove = true;
    }

    public void DisableMovement()
    {
        canMove = false;
    }

    void CheckTileObject()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.2f);

        foreach (Collider2D hit in hits)
        {
            if (Vector3.Distance(hit.transform.position, transform.position) > objectCheckDistance)
                continue;

            if (hit.CompareTag("Obstacle"))
            {
                gameManager.LoseLife();
                return;
            }

            else if (hit.CompareTag("Key"))
            {
                gameManager.GetKey();

                hit.gameObject.SetActive(false);
                return;
            }

            else if (hit.CompareTag("Heart"))
            {
                gameManager.GainLife();
                hit.gameObject.SetActive(false);
                return;
            }

            else if (hit.CompareTag("Portal"))
            {
                gameManager.UsePortal(transform.position);
                return;
            }

            else if (hit.CompareTag("Goal"))
            {
                gameManager.ReachGoal();
                return;
            }
        }
    }
    public void InitializeGrid(Transform newGridRoot)
    {
        gridRoot = newGridRoot;
        tileCenters.Clear();
        tileRenderers.Clear();
        disappearedTileCenters.Clear();

        foreach (Transform tile in gridRoot)
        {
            tileCenters.Add(tile.position);

            SpriteRenderer tileRenderer = tile.GetComponent<SpriteRenderer>();
            if (tileRenderer != null)
            {
                tileRenderer.color = new Color(
                    tileRenderer.color.r,
                    tileRenderer.color.g,
                    tileRenderer.color.b,
                    1f
                );
                tileRenderers[tile.position] = tileRenderer;
            }
        }

        currentTileCenter = FindClosestTile(transform.position);
        transform.position = currentTileCenter;
    }

    public void ConfigureDisappearingTiles(bool enabled, float fadedAlpha)
    {
        disappearingTilesEnabled = enabled;
        disappearedTileAlpha = Mathf.Clamp01(fadedAlpha);
    }

    void DisappearTile(Vector3 tileCenter)
    {
        if (disappearedTileCenters.Contains(tileCenter))
            return;

        disappearedTileCenters.Add(tileCenter);

        if (tileRenderers.TryGetValue(tileCenter, out SpriteRenderer tileRenderer))
        {
            Color color = tileRenderer.color;
            color.a = disappearedTileAlpha;
            tileRenderer.color = color;
        }
    }

    public void TeleportTo(Vector3 targetPosition)
    {
        Vector3 closestTile = FindClosestTile(targetPosition);

        if (disappearingTilesEnabled && disappearedTileCenters.Contains(closestTile))
            return;

        currentTileCenter = closestTile;
        transform.position = currentTileCenter;
    }
}
