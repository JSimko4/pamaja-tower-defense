using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
   public void LoadLevelScene(string level)
    {
        // In the LevelManager we load the level from the value set here
        PlayerPrefs.SetString("level", level);

        // Load level scene
        SceneManager.LoadScene("InLevelScene");
    }
    public void ResetLevelScene()
    {
        // In the LevelManager we load the level from the value set here
        PlayerPrefs.SetString("level", LevelManager.Instance.levelLoaded);
        // Load level scene
        SceneManager.LoadScene("InLevelScene");
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("EntryScreen");
    }
    public void LoadTownScene()
    {
        SceneManager.LoadScene("TownScreen");
    }
    public void LoadlevelsScene()
    {
        SceneManager.LoadScene("LevelsScreen");
    }
}
    