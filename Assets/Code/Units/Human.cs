using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Entity
{
    private Attack temp;

    public Human(GameObject obj, Actor actor) : base(obj, actor)
    {
        max_hp = hp = 5;
        max_sp = sp = 3;
        max_mp = mp = 3;
        p_dmg = 2;
        m_dmg = 2;
        frc = 2;
        p_def = 2;
        m_def = 2;
        res = 2;
        spd = 3;
        act = 3;

        temp = ((AttackHolder)Resources.Load("DataHolders/AttackHolder")).GetAttack(0, AttackType.Weapon);
    }

    public override Attack GetBasicAttack()
    {
        return temp;
    }
}