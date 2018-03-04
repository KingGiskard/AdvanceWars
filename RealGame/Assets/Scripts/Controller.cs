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

    HashSet<Player> players;

    public void initialize(Dictionary<NetworkClient, string> _players)
    {
        //load resources
        blue_tile_object = Resources.Load<GameObject>("Prefabs/GridFloorBlue");
        red_tile_object = Resources.Load<GameObject>("Prefabs/GridFloorRed");
        player_game_object = Resources.Load<GameObject>("Prefabs/Player");

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

        players = new HashSet<Player>();
        foreach (KeyValuePair<NetworkClient, string> client in _players)
        {
            PlayerController temp_controller = client.Key.connection.playerControllers[0];
            temp_controller.gameObject.GetComponent<Player>().initialize(temp_controller.playerControllerId, client.Value, this);
            players.Add(temp_controller.gameObject.GetComponent<Player>());
        }
		//foreach(int player_id in  _players.Keys)
		//{
		//	GameObject player = GameObject.Instantiate(player_game_object, new Vector3(6,9,6), Quaternion.Euler(90,0,0));
		//	player.GetComponent<Player>().initialize(player_id, _players[player_id], this);
		//	players.Add(player.GetComponent<Player>());
		//}
    }
    	
	// Update is called once per frame
	void Update () {

        //update the board
        //increment game counter
        
	}

    public void CreateUnit(Node place_to_make, int player_id, string unit_type)
    {
        //create unit for player of type unit_type at place_to_make
    } 

}
