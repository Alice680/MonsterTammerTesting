using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    private static int previous_id;

    private int id;

    private GameObject model;

    public Entity(GameObject obj)
    {
        id = ++previous_id;

        model = obj;
    }

    public void SetPosition(int x, int y)
    {
        model.transform.position = new Vector2(x + 0.5F, y + 0.5F);
    }

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
}