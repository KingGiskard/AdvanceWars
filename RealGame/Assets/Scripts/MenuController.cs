using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    // Use this for initialization
    void Start () {
       
        Controller game = this.gameObject.AddComponent<Controller>();
        game.initialize(new Dictionary<int, string> { { 1, "bobby" } });
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}