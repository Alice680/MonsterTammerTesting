using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles all visual elements of the game
public class UI : MonoBehaviour
{
    [Serializable] private struct ActionsElements
    {
        public GameObject[] crystals;
    }

    [SerializeField] private ActionsElements actions_elements;

    private DungeonManager dm_refrence;

    //Called the first time the scene is run to set up the UI and clear all it's values
    public void SetUp(DungeonManager dm)
    {
        dm_refrence = dm;

        ClearActions();
    }

    //Sets all UI elements to display their default values
    public void ClearUI()
    {
        ClearActions();
    }

    //Set the number of crystals visable to be equal to the number of actions the player has left.
    public void UpdateActions()
    {
        ClearActions();

        for(int i = 0; i < dm_refrence.GetActions(); ++i)
            actions_elements.crystals[i].SetActive(true);
    }

    //Set all the crystals to be none visable
    public void ClearActions()
    {
        for (int i = 0; i < 12; ++i)
            actions_elements.crystals[i].SetActive(false);
    }
}