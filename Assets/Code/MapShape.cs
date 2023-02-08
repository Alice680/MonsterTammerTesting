using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MapShape
{
    [SerializeField] private int XAxis, YAxis;

    [SerializeField] private Shape shape;

    [SerializeField] private bool hollow;

    public Vector2Int[] GetPoints(Vector2Int start, Direction direction, int max_x, int max_y)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        switch (shape)
        {
            case Shape.Blast:
                break;
            case Shape.Cone:
                break;
            case Shape.Cross:
                list = GetCrossPoints(start, direction);
                break;
            case Shape.Line:
                break;
            case Shape.Rectangle:
                list = GetRectanglePoints(start, direction);
                break;
        }

        return RemoveInvalidPoints(list, max_x, max_y);
    }

    //Remove points outside the grid be removing any point less then 0 on any axis or greater then it's max on a given axis. Before converting to an array to return.
    private Vector2Int[] RemoveInvalidPoints(List<Vector2Int> list, int max_x, int max_y)
    {
        List<Vector2Int> safe_locations = new List<Vector2Int>();

        foreach (Vector2Int point in list)
            if (point.x >= 0 && point.y >= 0 && point.x < max_x && point.y < max_y)
                safe_locations.Add(point);

        return safe_locations.ToArray();
    }

    ///Return points based on the shape

    //
    private List<Vector2Int> GetBlastPoints(Vector2Int start, Direction direction)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        return list;
    }

    private List<Vector2Int> GetConePoints(Vector2Int start, Direction direction)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        return list;
    }

    private List<Vector2Int> GetCrossPoints(Vector2Int start, Direction direction)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        for (int i = -XAxis; i <= XAxis; ++i)
            if (!hollow || i != 0)
                list.Add(new Vector2Int(i, 0) + start);

        for (int i = -YAxis; i <= YAxis; ++i)
            if (!hollow || i != 0)
                list.Add(new Vector2Int(0, i) + start);

        return list;
    }

    private List<Vector2Int> GetLinePoints(Vector2Int start, Direction direction)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        return list;
    }

    private List<Vector2Int> GetRectanglePoints(Vector2Int start, Direction direction)
    {
        List<Vector2Int> list = new List<Vector2Int>();

        for (int i = -XAxis; i <= XAxis; ++i)
            for (int e = -YAxis; e <= YAxis; ++e)
                if (!hollow || i != 0 || e != 0)
                    list.Add(new Vector2Int(i, e) + start);

        return list;
    }
}