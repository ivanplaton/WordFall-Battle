using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public int score = 120;

    public void ToGame()
    {
        SceneManager.LoadScene("SelectLevel");
    }
    public void ToServerMain()
    {
        Data.isNetwork = true;
        Data.isNetworkIdentityHost = true;
        SceneManager.LoadScene("ServerMain");
    }

    public void ToClientMain()
    {
        Data.isNetwork = true;
        Data.isNetworkIdentityHost = false;
        SceneManager.LoadScene("ClientMain");
    }

    public void ToSelectMode()
    {
        SceneManager.LoadScene("SelectMode");
    }

    public void ToMainMenu()
    {
        Data.isNetwork = false;
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
