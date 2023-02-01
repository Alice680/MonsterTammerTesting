using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    private static int previous_id;

    private int id;

    private int hp, atk, spd, mov;

    private Actor actor;

    private GameObject model;

    public Entity(GameObject obj, Actor act, int[] stats)
    {
        id = ++previous_id;

        model = obj;

        actor = act;

        if(stats.Length == 4)
        {
            hp = stats[0];
            atk = stats[1];
            spd = stats[2];
            mov = stats[3];
        }
    }

    public void SetPosition(int x, int y)
    {
        model.transform.position = new Vector2(x + 0.5F, y + 0.5F);
    }

    //ID
    public int GetID()
    {
        return id;
    }

    public bool Compare(Entity other)
    {
        if (other == null)
            return false;

        return id == other.GetID();
    }

    //Data retrival
    public Actor GetActor()
    {
        return actor;
    }

    //Stats
    public int GetSpeed()
    {
        return spd;
    }

    public int GetMoves()
    {
        return mov;
    }

    //Overide
    public override string ToString()
    {
        return model.name;
    }
}