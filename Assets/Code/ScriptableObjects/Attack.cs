using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "InGameStuff/Attack", order = 3)]
public class Attack : ScriptableObject
{
    [SerializeField] private int power;
    [SerializeField] private MapShape target_area;
    [SerializeField] private MapShape effect_area;

    public Vector2Int[] GetTargetArea(Vector2Int start,Direction direction, Vector2Int grid_size)
    {
        return target_area.GetPoints(start, direction, grid_size.x, grid_size.y);
    }
}