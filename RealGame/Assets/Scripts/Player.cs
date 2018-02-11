using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private int player_id;
    private string player_name;
    Dictionary<string, HashSet<Unit>> player_units;
    private int player_income;
    public int building_count;
    private Controller game_controller;

    public void initialize(int _player_id, string _player_name, Controller _game_controller)
    {
        player_id = _player_id;
        player_name = _player_name;
        player_units = new Dictionary<string, HashSet<Unit>>();
        player_income = 100;
        player_units.Add("capitol", new HashSet<Unit>());
        player_units["capitol"].Add(new BuildingUnit(_player_id, _player_name, 100, 10, 10, 200, "capitol building"));
    }

    // Update is called once per frame
    void Update()
    {
        //select unit

        //move unit

        //create unit

        //move camera

        //some kind of menu
    }
}
