using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

/// <summary>
/// The custom networkmanager derived from 'NetworkManager'.
/// you can override virtual function to implement customize behaviour.
/// </summary>
public class MyNetManager : NetworkManager
{
    NetworkClient myClient;
    public string address;
    public string uid;
    public NetworkDiscovery discovery;
    public ListManager listManager;
    PlayerManager playerManager;
    public Text receivedText;
    public InputField inputfield;
    public GameObject waitingPrefab;
    public ParticleSystem waitingParticle;
    public Text txtEnemyScore;
    private GameObject gameObjectNetorkManager;

    private GameObject broadcaster;
    private GameObject checkerObject;

    void Start()
    {
        address = null;
        uid = SystemInfo.deviceUniqueIdentifier;
        playerManager = PlayerManager.Instance;
        myClient = new NetworkClient();

        checkerObject = GameObject.Find("NetworkManager");
        if (checkerObject != null)
            Destroy(checkerObject);

        gameObjectNetorkManager = GameObject.Find("NetworkManager");
        broadcaster = GameObject.Find("Broadcaster");
        NetworkTransport.Shutdown();
        initNetwork();
    }

    public void initNetwork()
    {
        if (!waitingPrefab.activeSelf)
        {
            waitingPrefab.SetActive(true);
        }
        waitingParticle.Play();
        if (/*"ClientMain" == SceneManager.GetActiveScene().name*/ !Data.isNetworkIdentityHost)
        {
            Debug.Log("is Network Identity Host - " + Data.isNetworkIdentityHost);
            Data.isClient = true;
            SetupClient();
        }
        else
        {
            Debug.Log("is Network Identity Host - " + Data.isNetworkIdentityHost);
            this.SetupServer();
        }
        Debug.Log("Init Network Connected on network - " + Data.isConnectedOnNetwork);
    }

    /** Declare NetworkMessage class **/
    public class UidMessage : MessageBase
    {
        public string uid;
    };

    public class CustomMessage : MessageBase
    {
        public string text;
        public string EnemyWord;
        public string score;
    };

    public class MyMsgType
    {
        // Unique device id
        public static short UID = MsgType.Highest + 1;
        // Your custom message type
        public static short Custom = MsgType.Highest + 2;
    };

    /** Callback functions **/
    public void OnConnected(NetworkMessage netMsg)
    {
        Debug.Log("Connected :" + netMsg.conn.address);
        if (waitingParticle.isPlaying)
        {
            waitingParticle.Stop();
            waitingPrefab.SetActive(false);
            Data.StartGame = true;
        }

        Data.isConnectedOnNetwork = true;
        // if started as client
        if (!NetworkServer.active)
        {
            sendUid();
            discovery.StopBroadcast();
        }

        Debug.Log("OnConnected on network - " + Data.isConnectedOnNetwork);
    }

    public void OnDisconnected(NetworkMessage netMsg)
    {
        Debug.Log("Disconnected :" + netMsg.conn.address);

        if (!discovery.running)
        {
            myClient.ReconnectToNewHost(address, 4444);
        }

        if (NetworkServer.active)
        {
            Debug.Log("Disconnected :" + netMsg.conn.connectionId);
            string disconnectedUid = playerManager.getPlayerUidByConnID(netMsg.conn.connectionId);
            if (null != disconnectedUid)
            {
                listManager.displayConnectionState(disconnectedUid, false);
            }

            playerManager.setPlayerOffline(netMsg.conn.connectionId);
        }
    }

    /// <summary>
    /// Called when UidMessage has received.
    /// </summary>
    /// <param name="netMsg">A network message object</param>
    public void OnUID(NetworkMessage netMsg)
    {
        UidMessage msg = netMsg.ReadMessage<UidMessage>();
        Debug.Log("OnUID " + msg.uid);
        Data.PlayerID = msg.uid;

        // If UID already exsist, do not add new player
        if (playerManager.isExsistUID(msg.uid))
        {
            // Assign new connection id for re-connected client.
            playerManager.setPlayerConnId(netMsg.conn.connectionId, msg.uid);
            string connectedUid = playerManager.getPlayerUidByConnID(netMsg.conn.connectionId);
            listManager.displayConnectionState(connectedUid, true);
        }
        else
        {
            // New connection
            playerManager.addPlayer(netMsg.conn.connectionId, msg.uid); //
            listManager.addItem(msg.uid);
        }
    }

    /// <summary>
    /// Called when custom network message has received.
    /// </summary>
    /// <param name="netMsg">A network message object</param>
    public void OnCustomMessage(NetworkMessage netMsg)
    {
        CustomMessage msg = netMsg.ReadMessage<CustomMessage>();
        Debug.Log("On Custom Message Text: " + msg.text +
                 "\nOn Custom Message WOrd: " + msg.EnemyWord +
                 "\n Network is host: " + Data.isNetworkIdentityHost);

        if (NetworkServer.active)
        {
            string uid = playerManager.getPlayerUidByConnID(netMsg.conn.connectionId);
            uid = uid.Substring(0, 10);
            if (/*"ClientMain" != SceneManager.GetActiveScene().name*/ Data.isNetworkIdentityHost)
            {
                //receivedText.text += msg.text = "[" + uid + "...]:" + msg.text + "\n";
                Data.EnemyScore = Int32.Parse(msg.text);
                Debug.Log("Receieved word from enemy: " + msg.EnemyWord);
                txtEnemyScore.text = "Enemy: " + msg.text;
                NetworkServer.SendToAll(MyMsgType.Custom, msg);
            }

            if (msg.text.Contains("S"))
            {
                string[] myStringSplit = msg.text.Split('-');
                Data.EnemyScore = Int32.Parse(myStringSplit[1]);
                txtEnemyScore.text = "Enemy: " + myStringSplit[1];
            }
        }
        else
        {
            if (msg.text.Contains("S"))
            {
                string[] myStringSplit = msg.text.Split('-');
                Data.EnemyScore = Int32.Parse(myStringSplit[1]);
                txtEnemyScore.text = "Enemy: " + myStringSplit[1];
            }
            //receivedText.text += msg.text;
        }

    }

