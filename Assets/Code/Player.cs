using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{

    public Player(DungeonManager manager) : base(manager)
    {

    }

    public override void Run()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            dm_refrence.EndTurn();
        else
        {
            Vector2Int vec = new Vector2Int(0, 0);

            if (Input.GetKeyDown(KeyCode.D))
            {
                vec.x = 1;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                vec.x = -1;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                vec.y = 1;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                vec.y = -1;
            }

            dm_refrence.TryToMove(vec.x, vec.y);
        }
    }
}