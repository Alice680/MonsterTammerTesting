using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    //Refences
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject player;

    //Data storage
    private Grid current_map;
    private TurnKeeper turn_keeper;
    private List<Entity> entities;

    private void Start()
    {
        cam.transform.position = new Vector3(8, 8, -10);

        current_map = new Grid(17, 17, cam);
        turn_keeper = new TurnKeeper();
        entities = new List<Entity>();

        //Temp
        entities.Add(new Entity(player));
        current_map.MoveEntity(8, 8, entities[0]);

        foreach (Entity ent in entities)
        {

        }
    }

    private void Update()
    {
        //Temp
        Vector2Int vec = new Vector2Int(0, 0);

        if (Input.GetKeyDown(KeyCode.D))
        {
            vec.x = 1;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            vec.x = -1;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            vec.y = 1;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            vec.y = -1;
        }

        if (vec.x != 0 || vec.y != 0)
        {
            vec += current_map.GetEntityPosition(entities[0]);
            current_map.MoveEntity((int)vec.x, (int)vec.y, entities[0]);
        }
    }
}