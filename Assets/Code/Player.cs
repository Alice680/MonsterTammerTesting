using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    private class InputStorage
    {
        public Vector2Int move_direction;

        public bool enter_key;
        public bool return_key;

        //Stores user input so it dose not need to keep being refrenced
        public void StoreInput()
        {
            move_direction = new Vector2Int(0, 0);
            move_direction.x = (int)Input.GetAxisRaw("Horizontal");
            move_direction.y = (int)Input.GetAxisRaw("Vertical");

            if (move_direction.x != 0 && move_direction.y != 0)
            {
                if (Random.Range(0, 2) == 0)
                    move_direction.x = 0;
                else
                    move_direction.y = 0;
            }

            enter_key = Input.GetKeyDown(KeyCode.Return);
            return_key = Input.GetKeyDown(KeyCode.RightShift);
        }
    }

    private enum State { Idle, AttackArea, AttackEffect }

    private State current_state;

    private float last_input;

    private Vector2Int target;

    private UI ui;

    private InputStorage inputs;

    public Player(DungeonManager manager, UI ui) : base(manager)
    {
        this.ui = ui;
        inputs = new InputStorage();
    }

    public override void StartTurn()
    {
        ui.UpdateActions();

        current_state = State.Idle;

        target = new Vector2Int(-1, -1);
    }

    public override void Run()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            EndTurn();

        if (Time.time - last_input < 0.15f)
            return;

        inputs.StoreInput();

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
        if (inputs.move_direction.x != 0 || inputs.move_direction.y != 0)
        {
            last_input = Time.time;

            dm_refrence.TryToMove(inputs.move_direction);

            if (dm_refrence.GetActions() == 0)
                EndTurn();
            else
                ui.UpdateActions();

        }
        else if (inputs.enter_key)
        {
            target = dm_refrence.GetPositionFromID(dm_refrence.GetIDFromActive());

            dm_refrence.ShowAttackArea(target.x, target.y);

            current_state = State.AttackArea;
        }
    }

    private void RunViewAttackArea()
    {
        if (inputs.move_direction.x != 0 || inputs.move_direction.y != 0)
        {
            last_input = Time.time;

            if (dm_refrence.IsValidPosition(target + inputs.move_direction))
            {
                target += inputs.move_direction;
                dm_refrence.ShowAttackArea(target.x, target.y);
            }
        }
        else if (inputs.enter_key)
        {
            if (dm_refrence.IsValidAttackLocation(target))
            {
                dm_refrence.ShowAttackEffect(target);

                current_state = State.AttackEffect;
            }
        }
        else if (inputs.return_key)
        {
            target = new Vector2Int(-1, -1);

            dm_refrence.ClearTileMarkers();

            current_state = State.Idle;
        }
    }

    private void RunViewAttackEffect()
    {
        if (inputs.enter_key)
        {
            if (!dm_refrence.TryToAttack(target))
                return;

            target = new Vector2Int(-1, -1);

            dm_refrence.ClearTileMarkers();

            current_state = State.Idle;

            if (dm_refrence.GetActions() == 0)
                EndTurn();
            else
                ui.UpdateActions();
        }
        else if (inputs.return_key)
        {
            dm_refrence.ShowAttackArea(target.x, target.y);

            current_state = State.AttackArea;
        }
    }

    //Ignores current state and ends turn. Reseting all ui elemnts to their default value.
    private void EndTurn()
    {
        current_state = State.Idle;

        ui.ClearActions();

        dm_refrence.ClearTileMarkers();

        dm_refrence.EndTurn();
    }
}