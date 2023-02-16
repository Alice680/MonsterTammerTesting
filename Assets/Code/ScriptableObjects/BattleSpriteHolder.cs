using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpriteHolder", menuName = "DataHolders/BattleSprite", order = 3)]
public class BattleSpriteHolder : ScriptableObject
{
    [SerializeField] private GameObject[] player_sprites;

    public GameObject GetPlayerSprite(BattleSpriteType type, int index)
    {
        if (type == BattleSpriteType.Player && index < player_sprites.Length)
            return Instantiate(player_sprites[index]);

        return null;
    }
}