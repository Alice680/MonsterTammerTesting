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

    public Grid(DungeonParameters parameters, Camera temp_cam)
    {
        x_size = 17;
        y_size = 17;

        cam = temp_cam;

        /* */
        cam.transform.position = new Vector3(8.5f, 8.5f, -10f);
        /* */

        nodes = new Node[x_size, y_size];

        tile_refrence = (TileHolder)Resources.Load("DataHolders/TileHolder");

        for (int i = 0; i < x_size; ++i)
            for (int e = 0; e < y_size; ++e)
            {
                if (i == 0 || e == 0 || i == x_size - 1 || e == y_size - 1)
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
    public void MoveEntity(Vector2Int position, Entity ent)
    {
        if (!IsValidPosition(position) || ent == null)
            return;

        RemoveEntity(ent);
        RemoveEntity(position);

        ent.SetPosition(position.x, position.y);
        nodes[position.x, position.y].entity = ent;
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

    public void RemoveEntity(Vector2Int position)
    {
        if (!IsValidPosition(position))
            return;

        nodes[position.x, position.y].entity = null;
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

    public Entity GetEntityAtPosition(Vector2Int position)
    {
        if (!IsValidPosition(position))
            return null;

        return nodes[position.x, position.y].entity;
    }

    public bool CheckEntityEnter(Vector2Int position, Entity ent)
    {
        if (!IsValidPosition(position) || ent == null || GetEntityAtPosition(position) != null)
            return false;

        return nodes[position.x, position.y].tile_type == TileType.Ground;
    }

    //Tile Markers
    public void SetTileMarker(Vector2Int position, int index)
    {
        nodes[position.x, position.y].SetMarker(tile_refrence.GetTile(TileType.Marker, index));
    }

    public void ClearTileMarker(Vector2Int position)
    {
        nodes[position.x, position.y].ClearMarker();
    }

    public void ClearTileMarkers()
    {
        for (int i = 0; i < x_size; ++i)
            for (int e = 0; e < y_size; ++e)
                ClearTileMarker(new Vector2Int(i, e));
    }

    //TBD
    public bool IsValidPosition(Vector2Int position)
    {
        return position.x >= 0 && position.y >= 0 && position.x < x_size && position.y < y_size;
    }
}