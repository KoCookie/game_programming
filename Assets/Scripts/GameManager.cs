using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject grid;
    public GameObject mapObjects;
    public TMP_Text timerText;

    public float observationTime = 5f;

    void Start()
    {
        StartCoroutine(ObservationPhase());
    }

    IEnumerator ObservationPhase()
    {
        float timeLeft = observationTime;

        while (timeLeft > 0)
        {
            timerText.text = Mathf.CeilToInt(timeLeft).ToString();
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        timerText.text = "0";

        mapObjects.SetActive(false);
        SetGridColor(Color.white);

        Debug.Log("Hidden Phase Started");
    }

    void SetGridColor(Color color)
    {
        SpriteRenderer[] tiles = grid.GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer tile in tiles)
        {
            tile.color = color;
        }
    }
}