using System.Collections;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameObject trapObstaclePrefab;

    public float blinkInterval = 0.2f;
    public int blinkTimes = 3;
    public float moveDuration = 0.6f;
    public float stayAtEndDuration = 1f;

    public void ActivateTraps()
    {
        if (levelLoader == null)
            return;

        LevelData level = levelLoader.GetCurrentLevel();

        if (level == null)
            return;

        bool hasSpawnTraps = level.spawnTraps != null && level.spawnTraps.Length > 0;
        bool hasMoveTraps = level.moveTraps != null && level.moveTraps.Length > 0;

        if (!hasSpawnTraps && !hasMoveTraps)
            return;

        StartCoroutine(ActivateTrapRoutine());
    }

    IEnumerator ActivateTrapRoutine()
    {
        LevelData level = levelLoader.GetCurrentLevel();

        if (level.spawnTraps != null)
        {
            foreach (SpawnTrapData trap in level.spawnTraps)
            {
                GameObject obj = Instantiate(
                    trapObstaclePrefab,
                    levelLoader.GridToWorld(trap.appearPosition),
                    Quaternion.identity,
                    levelLoader.mapObjectsRoot
                );

                yield return StartCoroutine(BlinkObject(obj));

                HideVisualOnly(obj);
            }
        }

        if (level.moveTraps != null)
        {
            foreach (MoveTrapData trap in level.moveTraps)
            {
                GameObject obj = FindObjectAtPosition(levelLoader.GridToWorld(trap.startPosition));

                if (obj != null)
                {
                    Vector3 startPos = levelLoader.GridToWorld(trap.startPosition);
                    Vector3 endPos = levelLoader.GridToWorld(trap.endPosition);

                    // 1. 原位置闪烁 3 次
                    yield return StartCoroutine(BlinkObject(obj));

                    // 2. 确保它是显示状态
                    ShowVisualOnly(obj);

                    // 3. 平滑移动到目标格
                    yield return StartCoroutine(MoveObject(obj, startPos, endPos));

                    // 4. 到达终点后停留 1 秒
                    yield return new WaitForSeconds(stayAtEndDuration);

                    // 5. 只隐藏外观，保留 Collider
                    HideVisualOnly(obj);
                }
            }
        }
    }

    IEnumerator BlinkObject(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        if (sr == null)
            yield break;

        for (int i = 0; i < blinkTimes; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(blinkInterval);

            sr.enabled = true;
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    GameObject FindObjectAtPosition(Vector3 position)
    {
        foreach (Transform child in levelLoader.mapObjectsRoot)
        {
            if (Vector3.Distance(child.position, position) < 0.1f)
            {
                return child.gameObject;
            }
        }

        return null;
    }
    void HideVisualOnly(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            sr.enabled = false;
        }
    }
    IEnumerator MoveObject(GameObject obj, Vector3 startPos, Vector3 endPos)
    {
        float elapsedTime = 0f;

        obj.transform.position = startPos;

        while (elapsedTime < moveDuration)
        {
            float t = elapsedTime / moveDuration;
            obj.transform.position = Vector3.Lerp(startPos, endPos, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        obj.transform.position = endPos;
    }
    void ShowVisualOnly(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            sr.enabled = true;
        }
    }
}