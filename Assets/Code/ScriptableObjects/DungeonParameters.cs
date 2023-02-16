using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dungeon", menuName = "InGameStuff/Dungeon", order = 1)]
public class DungeonParameters : ScriptableObject
{
    [Serializable] private class Floor
    {

    }

    [SerializeField] private Floor[] floors;
}