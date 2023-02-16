using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICore : Actor
{
    public AICore(DungeonManager mannager) : base(mannager)
    {

    }

    //temp
    private float start_time;
    //

    public override void StartTurn()
    {
        start_time = Time.time;
    }

    public override void Run()
    {
        if (Time.time - start_time > 1f)
            dm_refrence.EndTurn();
    }
}