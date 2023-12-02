using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryScreenLogic : MonoBehaviour
{
    // Start is called before the first frame update

    public void LoagGame()
    {
        SceneManager.LoadScene("InLevelScene");
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
