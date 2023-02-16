using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The core manager of the game. It has the only update in the scene has all data flow through it to authenticate.
public class DungeonManager : MonoBehaviour
{
    //Temp
    [SerializeField] private Camera cam;

    public DungeonParameters layout;

    private Player player_controller;
    private AICore map_controller;

    //Data storage
    private Grid current_map;
    private TurnKeeper turn_keeper;
    private List<Entity> entities;
    private UI ui;

    private Actor active_actor;
    private Entity current_entity;

    //Temp Variables
    private bool turn_ended;

    //Round Variables
    private int actions_left;

    //Sets the default game varibles. Internal notes for more detail.
    private void Start()
    {
        ui = GetComponent<UI>();
        ui.SetUp(this);

        current_map = new Grid(layout, cam);
        turn_keeper = new TurnKeeper();
        entities = new List<Entity>();

        player_controller = new Player(this, ui);
        map_controller = new AICore(this);

        //temp
        cam.transform.position = new Vector3(8.5f, 8.5f, -10);

        AddEntity(new Vector2Int(8, 8), PlayerData.CreatePlayer(player_controller));
        //

        StartTurn();
    }

    //Gives over controll to the current actor if there turn has not ended and progress to the next turn if it has.
    private void Update()
    {
        if (!turn_ended)
        {
            active_actor.Run();
        }
        else
        {
            StartTurn();
        }
    }

    ///Internal Methods

    //Add Entity
    private void AddEntity(Vector2Int position, Entity entity)
    {
        entities.Add(entity);

        turn_keeper.AddEntity(entity);

        current_map.MoveEntity(position, entity);
    }

    //Remove Entity
    private void RemoveEntity(Entity entity)
    {
        current_map.RemoveEntity(entity);

        turn_keeper.RemoveEntity(entity);

        entities.Remove(entity);

        entity.DestroySelf();
    }

    ///Core external calls. These methods advance the game state in various ways.

    //
    public void StartTurn()
    {
        turn_ended = false;

        turn_keeper.Increment();

        current_entity = turn_keeper.GetCurrent();
        active_actor = current_entity.GetActor();

        actions_left = current_entity.GetActions();

        active_actor.StartTurn();
    }

    //
    public bool TryToMove(Vector2Int position)
    {
        if (turn_ended || actions_left == -0)
            return false;

        if (position.x != 0 && position.y != 0)
            return false;

        Vector2Int vec = current_map.GetEntityPosition(current_entity) + position;

        if (vec.x == -1 || vec.y == -1 || !current_map.CheckEntityEnter(vec, current_entity))
            return false;

        current_map.MoveEntity(vec, current_entity);

        --actions_left;

        return true;
    }

    //
    public bool TryToAttack(Vector2Int position, int index)
    {
        if (turn_ended || !IsValidPosition(position) || !IsValidAttackLocation(position, index))
            return false;


        Attack temp_attack = null;

        if (index == -1)
            temp_attack = current_entity.GetBasicAttack();
        else
            return false;

        --actions_left;

        Vector2Int[] points = temp_attack.GetTargetEffect(position, Direction.Up, current_map.GetGridSize());

        List<Entity> targets = new List<Entity>();

        foreach (Vector2Int point in points)
        {
            GameObject attack_sprite = temp_attack.GetBattleSprite();
            attack_sprite.transform.position = new Vector2(0.5f, 0.5f) + position;

            Entity temp = current_map.GetEntityAtPosition(point);

            if (temp != null)
                targets.Add(temp);
        }

        foreach (Entity target in targets)
        {
            temp_attack.TriggerAttack(current_entity, target);

            if (target.GetHP() <= 0)
                RemoveEntity(target);
        }

        return true;
    }

    //
    public void EndTurn()
    {
        turn_ended = true;
    }

    ///Retrive data stored within the DungeonManager itself

    //Returns the number of actions the current player has left
    public int GetActions()
    {
        return actions_left;
    }

    public bool IsValidPosition(Vector2Int position)
    {
        return current_map.IsValidPosition(position);
    }

    public bool IsValidAttackLocation(Vector2Int position, int index)
    {
        Attack temp_attack = null;

        if (index == -1)
            temp_attack = current_entity.GetBasicAttack();
        else
            return false;

        Vector2Int[] points = temp_attack.GetTargetArea(current_entity.GetPosition(), Direction.Up, current_map.GetGridSize());

        foreach (Vector2Int point in points)
            if (point.Equals(position))
                return true;

        return false;
    }

    ///Get Id from of enity through various means.

    //Get id from currently avtive Entity
    public int GetIDFromActive()
    {
        return current_entity.GetID();
    }

    ///Get info of enity from their ID.

    //A purely internal function that retrives an entity based on their ID
    private Entity GetEntity(int id)
    {
        foreach (Entity entity in entities)
            if (entity.GetID() == id)
                return entity;

        return null;
    }

    //Returns the positon of the entity using the built in method
    public Vector2Int GetPositionFromID(int id)
    {
        Entity entity = GetEntity(id);

        if (entity != null)
            return entity.GetPosition();

        return new Vector2Int(-1, -1);
    }

    ///Edit the tile markers in various ways. These are purly cosmetic and don't effect the gameplay in and of themselves.

    //Call the clear function on the grid.
    public void ClearTileMarkers()
    {
        current_map.ClearTileMarkers();
    }

    //
    public void ShowAttackArea(Vector2Int target, int index)
    {
        Attack temp_attack = null;

        if (index == -1)
            temp_attack = current_entity.GetBasicAttack();
        else
            return;

        Vector2Int[] points = temp_attack.GetTargetArea(current_entity.GetPosition(), Direction.Up, current_map.GetGridSize());

        current_map.ClearTileMarkers();

        current_map.SetTileMarker(target, 0);

        foreach (Vector2Int point in points)
        {
            if (point.x == target.x && point.y == target.y)
                current_map.SetTileMarker(point, 2);
            else
                current_map.SetTileMarker(point, 1);
        }
    }

    //
    public void ShowAttackEffect(Vector2Int target, int index)
    {
        Attack temp_attack = null;

        if (index == -1)
            temp_attack = current_entity.GetBasicAttack();
        else
            return;

        Vector2Int[] points = temp_attack.GetTargetEffect(target, Direction.Up, current_map.GetGridSize());

        current_map.ClearTileMarkers();

        foreach (Vector2Int point in points)
            current_map.SetTileMarker(target, 3);
    }
}