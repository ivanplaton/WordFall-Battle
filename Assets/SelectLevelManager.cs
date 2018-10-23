using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelManager : MonoBehaviour {

    //INIT BUTTONS
    public Button btnMedium = null;
    public Button btnHard = null;
    public Button btnVeryHard = null;

    //INIT LOCK IMAGE
    public Image mediumLock = null;
    public Image hardLock = null;
    public Image veryhardLock = null;

    // Use this for initialization
    void Start () {
        btnMedium.enabled = false;
        btnHard.enabled = false;
        btnVeryHard.enabled = false;

        if (Data.HighScore >= 10000)
        {
            btnMedium.enabled = true;
            mediumLock.enabled = false;
        }

        if (Data.HighScore >= 25000)
        {
            btnHard.enabled = true;
            hardLock.enabled = false;
        }

        if (Data.HighScore >= 50000)
        {
            btnVeryHard.enabled = true;
            veryhardLock.enabled = false;
        }
	}

    public void ToEasyLevel()
    {
        Data.LevelCategory = "Easy";
        Data.StartGame = true;
        SceneManager.LoadScene("Main");
    }

    public void ToMediumLevel()
    {
        Data.LevelCategory = "Medium";
        Data.StartGame = true;
        SceneManager.LoadScene("Main");
    }

    public void ToHardLevel()
    {
        Data.LevelCategory = "Hard";
        Data.StartGame = true;
        SceneManager.LoadScene("Main");
    }

    public void ToVeryHardLevel()
    {
        Data.LevelCategory = "Final";
        Data.StartGame = true;
        SceneManager.LoadScene("Main");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
