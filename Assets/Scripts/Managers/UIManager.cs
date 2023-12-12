using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField]
    private GameObject towersPanel;
    [SerializeField]
    private GameObject upgradeButton;

    [SerializeField]
    private GameObject alliesPanel;

    [SerializeField]
    private GameObject currentTowerInfo;

    public GameObject TowersPanel { get => towersPanel; }
    public GameObject AlliesPanel { get => alliesPanel; }

    [SerializeField]
    private GameObject nextWaveButton;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private GameObject loseScreen;

    [SerializeField]
    private TextMeshProUGUI waveText;

    [SerializeField]
    private TextMeshProUGUI livesText;

    [SerializeField]
    private TextMeshProUGUI goldText;

    [SerializeField]
    private TextMeshProUGUI manaText;

    [SerializeField]
    private TextMeshProUGUI currentTowerText;

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
    public void ShowUpgradeButton()
    {
        upgradeButton.SetActive(true);
    }

    public void HideUpgradeButton()
    {
        upgradeButton.SetActive(false);
    }

    public void ShowTowerInfo()
    {
        currentTowerInfo.SetActive(true);
    }

    public void HideTowerInfo()
    {
        currentTowerInfo.SetActive(false);
    }

    public void ShowNextWaveButton()
    {
        nextWaveButton.SetActive(true);
    }

    public void HideNextWaveButton()
    {
        nextWaveButton.SetActive(false);
    }
    public void ShowWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void HideWinScreen()
    {
        winScreen.SetActive(false);
    }
    public void ShowLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void HideLoseScreen()
    {
        loseScreen.SetActive(false);
    }



    public void SetWave(int currentWave, int totalWaves)
    {
        waveText.text = $"{currentWave}/{totalWaves}";
    }

    public void SetLives(int lives, int totalLives)
    {
        livesText.text = $"{lives}";
    }

    public void SetGold(int gold)
    {
        goldText.text = gold.ToString();
    }

    public void SetMana(int mana)
    {
        manaText.text = mana.ToString();
    }

    public void SetCurrentTowerText(string name, int damage, float range,float attackSpeed,bool upgraded)
    {
        if (upgraded)
        {
            HideUpgradeButton();
            currentTowerText.text = $"{name}\n \nLevel: 2\nDamage: {damage}\nRange: {range}\nAttack speed: {attackSpeed}";
        }
            
        else
        {
            ShowUpgradeButton();
            currentTowerText.text = $"{name}\n \nLevel: 1\nDamage: {damage}\nRange: {range}\nAttack speed: {attackSpeed}";
        }
            
    }

}
