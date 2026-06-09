using System.Collections;
using UnityEngine;

public class GateBlockerManager : MonoBehaviour
{
    public LevelLoader levelLoader;
    public GameManager gameManager;

    private LevelData level;
    private GameObject blocker;
    private int pathIndex;
    private int pendingSteps;
    private bool active;
    private bool moving;

    public void ActivateGateBlocker()
    {
        level = levelLoader.GetCurrentLevel();
        blocker = levelLoader.spawnedGateBlocker;
        pathIndex = 0;
        pendingSteps = 0;
        active = level != null
            && level.gateBlocker != null
            && level.gateBlocker.enabled
            && blocker != null;
        moving = false;

        if (active)
            ShowVisual();
    }

    public void AdvanceOneStep()
    {
        if (!active)
            return;

        ShowVisual();
        pendingSteps++;

        if (!moving)
            StartCoroutine(ProcessSteps());
    }

    IEnumerator ProcessSteps()
    {
        moving = true;

        while (pendingSteps > 0)
        {
            pendingSteps--;

            if (!HasNextStep())
                break;

            Vector3 targetPosition = levelLoader.GridToWorld(level.gateBlocker.pathToGoal[pathIndex]);
            pathIndex++;

            yield return MoveOneStep(targetPosition);

            if (Vector3.Distance(blocker.transform.position, levelLoader.GridToWorld(level.goalPosition)) < 0.1f)
            {
                gameManager.GateBlockerReachedGoal();
                active = false;
                break;
            }
        }

        moving = false;
    }

    bool HasNextStep()
    {
        if (level.gateBlocker.pathToGoal == null || pathIndex >= level.gateBlocker.pathToGoal.Length)
            return false;

        return true;
    }

    IEnumerator MoveOneStep(Vector3 targetPosition)
    {
        Vector3 startPosition = blocker.transform.position;
        float duration = Mathf.Max(0.01f, level.gateBlocker.moveDuration);
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            blocker.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        blocker.transform.position = targetPosition;
    }

    void ShowVisual()
    {
        SpriteRenderer sr = blocker.GetComponent<SpriteRenderer>();

        if (sr != null)
            sr.enabled = true;
    }
}
