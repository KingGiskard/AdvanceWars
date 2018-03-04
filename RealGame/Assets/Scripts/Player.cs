using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour
{

    private int player_id;
    private int player_income;

    public int building_count;

    private string player_name;

    private GameObject selected_unit;
    private Node selected_tile;

    private float horizontal_spd;
    private float vertical_spd;
    private float move_spd = 10.0f;

    Dictionary<string, HashSet<Unit>> player_units;

    private Controller game_controller;
    private Camera player_camera;

    public void initialize(int _player_id, string _player_name, Controller _game_controller)
    {
        player_id = _player_id;
        player_name = _player_name;
        player_units = new Dictionary<string, HashSet<Unit>>();
        player_income = 100;
        player_units.Add("capitol", new HashSet<Unit>());
        player_units["capitol"].Add(new BuildingUnit(_player_id, _player_name, 100, 10, 10, 200, "capitol building"));
        player_camera = this.gameObject.GetComponent<Camera>();

        game_controller = _game_controller;
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = player_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Combat Unit")
                {
                    selected_unit = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag == "Map Tile")
                {
                    selected_tile = game_controller.map.GetNodeFromVector3(hit.transform.position);
                }
            }
            else
            {
                selected_tile = null;
                selected_unit = null;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = player_camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Combat Unit")
                {
                    selected_unit = hit.collider.gameObject;
                }
                else if (hit.collider.gameObject.tag == "Map Tile")
                {
                    selected_tile = game_controller.map.GetNodeFromVector3(hit.transform.position);
                }
            }
            else
            {
                selected_tile = null;
                selected_unit = null;
            }
        }

        horizontal_spd = Input.GetAxis("Horizontal") * Time.deltaTime * move_spd;
        vertical_spd = Input.GetAxis("Vertical") * Time.deltaTime * move_spd;

        transform.Translate(horizontal_spd, vertical_spd, 0);

        //move unit

        //create unit

        //move camera

        //some kind of menu
    }
}
