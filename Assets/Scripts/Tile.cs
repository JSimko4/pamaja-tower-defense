using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite GatherTileSprite;
    public Sprite DefaultSprite;

    public static Tile ClickedTile;

    public Vector2Int mapCoordinates; // row, column indexes (coordinates) in LevelManager dictionary
    public int TileType { get; set; }

    public static int PathTileType { get => 1; }
    public static int EmptyTileType { get => 0; }

    
    public Tower Tower { get; private set; }
    public List<Ally> PresentAllies = new List<Ally>();

    public bool IsEmpty { get => TileType == EmptyTileType; }


    private Color32 defaultColor = new Color32(151, 214, 49, 255);
    private Color32 hoveredColor = new Color32(100, 100, 200, 255);

    //get center of tile
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x,
                               transform.position.y);
        }
    }


    public void PlaceTower(Tower tower)
    {
        ColorTile(defaultColor);
        Tower = tower;
        //reset clicked tile after placing tower
        ClickedTile = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (this == Ally.GatherTile)
        {
            // Change sprite to display gather point
            spriteRenderer.sprite = GatherTileSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if(Tower)
        {
            // TODO show tower range, tower tooltip...
            return;
        }


        // set gather point on this path tile
        if (!IsEmpty)
        {
            // Set default sprite for the old gather point tile
            if (Ally.GatherTile)
            {
                Ally.GatherTile.spriteRenderer.sprite = DefaultSprite;
            }

            // Change gather point for allies
            Ally.GatherTile = this;

            // Change sprite to display gather point
            spriteRenderer.sprite = GatherTileSprite;
        }
        

        // if click on same tile twice or on not empty tile, hide tower panel
        if(ClickedTile == this || !IsEmpty)
        {
            if(ClickedTile)
                ClickedTile.ColorTile(defaultColor);
            ClickedTile = null;

            UIManager.Instance.HideTowersPanel();
        }
        else // color clicked tile and show towers panel and set color of that tile
        {
            if (ClickedTile)
                ClickedTile.ColorTile(defaultColor);
            ClickedTile = this;

            UIManager.Instance.ShowTowersPanel();
            ColorTile(hoveredColor);
        }
    }

    // color only empty tiles
    private void ColorTile(Color newColor)
    {
        if (IsEmpty)
            spriteRenderer.color = newColor;
    }

    private void OnMouseEnter()
    {
        if (ClickedTile)
            return;


        if (EventSystem.current.IsPointerOverGameObject())
            return;

        ColorTile(hoveredColor);
    }

    private void OnMouseExit()
    {
        if (ClickedTile)
            return;

        ColorTile(defaultColor);
    }
}
