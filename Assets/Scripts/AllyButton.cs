using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AllyButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject allyPrefab;

    public GameObject AllyPrefab
    {
        get
        {
            return allyPrefab;
        }
    }


    private GameObject prefabInstance;

    private Vector3 SpawnPosition {
        get {
            return new Vector3(LevelManager.Instance.End.transform.position.x, LevelManager.Instance.End.transform.position.y, -1);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        prefabInstance = Instantiate(allyPrefab);
        Ally ally = prefabInstance.GetComponentInChildren<Ally>();

        // not enough gold
        if (GameManager.Instance.Gold < ally.Price)
        {
            Destroy(prefabInstance);
            return;
        }

        ally.startTile = LevelManager.Instance.End;
        ally.destinationTile = Ally.GatherTile;
        prefabInstance.transform.position = SpawnPosition;
    }
}
