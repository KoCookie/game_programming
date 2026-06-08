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

    public void LoadLevel3()
    {
        LevelManager.selectedLevel = 3;
        SceneManager.LoadScene("GameScene");
    }
    public void LoadLevel4()
    {
        LevelManager.selectedLevel = 4;
        SceneManager.LoadScene("GameScene");
    }

    public void LoadLevel5()
    {
        LevelManager.selectedLevel = 5;
        SceneManager.LoadScene("GameScene");
    }
    public void LoadLevel6()
    {
        LevelManager.selectedLevel = 6;
        SceneManager.LoadScene("GameScene");
    }
}