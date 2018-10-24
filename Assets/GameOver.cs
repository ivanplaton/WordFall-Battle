using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text txtScore = null;
    public Text txtSecondContent = null;
    public Text txtWinner = null;

    public Image win_Image = null;
    public Image lose_Image = null;

    // Use this for initialization
    void Start () {
        if (!MusicManager.Instance.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            MusicManager.Instance.gameObject.GetComponent<AudioSource>().Play();
        }

        win_Image.enabled = false;
        lose_Image.enabled = false;

        if (Data.isNetwork)
        {
            txtScore.text = "Your Score: " + Data.Score;
            txtSecondContent.text = "Enemy Score: " + Data.EnemyScore;

            if (Data.Score > Data.EnemyScore)
            {
                win_Image.enabled = true;
            }
            else if (Data.Score < Data.EnemyScore)
            {
                lose_Image.enabled = true;
            }
            else
            {
                win_Image.enabled = true;
                txtWinner.text = "Draw!";
            }

        }
        else
        {
            txtScore.text = "Score: " + Data.Score.ToString();

            if (Data.Score > Data.HighScore)
            {
                txtWinner.text = "New High Score!";
                Data.HighScore = Data.Score;
            }

            txtSecondContent.text = "High Score: " + Data.HighScore.ToString();

        }

	}

    public void PlayAgain()
    {
        Data.isNetwork = false;
        Data.isConnectedOnNetwork = false;
        Data.StartGame = false;
        Data.Score = 0;
        Data.EnemyScore = 0;
        Data.Combo = 0;
        Data.TwoMultiplier = 0;

        SceneManager.LoadScene("MenuScene");
    }
	
}
