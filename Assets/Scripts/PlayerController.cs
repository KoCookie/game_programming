using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool canMove = false;
    public float tileSize = 1f;
    public Transform gridRoot;
    private GameManager gameManager;

    private List<Vector3> tileCenters = new List<Vector3>();
    private Vector3 currentTileCenter;

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
        Vector3 targetPosition = currentTileCenter + direction * tileSize;
        Vector3 closestTile = FindClosestTile(targetPosition);

        if (Vector3.Distance(targetPosition, closestTile) < 0.15f)
        {
            currentTileCenter = closestTile;
            transform.position = currentTileCenter;

            CheckTileObject();
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
            if (hit.CompareTag("Obstacle"))
            {
                gameManager.LoseLife();
            }

            else if (hit.CompareTag("Key"))
            {
                gameManager.GetKey();

                hit.gameObject.SetActive(false);
            }

            else if (hit.CompareTag("Goal"))
            {
                gameManager.ReachGoal();
            }
        }
    }
    public void InitializeGrid(Transform newGridRoot)
    {
        gridRoot = newGridRoot;
        tileCenters.Clear();

        foreach (Transform tile in gridRoot)
        {
            tileCenters.Add(tile.position);
        }

        currentTileCenter = FindClosestTile(transform.position);
        transform.position = currentTileCenter;
    }
}