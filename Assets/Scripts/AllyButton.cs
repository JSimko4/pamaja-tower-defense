using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class AllyButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject allyPrefab;
    [SerializeField]
    private TextMeshProUGUI hoverText;
    public GameObject AllyPrefab
    {
        get
        {
            return allyPrefab;
        }
    }
    private GameObject prefabInstance;

    private static Vector3 SpawnPosition {
        get {
            return new Vector3(LevelManager.Instance.End.transform.position.x, LevelManager.Instance.End.transform.position.y, -1);
        }
    }

    void Start()
    {
        prefabInstance = Instantiate(allyPrefab);
        prefabInstance.SetActive(false);
        Ally ally = prefabInstance.GetComponentInChildren<Ally>();
        hoverText.text = $"{ally.Price} g.";
        if (hoverText != null)
            hoverText.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        spawnAlly(allyPrefab);
    }

    public static void spawnAlly(GameObject prefab)
    {
        GameObject prefabInstance = Instantiate(prefab);
        Ally ally = prefabInstance.GetComponentInChildren<Ally>();

        // not enough gold
        if (GameManager.Instance.Gold < ally.Price)
        {
            Destroy(prefabInstance);
            return;
        }

        GameManager.Instance.Gold -= ally.Price;
        ally.startTile = LevelManager.Instance.End;
        ally.destinationTile = Ally.GatherTile;
        prefabInstance.transform.position = SpawnPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverText != null)
            hoverText.enabled = true; // Show the hover text
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverText != null)
            hoverText.enabled = false; // Hide the hover text
    }

}
