using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Data : MonoBehaviour {

    private static int _FinalScore;
    private static int _TimesTwo = 0;
    private static int _Combo = 0;
    private static int _EnemyScore = 0;

    private static bool _StartGame = false;
    private static bool _isClient = false;
    private static bool _isNetwork = false;
    private static bool _isConnectedOnNetwork = false;
    private static bool _isNetworkIdentityHost = false;

    private static string _LevelCategory;
    private static string _PlayerID;

    public static int Score
    {
        get
        {
            return _FinalScore;
        }
        set
        {
            _FinalScore = value;
        }
    }

    public static int EnemyScore
    {
        get
        {
            return _EnemyScore;
        }
        set
        {
            _EnemyScore = value;
        }
    }

    public static int TwoMultiplier
    {
        get
        {
            return _TimesTwo;
        }
        set
        {
            _TimesTwo = value;
        }
    }

    public static int Combo
    {
        get
        {
            return _Combo;
        }
        set
        {
            _Combo = value;
        }
    }

    public static string LevelCategory
    {
        get
        {
            return _LevelCategory;
        }
        set
        {
            _LevelCategory = value;
        }
    }

    public static bool StartGame
    {
        get
        {
            return _StartGame;
        }
        set
        {
            _StartGame = value;
        }
    }

    public static int HighScore
    {
        get
        {
            string destination = Application.persistentDataPath + "/save.dat";
            //string destination = "C:/Users/pb6n0064/Desktop" + "/save.dat";
            FileStream file;
            if (File.Exists(destination)) file = File.OpenRead(destination);
            else
            {
                Debug.LogError("File not found");
                return 0;
            }

            BinaryFormatter bf = new BinaryFormatter();
            HighScoreData hd = (HighScoreData)bf.Deserialize(file);
            file.Close();

            return hd.Highscore;
        }
        set
        {
            HighScoreData hd = new HighScoreData();
            hd.Highscore = value;

            string destination = Application.persistentDataPath + "/save.dat";
            //string destination = "C:/Users/pb6n0064/Desktop" + "/save.dat";
            FileStream file;

            if (File.Exists(destination)) file = File.OpenWrite(destination);
            else file = File.Create(destination);

            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(file, hd);
            file.Close();
        }
    }

    //FOR NETWORK SETTINGS
    public static string PlayerID
    {
        get
        {
            return _PlayerID;
        }
        set
        {
            _PlayerID = value;
        }
    }

    public static bool isClient
    {
        get
        {
            return _isClient;
        }
        set
        {
            _isClient = value;
        }
    }

    public static bool isNetwork
    {
        get
        {
            return _isNetwork;
        }
        set
        {
            _isNetwork = value;
        }
    }

    public static bool isConnectedOnNetwork
    {
        get
        {
            return _isConnectedOnNetwork;
        }
        set
        {
            _isConnectedOnNetwork = value;
        }
    }

    public static bool isNetworkIdentityHost
    {
        get
        {
            return _isNetworkIdentityHost;
        }
        set
        {
            _isNetworkIdentityHost = value;
        }
    }
}

[Serializable]
class HighScoreData
{
    public int Highscore;
}
