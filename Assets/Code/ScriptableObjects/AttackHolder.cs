using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attacks", menuName = "DataHolders/Attacks", order = 2)]
public class AttackHolder : ScriptableObject
{
    [SerializeField] private Attack[] WeaponAttacks;

    public Attack GetAttack(int index, AttackType type)
    {
        if (index < 0)
            return null;

        if (type == AttackType.Weapon && index < WeaponAttacks.Length)
            return WeaponAttacks[index];

        return null;
    }
}