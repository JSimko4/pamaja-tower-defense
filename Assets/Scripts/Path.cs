using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Path
{
    private int currentTileIndex;
    private List<Tile> tiles;

    public Path(List<Tile> pathTiles)
    {
        tiles = pathTiles;
        currentTileIndex = 0;
    }



    public Tile GetNextTile()
    {
        if (currentTileIndex < tiles.Count)
            return tiles[currentTileIndex++];

        return null;
    }
}
