using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private enum State { Idle, AttackArea, AttackEffect }

    private State current_state;

    private float last_input;

    private Vector2Int target;

    private UI ui;

    public Player(DungeonManager manager, UI ui) : base(manager)
    {
        this.ui = ui;
    }

    public override void StartTurn()
    {
        ui.UpdateActions();

        current_state = State.Idle;
    }

    public override void Run()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            RunEndTurn();

        if (Time.time - last_input < 0.15f)
            return;

        switch (current_state)
        {
            case State.Idle:
                RunIdle();
                break;
            case State.AttackArea:
                RunViewAttackArea();
                break;
            case State.AttackEffect:
                RunViewAttackEffect();
                break;
        }
    }

    ///Takes inputs based on the users current state

    private void RunIdle()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector2Int move_direction = new Vector2Int(0, 0);

            move_direction.x = (int)Input.GetAxisRaw("Horizontal");
            move_direction.y = (int)Input.GetAxisRaw("Vertical");

            dm_refrence.TryToMove(move_direction.x, move_direction.y);

            if (dm_refrence.GetActions() == 0)
                dm_refrence.EndTurn();

            ui.UpdateActions();

            last_input = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            target = dm_refrence.GetPositionFromID(dm_refrence.GetIDFromActive());

            dm_refrence.ShowAttackArea(target.x, target.y);

            current_state = State.AttackArea;
        }
    }

    private void RunViewAttackArea()
    {

    }

    private void RunViewAttackEffect()
    {

    }

    //Ignores current state and ends turn. Reseting all ui elemnts to their default value.
    private void RunEndTurn()
    {
        current_state = State.Idle;

        dm_refrence.ClearTileMarkers();

        dm_refrence.EndTurn();
    }
}