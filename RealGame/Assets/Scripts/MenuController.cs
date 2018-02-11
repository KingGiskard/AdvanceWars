using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    //attaches to camera
    public GameObject player_game_object;

    // Use this for initialization
    void Start () {
       
        Controller game = this.gameObject.AddComponent<Controller>();
        game.initialize(new Dictionary<int, string> { { 1, "bobby" } }, player_game_object);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}