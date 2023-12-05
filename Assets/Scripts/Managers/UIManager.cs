using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject towersPanel;

    [SerializeField]
    private GameObject alliesPanel;

    public GameObject TowersPanel { get => towersPanel; }
    public GameObject AlliesPanel { get => alliesPanel; }

    [SerializeField]
    private GameObject nextWaveButton;

    [SerializeField]
    private TextMeshProUGUI waveText;

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

    public void ShowNextWaveButton()
    {
        nextWaveButton.SetActive(true);
    }

    public void HideNextWaveButton()
    {
        nextWaveButton.SetActive(false);
    }

    public void SetWave(int currentWave, int totalWaves)
    {
        waveText.text = currentWave + " / " + totalWaves;
    }
}
