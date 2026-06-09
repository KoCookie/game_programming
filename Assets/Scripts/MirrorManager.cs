using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public LevelLoader levelLoader;

    public IEnumerator PlayMirrorShift()
    {
        if (levelLoader == null)
            yield break;

        LevelData level = levelLoader.GetCurrentLevel();

        if (level == null || !level.mirrorAtEndOfObservation)
            yield break;

        List<Transform> objectsToMirror = GetMirrorableObjects();
        Vector3[] startPositions = new Vector3[objectsToMirror.Count];
        Vector3[] targetPositions = new Vector3[objectsToMirror.Count];

        for (int i = 0; i < objectsToMirror.Count; i++)
        {
            startPositions[i] = objectsToMirror[i].position;
            targetPositions[i] = levelLoader.MirrorWorldPosition(startPositions[i]);
        }

        float duration = Mathf.Max(0.05f, level.mirrorAnimationDuration);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / duration);

            for (int i = 0; i < objectsToMirror.Count; i++)
            {
                if (objectsToMirror[i] != null)
                    objectsToMirror[i].position = Vector3.Lerp(startPositions[i], targetPositions[i], t);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        for (int i = 0; i < objectsToMirror.Count; i++)
        {
            if (objectsToMirror[i] != null)
                objectsToMirror[i].position = targetPositions[i];
        }

        levelLoader.SetMirrorApplied(true);
    }

    List<Transform> GetMirrorableObjects()
    {
        List<Transform> results = new List<Transform>();

        foreach (Transform child in levelLoader.mapObjectsRoot)
        {
            if (child.CompareTag("Obstacle")
                || child.CompareTag("Key")
                || child.CompareTag("Goal")
                || child.CompareTag("Heart")
                || child.CompareTag("Portal")
                || child.CompareTag("MemoryThief"))
            {
                results.Add(child);
            }
        }

        return results;
    }
}
