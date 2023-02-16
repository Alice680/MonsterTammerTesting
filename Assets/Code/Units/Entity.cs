using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity
{
    private static int previous_id;

    private int id;

    protected int max_hp, max_sp, max_mp, p_dmg, m_dmg, frc, p_def, m_def, res, spd, act;
    protected int hp, sp, mp;

    protected Actor actor;

    protected Vector2Int position;

    protected GameObject model;

    public Entity(GameObject obj, Actor actor)
    {
        id = ++previous_id;

        model = obj;

        this.actor = actor;
    }

    public void SetPosition(int x, int y)
    {
        position = new Vector2Int(x, y);
        model.transform.position = new Vector2(x + 0.5F, y + 0.5F);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
    }

    public void DestroySelf()
    {
        GameObject.Destroy(model);
    }

    public virtual Attack GetBasicAttack()
    {
        return null;
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

    public Vector2Int GetPosition()
    {
        return position;
    }

    //Stats
    public int GetHP()
    {
        return hp;
    }

    public int GetDamage()
    {
        return p_dmg;
    }

    public int GetSpeed()
    {
        return spd;
    }

    public int GetActions()
    {
        return act;
    }

    //Overide
    public override string ToString()
    {
        return model.name;
    }
}