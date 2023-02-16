using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static Entity CreatePlayer(Player player)
    {
        GameObject temp = ((BattleSpriteHolder)Resources.Load("DataHolders/SpriteHolder")).GetPlayerSprite(BattleSpriteType.Player, 0);

        Entity entity = new Human(temp, player);

        return entity;
    }
}