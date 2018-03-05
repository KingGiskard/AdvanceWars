using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Controller : MonoBehaviour {

    public GridBase map;
    private int turnCounter;

    private Material grid_mat_1;
    private Material grid_mat_2;

    private GameObject blue_tile_object;
    private GameObject red_tile_object;
    private GameObject player_game_object;

    private GameObject tank_object;

    private Dictionary<int,Player> players;

    public void initialize(Dictionary<NetworkClient, string> _players)
    {
        //load resources
        blue_tile_object = Resources.Load<GameObject>("Prefabs/GridFloorBlue");
        red_tile_object = Resources.Load<GameObject>("Prefabs/GridFloorRed");
        player_game_object = Resources.Load<GameObject>("Prefabs/Player");
        tank_object = Resources.Load<GameObject>("Models/Minitank");

        //create the board
        map = new GridBase(13, 13);

        //give each node a gameobject
        foreach (Node node in map.grid)
        {
            GameObject gridObj;
            if (((node.x * map.xMax) + node.z) % 2 == 1)
            {
                gridObj = Instantiate(blue_tile_object, new Vector3(node.x, 0, node.z), Quaternion.identity) as GameObject;
                gridObj.transform.name = node.x.ToString() + " " + node.z.ToString();
                gridObj.transform.parent = transform;
            }
            else
            {
                gridObj = Instantiate(red_tile_object, new Vector3(node.x, 0, node.z), Quaternion.identity) as GameObject;
                gridObj.transform.name = node.x.ToString() + " " + node.z.ToString();
                gridObj.transform.parent = transform;
            }
            node.worldObject = gridObj;
            NetworkServer.Spawn(gridObj);
        }

        turnCounter = 0;

        players = new Dictionary<int, Player>();
        foreach (KeyValuePair<NetworkClient, string> client in _players)
        {
            PlayerController temp_controller = client.Key.connection.playerControllers[0];
            temp_controller.gameObject.GetComponent<Player>().initialize(temp_controller.playerControllerId, client.Value, this);
            players.Add(temp_controller.playerControllerId, temp_controller.gameObject.GetComponent<Player>());
        }
		
    }
    	
	// Update is called once per frame
	void Update () {

        //update the board
        //increment game counter
        
	}

    public void CreateUnit(Node place_to_make, int player_id, string unit_type)
    {
        //create unit for player of type unit_type at place_to_make
        Unit newUnit;
        GameObject Combat_unit;

        if (isCombatUnit(unit_type))
        {
            Combat_unit = Instantiate(tank_object, new Vector3(place_to_make.x, 0, place_to_make.z), Quaternion.identity);
            Combat_unit.gameObject.name = unit_type;        
        }
    } 
    private bool isCombatUnit(string input)
    {
        return input.Equals("tank");
    }

}
