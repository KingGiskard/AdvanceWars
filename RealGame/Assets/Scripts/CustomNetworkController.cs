using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.Networking.Types;

public class CustomNetworkController : MonoBehaviour
{

    public static CustomNetworkController singleton;
    public string online_scene;
    public string offline_scene;
    public bool isNetworkActive;
    public GameObject player_prefab;
    public Transform spawn_transform;

    public MatchInfo match_info;
    public NetworkClient client;

    public int network_port = 7777;
    public string network_address = "localhost";
    public int max_connect = 2;
    public string player_name;

    public Controller game_controller;
    private float max_delay = 0.01f;
    private bool custom_config = false;
    private ConnectionConfig connection_config;
    private List<QosType> channels = new List<QosType>();
    private Dictionary<NetworkClient, string> clients = new Dictionary<NetworkClient, string>();
    private bool auto_create_player = true;

    static string address;
    static public string network_scene_name;
    static AsyncOperation loading_scene_async;
    static NetworkConnection client_ready_connect;
    static int start_pos_index;
    static List<Transform> start_pos = new List<Transform>();

    static AddPlayerMessage add_player_msg = new AddPlayerMessage();
    static RemovePlayerMessage remove_player_msg = new RemovePlayerMessage();

    void Awake()
    {
        if (singleton != null)
        {
            Destroy(gameObject);
            return;
        }
        singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    internal void RegisterServerMessages()
    {
        NetworkServer.RegisterHandler(MsgType.Connect, OnServerConnectInternal);
        NetworkServer.RegisterHandler(MsgType.Disconnect, OnServerDisconnectInternal);
        NetworkServer.RegisterHandler(MsgType.Ready, OnServerReadyMessageInternal);
        NetworkServer.RegisterHandler(MsgType.AddPlayer, OnServerAddPlayerMessageInternal);
    }

    internal void RegisterClientMessages(NetworkClient local_client)
    {
        local_client.RegisterHandler(MsgType.Connect, OnClientConnectInternal);
        local_client.RegisterHandler(MsgType.Disconnect, OnClientDisconnectInternal);
        local_client.RegisterHandler(MsgType.Scene, OnClientSceneInternal);

        if (player_prefab != null)
        {
            ClientScene.RegisterPrefab(player_prefab);
        }
    }

    //-----------Starting and Stopping Servers, Clients, and Hosts--------------------
    public bool StartServer()
    {
        return StartServer(null, null, max_connect);
    }

    bool StartServer(MatchInfoSnapshot info, ConnectionConfig config, int maxConnect)
    {
        //OnStartServer();

        Application.runInBackground = true;
        NetworkCRC.scriptCRCCheck = true;

        if (custom_config && connection_config != null && config == null)
        {
            connection_config.Channels.Clear();
            foreach (var c in channels)
            {
                connection_config.AddChannel(c);
            }
            NetworkServer.Configure(connection_config, maxConnect);
        }

        RegisterServerMessages();
        NetworkServer.useWebSockets = false;

        if (info == null)
        {
            if (!NetworkServer.Listen(network_port))
            {
                return false;
            }
        }

        isNetworkActive = true;

        if (online_scene != "" && online_scene != SceneManager.GetActiveScene().name)
        {
            ServerChangeScene(online_scene);
        }
        else
        {
            NetworkServer.SpawnObjects();
        }

        clients.Clear();

        return true;
    }

    public NetworkClient StartClient()
    {
        return StartClient(null, null);
    }

    NetworkClient StartClient(MatchInfo info, ConnectionConfig config)
    {
        match_info = info;
        Application.runInBackground = true;
        isNetworkActive = true;

        client = new NetworkClient();
        

        if (config == null)
        {
            if (custom_config && connection_config != null)
            {
                connection_config.Channels.Clear();
                foreach (var c in channels)
                {
                    connection_config.AddChannel(c);
                }
                client.Configure(connection_config, max_connect);
            }
        }
        RegisterClientMessages(client);
        client.Connect(network_address, network_port);
        OnStartClient(client, player_name);
        address = network_address;
        return client;
    }

    public virtual NetworkClient StartHost()
    {
        //OnStartHost();
        if (StartServer())
        {
            var localClient = ConnectLocalClient();
            //OnServerConnect(localClient.connection);
            OnStartClient(localClient, player_name);
            return localClient;
        }
        return null;
    }

    NetworkClient ConnectLocalClient()
    {
        network_address = "localhost";
        client = ClientScene.ConnectLocalServer();
        RegisterClientMessages(client);
        return client;
    }

    public void StopServer()
    {
        if (!NetworkServer.active)
            return;
        //OnServerStop();
        isNetworkActive = false;
        NetworkServer.Shutdown();
        //StopMatchMaker();
        if (offline_scene != null)
        {
            ServerChangeScene(offline_scene);
        }
    }

    public void StopClient()
    {
        //OnClientStop();
        isNetworkActive = false;
        if (client != null)
        {
            client.Disconnect();
            client.Shutdown();
            client = null;
        }
        //StopMatchMaker();
        ClientScene.DestroyAllClientObjects();
        if (offline_scene != "")
        {
            ClientSceneChange(offline_scene, false);
        }
    }

    public void StopHost()
    {
        //OnHostStop();

        StopServer();
        StopClient();
    }

    public bool IsClientConnected()
    {
        return client != null && client.isConnected;
    }

    public void FindSceneController()
    {
        game_controller = GameObject.Find("Controller").GetComponent<Controller>();
    }

    public void BeginGame()
    {
        game_controller.initialize(clients);
    }

    //---------------Changing Scenes for Server and Client-----------------
    public virtual void ServerChangeScene(string sceneName)
    {
        NetworkServer.SetAllClientsNotReady();
        network_scene_name = sceneName;
        StartCoroutine(AsyncLoad(sceneName));

        StringMessage msg = new StringMessage(network_scene_name);
        //NetworkServer.SendToAll(MsgType.Scene, msg);

        start_pos_index = 0;
        start_pos.Clear();
    }

    System.Collections.IEnumerator AsyncLoad(string scene_name)
    {
        loading_scene_async = SceneManager.LoadSceneAsync(scene_name);
        while (!loading_scene_async.isDone)
        {
            yield return null;
        }
        FindSceneController();
        yield return loading_scene_async;

    }

    internal void ClientSceneChange(string newSceneName, bool forcedReload)
    {
        if (newSceneName == network_scene_name && !forcedReload)
        {
            return;
        }
        StartCoroutine(AsyncLoad(newSceneName));
        network_scene_name = newSceneName;
    }

    //------------Internal Server Message Handlers-----------------
    internal void OnServerConnectInternal(NetworkMessage net_msg)
    {
        net_msg.conn.SetMaxDelay(max_delay);

        if (NetworkServer.connections.Count >= max_connect + 1)
        {
            if (network_scene_name != "" && network_scene_name != offline_scene)
            {
                StringMessage msg = new StringMessage(network_scene_name);
                NetworkServer.SendToAll(MsgType.Scene, msg);
            }
            BeginGame();
        }
    }

    internal void OnServerDisconnectInternal(NetworkMessage net_msg)
    {
        OnServerDisconnect(net_msg.conn);
    }

    internal void OnServerReadyMessageInternal(NetworkMessage net_msg)
    {
        OnServerReady(net_msg.conn);
    }

    internal void OnServerAddPlayerMessageInternal(NetworkMessage net_msg)
    {
        net_msg.ReadMessage(add_player_msg);

        if (add_player_msg.msgSize != 0)
        {
            var reader = new NetworkReader(add_player_msg.msgData);
            OnServerAddPlayer(net_msg.conn, add_player_msg.playerControllerId, reader);
        }
        else
        {
            OnServerAddPlayer(net_msg.conn, add_player_msg.playerControllerId);
        }
    }

    //-----------Internal Client Message Handler-----------------
    internal void OnClientConnectInternal(NetworkMessage net_msg)
    {
        net_msg.conn.SetMaxDelay(max_delay);
        if (String.IsNullOrEmpty(online_scene) || online_scene == offline_scene)
        {
            OnClientConnect(net_msg.conn);
        }
        else
        {
            client_ready_connect = net_msg.conn;
        }
    }

    internal void OnClientDisconnectInternal(NetworkMessage net_msg)
    {
        if (offline_scene != "")
        {
            ClientSceneChange(offline_scene, false);
        }
        OnClientDisconnect(net_msg.conn);
    }

    internal void OnClientSceneInternal(NetworkMessage net_msg)
    {
        string new_scene_name = net_msg.reader.ReadString();

        if (IsClientConnected() && !NetworkServer.active)
        {
            ClientSceneChange(new_scene_name, true);
            OnClientSceneChanged(net_msg.conn);
        }
    }

    //----------Server System Callbacks----------------------
    public virtual void OnServerConnect(NetworkConnection conn)
    {

    }

    public virtual void OnServerDisconnect(NetworkConnection conn)
    {
        NetworkServer.DestroyPlayersForConnection(conn);
    }

    public virtual void OnServerReady(NetworkConnection conn)
    {
        NetworkServer.SetClientReady(conn);
    }

    public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        OnServerAddPlayerInternal(conn, playerControllerId);
    }

