using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OnlineMenu : MonoBehaviour {

    private Canvas ui_canvas;
    private Button resume;
    private Button leave;
    private CustomNetworkController net_controller;

    private bool is_menu_open;
        
	void Awake ()
    {
        ui_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        resume = GameObject.Find("Resume").GetComponent<Button>();
        leave = GameObject.Find("Leave").GetComponent<Button>();
        net_controller = GameObject.Find("Network Manager").GetComponent<CustomNetworkController>();
	}

    private void Start()
    {
        CloseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (is_menu_open)
            {
                CloseMenu();
                is_menu_open = !is_menu_open;
            }
            else
            {
                OpenMenu();
                is_menu_open = !is_menu_open;
            }
        }
    }

    public void Resume()
    {
        CloseMenu();
    }

    public void Leave()
    {
        
    }

    private void OpenMenu()
    {
        resume.enabled = true;
        resume.gameObject.SetActive(true);
        leave.enabled = true;
        leave.gameObject.SetActive(true);
        is_menu_open = true;
    }

    private void CloseMenu()
    {
        resume.enabled = false;
        resume.gameObject.SetActive(false);
        leave.enabled = false;
        leave.gameObject.SetActive(false);
        is_menu_open = false;
    }
}
