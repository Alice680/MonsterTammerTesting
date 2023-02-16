using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor
{
    protected DungeonManager dm_refrence;

    public Actor(DungeonManager manager)
    {
        dm_refrence = manager;
    }

    public virtual void StartTurn()
    {

    }

    public virtual void Run()
    {

    }
}