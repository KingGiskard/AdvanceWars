using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private GridBase map;
    private int turnCounter;

    private Material grid_mat_1;
    private Material grid_mat_2;

    HashSet<Player> players;

    public void initialize(Dictionary<int, string> _players, GameObject tile_game_object, GameObject player_game_object)
    {
        //create the board
        map = new GridBase(13, 13);

        //load material resources
        grid_mat_1 = Resources.Load<Material>("Materials/Blue");
        grid_mat_2 = Resources.Load<Material>("Materials/Red");


        //give each node a gameobject
        foreach (Node node in map.grid)
        {
            GameObject gridObj = Instantiate(tile_game_object, new Vector3(node.x, 0, node.z), Quaternion.identity) as GameObject;
            gridObj.transform.name = node.x.ToString() + " " + node.z.ToString();
            gridObj.transform.parent = transform;
            if (((node.x * map.xMax) + node.z) % 2 == 1)
            {
                gridObj.GetComponent<Renderer>().material = grid_mat_1;
            }
            else
            {
                gridObj.GetComponent<Renderer>().material = grid_mat_2;
            }
            node.worldObject = gridObj;
        }

        turnCounter = 0;

        players = new HashSet<Player>();
        foreach(int player_id in  _players.Keys)
        {

            GameObject player = GameObject.Instantiate(player_game_object, new Vector3(6,13,6), Quaternion.Euler(90,0,0));
            player.GetComponent<Player>().initialize(player_id, _players[player_id], this);
            players.Add(player.GetComponent<Player>());
        }

    }
    
	
	// Update is called once per frame
	void Update () {

        //update the board
        //increment game counter
		
	}
}
