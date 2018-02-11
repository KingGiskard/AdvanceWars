using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    private GridBase map;
    private int turnCounter;

    HashSet<Player> players;

    public void initialize(Dictionary<int, string> _players, GridBase _game_map, GameObject player_game_object)
    {
        turnCounter = 0;

        players = new HashSet<Player>();
        foreach(int player_id in  _players.Keys)
        {

            GameObject player = GameObject.Instantiate(player_game_object, new Vector3(6,10,6), Quaternion.Euler(90,0,0));
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
