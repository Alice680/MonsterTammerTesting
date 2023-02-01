using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    //Temp
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject enemy;

    private Player player_controller;

    //Data storage
    private Grid current_map;
    private TurnKeeper turn_keeper;
    private List<Entity> entities;

    private Actor active_actor;
    private Entity current_entity;

    //Temp Variables
    private bool turn_ended;

    private void Start()
    {
        cam.transform.position = new Vector3(8, 8, -10);

        current_map = new Grid(17, 17, cam);
        turn_keeper = new TurnKeeper();
        entities = new List<Entity>();

        player_controller = new Player(this);

        //temp
        entities.Add(new Entity(player, player_controller, new int[4] { 3, 1, 2, 2 }));
        entities.Add(new Entity(enemy, new AICore(this), new int[4] { 1, 1, 1, 2 }));
        current_map.MoveEntity(8, 8, entities[0]);
        current_map.MoveEntity(3, 12, entities[1]);

        active_actor = player_controller;
        //

        foreach (Entity ent in entities)
        {
            turn_keeper.AddEntity(ent);
        }

        StartTurn();
    }

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

    //Basic Actions
    public void StartTurn()
    {
        turn_ended = false;

        turn_keeper.Increment();

        current_entity = turn_keeper.GetCurrent();
        active_actor = current_entity.GetActor();
    }

    public bool TryToMove(int x, int y)
    {
        if (turn_ended)
            return false;

        if (!(x != 0 ^ y != 0))
            return false;

        Vector2Int vec = current_map.GetEntityPosition(current_entity) + new Vector2Int(x, y);

        if (vec.x == -1 || vec.y == -1 || !current_map.CheckEntityEnter(vec.x, vec.y, current_entity))
            return false;

        current_map.MoveEntity((int)vec.x, (int)vec.y, current_entity);

        return true;
    }

    public bool TryToAttack(int x, int y)
    {
        if (turn_ended)
            return false;

        return false;
    }

    public void EndTurn()
    {
        turn_ended = true;
    }
}