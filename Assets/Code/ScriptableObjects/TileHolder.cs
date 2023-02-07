using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileHolder", menuName = "DataHolders/Tiles", order = 1)]
public class TileHolder : ScriptableObject
{
    [SerializeField] private GameObject[] marker_tiles;
    [SerializeField] private GameObject[] ground_tiles;
    [SerializeField] private GameObject[] wall_tiles;

    public GameObject GetTile(TileType type, int index)
    {
        if (type == TileType.Marker)
            return Instantiate(marker_tiles[index]);
        else if (type == TileType.Ground)
            return Instantiate(ground_tiles[index]);
        else if (type == TileType.Wall)
            return Instantiate(wall_tiles[index]);

        return null;
    }
}