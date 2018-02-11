using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject player_game_object;
    public GameObject map_game_object;
    // Use this for initialization
    void Start () {


        GameObject map = GameObject.Instantiate(map_game_object, Vector3.zero, Quaternion.identity);
        map.GetComponent<GridBase>().initialize(12, 1, 12);
       
        Controller game = this.gameObject.AddComponent<Controller>();
        game.initialize(new Dictionary<int, string> { { 1, "bobby" } }, map.GetComponent<GridBase>(), player_game_object);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
