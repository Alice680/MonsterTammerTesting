using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The core manager of the game. It has the only update in the scene has all data flow through it to authenticate.
public class DungeonManager : MonoBehaviour
{
    //Temp
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    [SerializeField] private Attack short_sword;

    private Player player_controller;

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

        current_map = new Grid(17, 17, cam);
        turn_keeper = new TurnKeeper();
        entities = new List<Entity>();

        player_controller = new Player(this, ui);

        //temp
        cam.transform.position = new Vector3(8.5f, 8.5f, -10);

        entities.Add(new Entity(player, player_controller, new int[4] { 3, 1, 2, 3 }));
        //entities.Add(new Entity(enemy, new AICore(this), new int[4] { 1, 1, 1, 2 }));
        current_map.MoveEntity(8, 8, entities[0]);
        //current_map.MoveEntity(3, 12, entities[1]);

        active_actor = player_controller;
        //

        foreach (Entity ent in entities)
        {
            turn_keeper.AddEntity(ent);
        }

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
    public bool TryToMove(int x, int y)
    {
        if (turn_ended || actions_left == -0)
            return false;

        if (!(x != 0 ^ y != 0))
            return false;

        Vector2Int vec = current_map.GetEntityPosition(current_entity) + new Vector2Int(x, y);

        if (vec.x == -1 || vec.y == -1 || !current_map.CheckEntityEnter(vec.x, vec.y, current_entity))
            return false;

        current_map.MoveEntity((int)vec.x, (int)vec.y, current_entity);

        --actions_left;

        return true;
    }

    //
    public bool TryToAttack(int x, int y)
    {
        if (turn_ended)
            return false;

        return false;
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

        return new Vector2Int(-1,-1);
    }

    ///Edit the tile markers in various ways. These are purly cosmetic and don't effect the gameplay in and of themselves.

    //Call the clear function on the grid.
    public void ClearTileMarkers()
    {
        current_map.ClearTileMarkers();
    }

    public void ShowAttackArea(int x, int y)
    {
        Vector2Int[] points = short_sword.GetTargetArea(current_entity.GetPosition(), Direction.Up, current_map.GetGridSize());

        current_map.ClearTileMarkers();

        current_map.SetTileMarker(x, y, 0);

        foreach (Vector2Int point in points)
        {
            if (point.x == x && point.y == y)
                current_map.SetTileMarker(point.x, point.y, 2);
            else
                current_map.SetTileMarker(point.x, point.y, 1);
        }
    }

    public void ShowAttackEffect(int x, int y)
    {

    }
}