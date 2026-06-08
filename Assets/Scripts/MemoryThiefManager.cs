using System.Collections;
using UnityEngine;

public class MemoryThiefManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameManager gameManager;

    private Coroutine moveRoutine;

    public void ActivateMemoryThief()
    {
        if (moveRoutine != null)
        {
            StopCoroutine(moveRoutine);
        }

        LevelData level = levelLoader.GetCurrentLevel();

        if (level == null || level.memoryThief == null || !level.memoryThief.enabled)
            return;

        if (levelLoader.spawnedMemoryThief == null)
            return;

        moveRoutine = StartCoroutine(MoveToKeyRoutine(level));
    }

    IEnumerator MoveToKeyRoutine(LevelData level)
    {
        GameObject thief = levelLoader.spawnedMemoryThief;
        ShowVisual(thief);

        Vector2Int[] path = level.memoryThief.pathToKey;

        if (path != null)
        {
            foreach (Vector2Int pathPoint in path)
            {
                if (gameManager.HasKey)
                    yield break;

                yield return MoveOneStep(thief, levelLoader.GridToWorld(pathPoint), level.memoryThief.moveInterval);
            }
        }

        bool pathEndsAtKey = path != null
            && path.Length > 0
            && path[path.Length - 1] == level.keyPosition;

        if (!gameManager.HasKey && !pathEndsAtKey)
        {
            yield return MoveOneStep(thief, levelLoader.GridToWorld(level.keyPosition), level.memoryThief.moveInterval);
        }

        if (!gameManager.HasKey)
        {
            gameManager.MemoryThiefAteKey();
        }
    }

    IEnumerator MoveOneStep(GameObject thief, Vector3 targetPosition, float duration)
    {
        duration = Mathf.Max(0.05f, duration);

        Vector3 startPosition = thief.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            thief.transform.position = Vector3.Lerp(startPosition, targetPosition, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        thief.transform.position = targetPosition;
    }

    void ShowVisual(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            sr.enabled = true;
        }
    }
}
