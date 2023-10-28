using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject towersPanel;

    public GameObject TowersPanel { get => towersPanel; }

    // Start is called before the first frame update
    void Start()
    {
        HideTowersPanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowTowersPanel() { 
        towersPanel.SetActive(true);
    }

    public void HideTowersPanel() {
        towersPanel.SetActive(false);    
    }
}
