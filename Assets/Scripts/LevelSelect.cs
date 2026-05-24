using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public void LoadLevel1()
    {
        LevelManager.selectedLevel = 1;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadLevel2()
    {
        LevelManager.selectedLevel = 2;
        SceneManager.LoadScene("GameScene");
    }
}