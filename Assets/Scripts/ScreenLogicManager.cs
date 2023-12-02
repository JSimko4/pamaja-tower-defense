using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    // Start is called before the first frame update

    public void LoadLevelScene()
    {
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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
    