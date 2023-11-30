using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : Singleton<PathFinding>
{

    public Path GetPath(Tile startTile, Tile endTile)
    {
        List<Tile> path = new List<Tile>();

        // The start or end tile is not a path tile, return an empty path
        if (startTile.TileType != Tile.PathTileType || endTile.TileType != Tile.PathTileType)
        {
            return new Path(path);
        }

        // Call the recursive backtracking function
        // We can replace this function with the other path finding strategy later on
        Dictionary<Vector2Int, bool> visitedTilesByCoordinates = new Dictionary<Vector2Int, bool>();
        Backtrack(startTile, endTile, visitedTilesByCoordinates, path);

        return new Path(path);
    }

    private bool Backtrack(Tile currentTile, Tile endTile, Dictionary<Vector2Int, bool> visitedTilesByCoordinates, List<Tile> path){
        // Base case: reached the end tile
        if (currentTile == endTile)
        {
            path.Add(currentTile);
            return true;
        }

        // Mark the current tile as visited
        visitedTilesByCoordinates.Add(currentTile.mapCoordinates, true);

        // Check neighbors
        foreach (Tile neighbor in GetNeighbors(currentTile))
        {
            if (neighbor.TileType == Tile.PathTileType && !visitedTilesByCoordinates.ContainsKey(neighbor.mapCoordinates))
            {
                // Recursively try to find a path from the neighbor to the end tile
                if (Backtrack(neighbor, endTile, visitedTilesByCoordinates, path))
                {
                    path.Insert(0, currentTile);
                    return true;
                }
            }
        }

        return false;
    }
  
    private List<Tile> GetNeighbors(Tile tile){
        List<Tile> neighbors = new List<Tile>();

        // Check the tile above
        AddNeighborIfExists(neighbors, tile, 0, 1);

        // Check the tile below
        AddNeighborIfExists(neighbors, tile, 0, -1);

        // Check the tile to the left
        AddNeighborIfExists(neighbors, tile, -1, 0);

        // Check the tile to the right
        AddNeighborIfExists(neighbors, tile, 1, 0);

        return neighbors;
    }

    private void AddNeighborIfExists(List<Tile> neighbors, Tile tile, int offsetX, int offsetY)
    {
        int x = tile.mapCoordinates[0] + offsetX;
        int y = tile.mapCoordinates[1] + offsetY;

        Dictionary<Vector2Int, Tile> tiles = LevelManager.Instance.Tiles;
        tiles.TryGetValue(new Vector2Int(x, y), out Tile neighbor);
        if (neighbor) neighbors.Add(neighbor);
    }
}
