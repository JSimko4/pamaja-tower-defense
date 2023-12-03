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

    public int Gold { get { return gold; } set { gold = value; } }
    public int Lives { get { return lives; } set { lives = value; } }
    public int Mana { get { return mana; } set { mana = value; } }
    public bool GameLost { get => Lives <= 0; }


    private int currentWave = 0;
    private int totalWaves = 2;

    private int wavePaths;
    private int wavePathsFinished;
    private bool waveStarted;
    private int waveManaReward;
    private bool Spawning { get => wavePaths != wavePathsFinished; }
    public bool WaveActive { get => activeMonsters.Count > 0 || Spawning; }


    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.SetWave(currentWave, totalWaves);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Current gold: {Gold}");
        //Debug.Log($"Current mana: {Mana}");
        Debug.Log($"Current lives: {Lives}");
        Debug.Log($"Spawning: {Spawning}");
        Debug.Log($"WaveActive: {WaveActive}");

        if (GameLost)
        {
            Debug.Log("TODO: Game lost - show restart screen");
        }

        // Finished wave
        else if (!WaveActive && waveStarted) 
        {   
            if (currentWave == totalWaves)   // If it was last wave
            {
                Debug.Log("TODO: All waves done - back to main menu or something like that");
            }
            else // Give mana reward for finishing wave it isnt the last wave
            {
                Debug.Log($"Giving mana reward for finishing wave: {waveManaReward}");
                Mana += waveManaReward;
                UIManager.Instance.ShowNextWaveButton();
            }

            waveStarted = false;
        }
    }

    public void BuyTower(Tower tower)
    {
        gold -= tower.Price;
        tower.Place();
    }

    public void StartWave()
    {
        UIManager.Instance.HideNextWaveButton();

        currentWave++;
        UIManager.Instance.SetWave(currentWave, totalWaves);


        // TODO read this from file Spawns1.1.txt (one number will represent stage another wave)
        // meaning, spawn monster0, then monster1, then wait 2s, then again monster 0 from route1 & from route2 first wait 7.5s, then spawn monster1...
        string[] spawns = {
            "1,0,1,2s,0",
            "7s,1,0"
        };

        // start multiple threads because monsters can come from multiple spawns in one wave
        waveStarted = true;
        waveManaReward = 50; // TODO get from the file?
        wavePaths = spawns.Length;
        wavePathsFinished = 0;
        for (int i = 0; i < spawns.Length; i++)
        {
            StartCoroutine(SpawnWave(i, spawns[i]));
        }

    }

    private IEnumerator SpawnWave(int path_id, string spawnString)
    {
        string value = string.Empty;

        bool wait = false;


        for (int i = 0; i <= spawnString.Length; i++)
        {
            if (i == spawnString.Length || spawnString[i] == ',') // actually spawn and clear value and wait flag
            {
                if (i == spawnString.Length) //after reading the whole string spawning is completed
                    wavePathsFinished++;

                if (!wait)
                    yield return SpawnMonster(path_id, int.Parse(value));

                wait = false;
                value = string.Empty;
            }
            else if (char.IsDigit(spawnString[i]) || spawnString[i] == '.')  // read value from string
            {
                value += spawnString[i];
            }
            else if (spawnString[i] == 's')     // wait for seconds
            {
                wait = true; 
                yield return new WaitForSeconds(float.Parse(value));
            }
        }
    }

    private IEnumerator SpawnMonster(int path_id, int monster_id)
    {
        Monster monster = Instantiate(
            monsterPrefabs[monster_id],
            LevelManager.Instance.getStartTile(path_id).WorldPosition,
            Quaternion.identity
        ).GetComponent<Monster>();

        monster.startTile = LevelManager.Instance.getStartTile(path_id);
        activeMonsters.Add(monster);

        yield return new WaitForSeconds(0.2f);
    }

    public void DecreaseLives(int value) {
        lives -= value;
    }

}
