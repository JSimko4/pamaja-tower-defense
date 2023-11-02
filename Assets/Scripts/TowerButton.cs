using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IPointerClickHandler, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField]
    private GameObject towerPrefab;

    public GameObject TowerPrefab
    {
        get
        {
            return towerPrefab;
        }
    }


    private GameObject prefabInstance;

    private Vector3 SpawnPosition {
        get {
            return new Vector3(Tile.ClickedTile.transform.position.x, Tile.ClickedTile.transform.position.y, -1);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Tower tower = prefabInstance.GetComponentInChildren<Tower>();
        // not enough gold
        if (GameManager.Instance.Gold < tower.Price)
            return;

        GameManager.Instance.BuyTower(tower);
        UIManager.Instance.HideTowersPanel();

        prefabInstance = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!prefabInstance)
            prefabInstance = Instantiate(towerPrefab);

        prefabInstance.SetActive(true);
        prefabInstance.transform.position = SpawnPosition;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(prefabInstance)
            prefabInstance.SetActive(false);
    }
}