    public virtual void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader msgReader)
    {
        OnServerAddPlayerInternal(conn, playerControllerId);
    }

    void OnServerAddPlayerInternal(NetworkConnection conn, short playerControllerId)
    {
        if (player_prefab == null)
        {
            return;
        }
        if (player_prefab.GetComponent<NetworkIdentity>() == null)
        {
            return;
        }
        if (playerControllerId < conn.playerControllers.Count && conn.playerControllers[playerControllerId].IsValid && conn.playerControllers[playerControllerId].gameObject != null)
        {
            return;
        }
        GameObject player;
        Transform start_pos = GetStartPosition();
        player = (GameObject)Instantiate(player_prefab, start_pos.position, start_pos.rotation);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    public Transform GetStartPosition()
    {
        return spawn_transform;
    }

    //------------Client System Callbacks---------------------
    public virtual void OnClientConnect(NetworkConnection conn)
    {
        if (String.IsNullOrEmpty(online_scene) || online_scene == offline_scene)
        {
            ClientScene.Ready(conn);
            if (auto_create_player)
            {
                ClientScene.AddPlayer(0);
            }
        }
    }

    public virtual void OnClientDisconnect(NetworkConnection conn)
    {
        StopClient();
    }

    public virtual void OnClientSceneChanged(NetworkConnection conn)
    {
        ClientScene.Ready(conn);

        if (!auto_create_player)
        {
            return;
        }

        bool add_player = (ClientScene.localPlayers.Count == 0);
        bool found_player = false;

        foreach (var playerController in ClientScene.localPlayers)
        {
            if (playerController.gameObject != null)
            {
                found_player = true;
                break;
            }
        }
        if (!found_player)
        {
            ClientScene.AddPlayer(0);
        }
    }

    //------------Start & Stop Callbacks----------------------
    public virtual void OnStartClient(NetworkClient client, string name)
    {
        clients.Add(client, name);

        ClientScene.RegisterPrefab(Resources.Load<GameObject>("Prefabs/GridFloorBlue"));
        ClientScene.RegisterPrefab(Resources.Load<GameObject>("Prefabs/GridFloorRed"));
    }
}