    /// <summary>
    /// Check if client is currently connected.
    /// </summary>
    /// <returns>returns true if client is connected.</returns>
    public bool isConnected()
    {
        return myClient.isConnected;
    }

    /// <summary>
    /// Send unique device identifier to server to identify this client.
    /// </summary>
    public void sendUid()
    {
        UidMessage msg = new UidMessage();
        msg.uid = uid;

        myClient.Send(MyMsgType.UID, msg);
    }


    /// <summary>
    /// Send raw network message to client
    /// </summary>
    /// <param name="uid">A unique device identifier.</param>
    public void sendMessageToClient(string uid)
    {
        CustomMessage msg = new CustomMessage();
        int connID = -1;
        msg.text = "S-" + Data.Score.ToString();

        PlayerManager.playerData playerData = playerManager.getPlayerByUid(uid);
        connID = playerData.ConnID;
        Debug.Log("Send to Client - " + msg + " - " + connID.ToString());
        NetworkServer.SendToClient(connID, MyMsgType.Custom, msg);
    }

    /// <summary>
    /// Send raw network message to server
    /// </summary>
    /// <param name="text">A message text will sended.</param>
    public void sendMessageToServer()
    {
        CustomMessage msg = new CustomMessage();
        //msg.text = inputfield.text;
        msg.text = Data.Score.ToString();
        msg.EnemyWord = Data.EnemyWords;
        inputfield.text = "";
        Debug.Log("Send to Server - " + msg);
        myClient.Send(MyMsgType.Custom, msg);

    }

    /// <summary>
    /// Start as server and if discovery not running, start broadcast.
    /// </summary>
    public void SetupServer()
    {
        if (!NetworkServer.active)
        {
            Debug.Log("SetupServer( )");
            ConnectionConfig config = new ConnectionConfig();
            config.AddChannel(QosType.ReliableSequenced);
            config.AddChannel(QosType.Unreliable);
            NetworkServer.Configure(config, 1000);

            NetworkServer.Listen(4444);
            NetworkServer.RegisterHandler(MsgType.Connect, OnConnected);
            NetworkServer.RegisterHandler(MsgType.Disconnect, OnDisconnected);
            NetworkServer.RegisterHandler(MyMsgType.UID, OnUID);
            NetworkServer.RegisterHandler(MyMsgType.Custom, OnCustomMessage);
        }

        if (!discovery.running)
        {
            discovery.Initialize();
            discovery.StartAsServer();
        }
    }

    /// <summary>
    /// Start as client and if discovery not running, start broadcast receive mode.
    /// </summary>
    public void SetupClient()
    {
        Debug.Log("SetupClient()");

        ConnectionConfig config = new ConnectionConfig();
        config.AddChannel(QosType.ReliableSequenced);
        config.AddChannel(QosType.Unreliable);
        myClient.Configure(config, 1000);

        discovery.Initialize();
        discovery.StartAsClient();

        // Register message event handler
        myClient.RegisterHandler(MsgType.Connect, OnConnected);
        myClient.RegisterHandler(MsgType.Disconnect, OnDisconnected);
        myClient.RegisterHandler(MyMsgType.Custom, OnCustomMessage);
    }

    /// <summary>
    /// Connect to server with IP address.
    /// </summary>
    /// <param name="givenAddress">An IP address trying to connect</param>
    public void ConnectToServer(string givenAddress)
    {
        if (null == address) { address = givenAddress; }
        myClient.Connect(givenAddress, 4444);
    }

    /// <summary>
    /// Initialize NetworkServer object and Stop listening on port.
    /// </summary>
    public void DisableServer()
    {
        Debug.Log("StopServer");
        if (NetworkServer.active)
        {
            NetworkServer.ClearHandlers();
            NetworkServer.Reset();
            NetworkServer.Shutdown();
        }

        //if (discovery.running)
        //{
        //    discovery.StopBroadcast();
        //}
        
    }

    public void DisableClient()
    {
        Debug.Log("StopClient");
        //myClient.Disconnect();
        //myClient.Shutdown();
        Debug.Log("StopClient234");

        //if (discovery.running)
        //{
        //    discovery.StopBroadcast();   
        //}
    }


    public void onDisconnectClick()
    {
        Data.isNetwork = false;
        Data.isConnectedOnNetwork = false;
        Data.StartGame = false;
        Data.Score = 0;
        Data.EnemyScore = 0;
        Data.Combo = 0;
        Data.TwoMultiplier = 0;

        if ("ClientMain" == SceneManager.GetActiveScene().name)
        {
            this.DisableClient();
        }
        else
        {
            this.DisableServer();
        }

        SceneManager.LoadScene("MenuScene");
    }

}