using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class OfflineMenu : MonoBehaviour {

    private Button start;
    private Button options;
    private Button quit;

    private Button host;
    private Button join;
    private Button server;
    private Button matchmaking;
    private Button start_back;

    private InputField net_port;
    private InputField net_address;
    private InputField max_connect;
    private InputField player_name;
    private Button apply;
    private Button options_back;

    private CustomNetworkController net_controller;

    void Awake()
    {
        net_controller = GameObject.Find("Network Manager").GetComponent<CustomNetworkController>();

        start = GameObject.Find("StartButton").GetComponent<Button>();
        options = GameObject.Find("OptionsButton").GetComponent<Button>();
        quit = GameObject.Find("QuitButton").GetComponent<Button>();

        host = GameObject.Find("HostButton").GetComponent<Button>();
        join = GameObject.Find("JoinButton").GetComponent<Button>();
        server = GameObject.Find("ServerButton").GetComponent<Button>();
        matchmaking = GameObject.Find("MatchmakingButton").GetComponent<Button>();
        start_back = GameObject.Find("StartBackButton").GetComponent<Button>();

        net_port = GameObject.Find("NetworkPortInput").GetComponent<InputField>();
        net_address = GameObject.Find("NetworkAddressInput").GetComponent<InputField>();
        max_connect = GameObject.Find("MaxConnectionsInput").GetComponent<InputField>();
        player_name = GameObject.Find("PlayerNameInput").GetComponent<InputField>();
        apply = GameObject.Find("ApplyButton").GetComponent<Button>();
        options_back = GameObject.Find("OptionsBackButton").GetComponent<Button>();
    }

    void Start ()
    {
        MainMenu();
    }

    public void MainMenu()
    {
        start.enabled = true;
        start.gameObject.SetActive(true);
        options.enabled = true;
        options.gameObject.SetActive(true);
        quit.enabled = true;
        quit.gameObject.SetActive(true);

        host.enabled = false;
        host.gameObject.SetActive(false);
        join.enabled = false;
        join.gameObject.SetActive(false);
        server.enabled = false;
        server.gameObject.SetActive(false);
        matchmaking.enabled = false;
        matchmaking.gameObject.SetActive(false);
        start_back.enabled = false;
        start_back.gameObject.SetActive(false);

        net_port.enabled = false;
        net_port.gameObject.SetActive(false);
        net_address.enabled = false;
        net_address.gameObject.SetActive(false);
        max_connect.enabled = false;
        max_connect.gameObject.SetActive(false);
        player_name.enabled = false;
        player_name.gameObject.SetActive(false);
        apply.enabled = false;
        apply.gameObject.SetActive(false);
        options_back.enabled = false;
        options_back.gameObject.SetActive(false);
    }

    public void StartMenu()
    {
        start.enabled = false;
        start.gameObject.SetActive(false);
        options.enabled = false;
        options.gameObject.SetActive(false);
        quit.enabled = false;
        quit.gameObject.SetActive(false);

        host.enabled = true;
        host.gameObject.SetActive(true);
        join.enabled = true;
        join.gameObject.SetActive(true);
        server.enabled = true;
        server.gameObject.SetActive(true);
        matchmaking.enabled = true;
        matchmaking.gameObject.SetActive(true);
        start_back.enabled = true;
        start_back.gameObject.SetActive(true);

        net_port.enabled = false;
        net_port.gameObject.SetActive(false);
        net_address.enabled = false;
        net_address.gameObject.SetActive(false);
        max_connect.enabled = false;
        max_connect.gameObject.SetActive(false);
        player_name.enabled = false;
        player_name.gameObject.SetActive(false);
        apply.enabled = false;
        apply.gameObject.SetActive(false);
        options_back.enabled = false;
        options_back.gameObject.SetActive(false);
    }

    public void Server()
    {
        start.enabled = false;
        start.gameObject.SetActive(false);
        options.enabled = false;
        options.gameObject.SetActive(false);
        quit.enabled = false;
        quit.gameObject.SetActive(false);

        host.enabled = false;
        host.gameObject.SetActive(false);
        join.enabled = false;
        join.gameObject.SetActive(false);
        server.enabled = false;
        server.gameObject.SetActive(false);
        matchmaking.enabled = false;
        matchmaking.gameObject.SetActive(false);
        start_back.enabled = false;
        start_back.gameObject.SetActive(false);

        net_port.enabled = false;
        net_port.gameObject.SetActive(false);
        net_address.enabled = false;
        net_address.gameObject.SetActive(false);
        max_connect.enabled = false;
        max_connect.gameObject.SetActive(false);
        player_name.enabled = false;
        player_name.gameObject.SetActive(false);
        apply.enabled = false;
        apply.gameObject.SetActive(false);
        options_back.enabled = false;
        options_back.gameObject.SetActive(false);

        net_controller.StartServer();
    }

    public void Host()
    {
        start.enabled = false;
        start.gameObject.SetActive(false);
        options.enabled = false;
        options.gameObject.SetActive(false);
        quit.enabled = false;
        quit.gameObject.SetActive(false);

        host.enabled = false;
        host.gameObject.SetActive(false);
        join.enabled = false;
        join.gameObject.SetActive(false);
        server.enabled = false;
        server.gameObject.SetActive(false);
        matchmaking.enabled = false;
        matchmaking.gameObject.SetActive(false);
        start_back.enabled = false;
        start_back.gameObject.SetActive(false);

        net_port.enabled = false;
        net_port.gameObject.SetActive(false);
        net_address.enabled = false;
        net_address.gameObject.SetActive(false);
        max_connect.enabled = false;
        max_connect.gameObject.SetActive(false);
        player_name.enabled = false;
        player_name.gameObject.SetActive(false);
        apply.enabled = false;
        apply.gameObject.SetActive(false);
        options_back.enabled = false;
        options_back.gameObject.SetActive(false);

        net_controller.StartHost();
    }

    public void Join()
    {
        start.enabled = false;
        start.gameObject.SetActive(false);
        options.enabled = false;
        options.gameObject.SetActive(false);
        quit.enabled = false;
        quit.gameObject.SetActive(false);

        host.enabled = false;
        host.gameObject.SetActive(false);
        join.enabled = false;
        join.gameObject.SetActive(false);
        server.enabled = false;
        server.gameObject.SetActive(false);
        matchmaking.enabled = false;
        matchmaking.gameObject.SetActive(false);
        start_back.enabled = false;
        start_back.gameObject.SetActive(false);

        net_port.enabled = false;
        net_port.gameObject.SetActive(false);
        net_address.enabled = false;
        net_address.gameObject.SetActive(false);
        max_connect.enabled = false;
        max_connect.gameObject.SetActive(false);
        player_name.enabled = false;
        player_name.gameObject.SetActive(false);
        apply.enabled = false;
        apply.gameObject.SetActive(false);
        options_back.enabled = false;
        options_back.gameObject.SetActive(false);

        net_controller.StartClient();
    }

    public void Options()
    {
        start.enabled = false;
        start.gameObject.SetActive(false);
        options.enabled = false;
        options.gameObject.SetActive(false);
        quit.enabled = false;
        quit.gameObject.SetActive(false);

        host.enabled = false;
        host.gameObject.SetActive(false);
        join.enabled = false;
        join.gameObject.SetActive(false);
        server.enabled = false;
        server.gameObject.SetActive(false);
        matchmaking.enabled = false;
        matchmaking.gameObject.SetActive(false);
        start_back.enabled = false;
        start_back.gameObject.SetActive(false);

        net_port.enabled = true;
        net_port.gameObject.SetActive(true);
        net_address.enabled = true;
        net_address.gameObject.SetActive(true);
        max_connect.enabled = true;
        max_connect.gameObject.SetActive(true);
        player_name.enabled = true;
        player_name.gameObject.SetActive(true);
        apply.enabled = true;
        apply.gameObject.SetActive(true);
        options_back.enabled =  true;
        options_back.gameObject.SetActive(true);
    }

    public void Apply()
    {
        if (net_port.text != "")
        {
            net_controller.network_port = int.Parse(net_port.text);
        }

        if (net_address.text != "")
        {
            net_controller.network_address = net_address.text;
        }

        if (max_connect.text != "")
        {
            net_controller.max_connect = int.Parse(max_connect.text);
        }

        if (player_name.text != "")
        {
            net_controller.player_name = player_name.text;
        }

        MainMenu();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
