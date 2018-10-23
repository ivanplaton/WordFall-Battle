using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordTimer : MonoBehaviour {

	public WordManager wordManager;
    MyNetManager networkManager;

    public float wordDelay = 2.0f;
	private float nextWordTime = 0f;

    private void Update()
    {
        if (Data.isNetwork)
        {
            if (wordManager.getScore() >= 1500 && wordManager.getScore() < 10000)
            {
                wordManager.setScoreIncrement(20);
                WordGenerator.setCategory("Medium");
                wordDelay = 1.5f;
            }
            else if (wordManager.getScore() >= 10000 && wordManager.getScore() < 25000)
            {
                wordManager.setScoreIncrement(40);
                WordGenerator.setCategory("Hard");
                wordDelay = 1.4f;
            }
            else if (wordManager.getScore() >= 25000)
            {
                wordManager.setScoreIncrement(100);
                WordGenerator.setCategory("Final");
                wordDelay = 1.2f;
            }
        }
        else
        {
            string category = Data.LevelCategory;
            switch(category)
            {
                case "Easy":
                    wordManager.setScoreIncrement(10);
                    WordGenerator.setCategory("Easy");
                    wordDelay = 2.0f;
                    break;
                case "Medium":
                    wordManager.setScoreIncrement(20);
                    WordGenerator.setCategory("Medium");
                    wordDelay = 1.5f;
                    break;
                case "Hard":
                    wordManager.setScoreIncrement(40);
                    WordGenerator.setCategory("Hard");
                    wordDelay = 1.4f;
                    break;
                case "Final":
                    wordManager.setScoreIncrement(100);
                    WordGenerator.setCategory("Final");
                    wordDelay = 1.2f;
                    break;
                default:
                    break;
            }
        }


        if (Time.time >= nextWordTime)
        {
            if (Data.StartGame)
            {
                wordManager.AddWord();
                nextWordTime = Time.time + wordDelay;
            }

            if (Data.isNetwork)
            {
                initNetworkMessage();
            }

        }
    }

    private void Start()
    {
        if (Data.isNetwork)
        {
            networkManager = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<MyNetManager>();
        }
    }

    private void initNetworkMessage()
    {
        if (Data.isConnectedOnNetwork)
        {
            if (Data.isClient)
            {
                networkManager.sendMessageToServer();
            }
            else
            {
                networkManager.sendMessageToClient(Data.PlayerID);
            }
        }
    }

}

