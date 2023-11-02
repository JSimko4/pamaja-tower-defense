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

    public int Gold { get { return gold; } }
    public int Lives { get { return lives; } }
    public int Mana { get { return mana; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BuyTower(Tower tower)
    {
        gold -= tower.Price;
        tower.Place();
    }

}
