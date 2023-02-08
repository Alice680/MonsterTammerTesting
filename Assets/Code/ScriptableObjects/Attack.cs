using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack", menuName = "InGameStuff/Attack", order = 3)]
public class Attack : ScriptableObject
{
    [SerializeField] private int power;
    [SerializeField] private MapShape target_area, effect_area;
    [SerializeField] private GameObject menu_sprite, battle_sprite;

    //Aplly Effect
    public void TriggerAttack(Entity user, Entity target)
    {
        target.TakeDamage(power + user.GetDamage());
    }

    public Vector2Int[] GetTargetArea(Vector2Int start, Direction direction, Vector2Int grid_size)
    {
        return target_area.GetPoints(start, direction, grid_size.x, grid_size.y);
    }

    public Vector2Int[] GetTargetEffect(Vector2Int start, Direction direction, Vector2Int grid_size)
    {
        return effect_area.GetPoints(start, direction, grid_size.x, grid_size.y);
    }

    ///Get info out of attacks
    
    public GameObject GetBattleSprite()
    {
        return Instantiate(battle_sprite);
    }

}