using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICore : Actor
{
    public AICore(DungeonManager mannager) : base(mannager)
    {

    }

    public override void Run()
    {
        Debug.Log("End Turn");
        dm_refrence.EndTurn();
    }
}