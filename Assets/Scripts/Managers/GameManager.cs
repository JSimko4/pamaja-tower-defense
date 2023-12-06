using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private int gold;
    [SerializeField]
    private int lives;
    [SerializeField]
    private int mana;

    [SerializeField]
    private List<GameObject> monsterPrefabs;
    [SerializeField]
    private List<GameObject> allyPrefabs;

    public List<Monster> activeMonsters = new List<Monster>();

    private int totalLives;
    public int Gold { get { return gold; } set { gold = value; } }
    public int Lives { get { return lives; } set { lives = value; } }
    public int Mana { get { return mana; } set { mana = value; } }
    public bool GameLost { get => Lives <= 0; }

    public List<Wave> waves;
    private int currentWave = 0;
    private int waveSpawns;
    private int waveSpawnsFinished;
    private bool waveStarted;
    private int waveManaReward;

    private int TotalWaves { get => waves.Count; }
    private bool Spawning { get => waveSpawns != waveSpawnsFinished; }
    public bool WaveActive { get => activeMonsters.Count > 0 || Spawning; }


    // Start is called before the first frame update
    void Start()
    {
        totalLives = lives;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameLost)
        {
            Debug.Log("TODO: Game lost - show restart screen");
        }
        // Finished wave
        else if (!WaveActive && waveStarted)
        {
            if (currentWave == TotalWaves) // If it was last wave
            {
                Debug.Log("TODO: All waves done - back to main menu or something like that");
            }
            else // Give mana reward for finishing wave it isnt the last wave
            {
                UIManager.Instance.ShowNextWaveButton();
                Mana += waveManaReward;
            }
            waveStarted = false;
        }

        UpdateUiValues();
    }

    public void UpdateUiValues()
    {   
        UIManager.Instance.SetLives(Lives, totalLives);
        UIManager.Instance.SetGold(Gold);
        UIManager.Instance.SetMana(Mana);
    }
    public void BuyTower(Tower tower)
    {
        gold -= tower.Price;
        tower.Place();
    }

    public void StartWave()
    {
        // Get wave spawns
        List<Spawn> spawns = waves[currentWave].Spawns;

        // Get wave mana reward
        waveManaReward = waves[currentWave].manaReward;

        // Update UI
        UIManager.Instance.HideNextWaveButton();
        UIManager.Instance.SetWave(++currentWave, TotalWaves);

        // Start multiple threads because monsters can come from multiple spawns in one wave
        waveStarted = true;
        waveSpawns = spawns.Count;
        waveSpawnsFinished = 0;
        for (int i = 0; i < spawns.Count; i++)
        {
            StartCoroutine(Spawn(spawns[i].monsterIndex, spawns[i].startTilePositionIndex, spawns[i].delayBeforeSpawnInSec));
        }
    }

    private IEnumerator Spawn(int monsterIndex, int startTileIndex, float delayBeforeSpawnInSec)
    {
        if (delayBeforeSpawnInSec > 0)
        {
            yield return new WaitForSeconds(delayBeforeSpawnInSec);
        }

        waveSpawnsFinished++;
        yield return SpawnMonster(startTileIndex, monsterIndex);
    }

    private IEnumerator SpawnMonster(int startTileIndex, int monsterId)
    {
        Monster monster = Instantiate(
            monsterPrefabs[monsterId],
            LevelManager.Instance.getStartTile(startTileIndex).WorldPosition,
            Quaternion.identity
        ).GetComponent<Monster>();

        monster.startTile = LevelManager.Instance.getStartTile(startTileIndex);
        activeMonsters.Add(monster);

        yield return new WaitForSeconds(0.2f);
    }

    public void DecreaseLives(int value) {
        lives -= value;
    }

}
