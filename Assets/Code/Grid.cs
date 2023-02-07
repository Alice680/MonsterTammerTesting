using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private class Node
    {
        public int x_position, y_position;

        public TileType tile_type;

        public GameObject tile_object;

        public GameObject tile_marker;

        public Entity entity;

        public Node(int x, int y, TileType type, GameObject obj)
        {
            x_position = x;
            y_position = y;

            tile_type = type;
            tile_object = obj;

            tile_object.transform.position = new Vector2(x + 0.5f, y + 0.5f);

            entity = null;

            Debug.DrawLine(new Vector2(x, y), new Vector2(x + 1, y), Color.black, Mathf.Infinity, false);
            Debug.DrawLine(new Vector2(x, y), new Vector2(x, y + 1), Color.black, Mathf.Infinity, false);
        }

        public void SetMarker(GameObject tile)
        {
            ClearMarker();

            tile_marker = tile;

            tile_marker.transform.position = new Vector2(x_position + 0.5f, y_position + 0.5f);
        }

        public void ClearMarker()
        {
            GameObject.Destroy(tile_marker);
            tile_marker = null;
        }
    }

    private TileHolder tile_refrence;

    private int x_size, y_size;
    private Node[,] nodes;

    private Camera cam;

    public Grid(int x, int y, Camera temp_cam)
    {
        x_size = x;
        y_size = y;

        cam = temp_cam;

        /* */
        cam.transform.position = new Vector3(8.5f, 8.5f, -10f);
        /* */

        nodes = new Node[x_size, y_size];

        tile_refrence = (TileHolder)Resources.Load("DataHolders/TileHolder");

        for (int i = 0; i < x_size; ++i)
            for (int e = 0; e < y_size; ++e)
            {
                if (i == 0 || e == 0 || i == x - 1 || e == y - 1)
                    nodes[i, e] = new Node(i, e, TileType.Wall, tile_refrence.GetTile(TileType.Wall, 0));
                else
                    nodes[i, e] = new Node(i, e, TileType.Ground, tile_refrence.GetTile(TileType.Ground, 0));
            }
    }

    //Grid Data
    public Vector2Int GetGridSize()
    {
        return new Vector2Int(x_size, y_size);
    }

    //Entity stuff
    public void MoveEntity(int x, int y, Entity ent)
    {
        if (!IsValidPosition(x, y) || ent == null)
            return;

        RemoveEntity(ent);
        RemoveEntity(x, y);

        ent.SetPosition(x, y);
        nodes[x, y].entity = ent;
    }

    public void RemoveEntity(Entity ent)
    {
        if (ent == null)
            return;

        for (int i = 0; i < x_size; ++i)
            for (int e = 0; e < y_size; ++e)
                if (ent.Compare(nodes[i, e].entity))
                {
                    nodes[i, e].entity = null;
                    return;
                }
    }

    public void RemoveEntity(int x, int y)
    {
        if (!IsValidPosition(x, y))
            return;

        nodes[x, y].entity = null;
    }

    public Vector2Int GetEntityPosition(Entity ent)
    {
        if (ent == null)
            return new Vector2Int(-1, -1);

        for (int i = 0; i < x_size; ++i)
            for (int e = 0; e < y_size; ++e)
                if (ent.Compare(nodes[i, e].entity))
                    return new Vector2Int(i, e);

        return new Vector2Int(-1, -1);
    }

    public Entity GetEntityAtPosition(int x, int y)
    {
        if (!IsValidPosition(x, y))
            return null;

        return nodes[x, y].entity;
    }

    public bool CheckEntityEnter(int x, int y, Entity ent)
    {
        if (!IsValidPosition(x, y) || ent == null || GetEntityAtPosition(x, y) != null)
            return false;

        return nodes[x, y].tile_type == TileType.Ground;
    }

    //Tile Markers
    public void SetTileMarker(int x, int y, int index)
    {
        nodes[x, y].SetMarker(tile_refrence.GetTile(TileType.Marker, index));
    }

    public void ClearTileMarker(int x, int y)
    {
        nodes[x, y].ClearMarker();
    }

    public void ClearTileMarkers()
    {
        for (int i = 0; i < x_size; ++i)
            for (int e = 0; e < y_size; ++e)
                ClearTileMarker(i, e);
    }

    //TBD
    public bool IsValidPosition(int x, int y)
    {
        return x >= 0 && y >= 0 && x < x_size && y < y_size;
    }
}