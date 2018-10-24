using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    public int score = 120;

    public Toggle checkBox = null;

    void Start()
    {

        if (Data.MuteSound)
        {
            if (checkBox != null)
            {
                checkBox.isOn = false;
            }
        }

        if (!MusicManager.Instance.gameObject.GetComponent<AudioSource>().isPlaying)
        {
            MusicManager.Instance.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void Mute_Unmute()
    {
        if (checkBox.isOn)
        {
            Data.MuteSound = false;
            if (!MusicManager.Instance.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                MusicManager.Instance.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            Data.MuteSound = true;
            if (MusicManager.Instance.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                MusicManager.Instance.gameObject.GetComponent<AudioSource>().Stop();
            }
        }
    }

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
