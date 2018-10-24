using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    //Play Global
    private static MusicManager instance = null;
    public static MusicManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
    //Play Gobal End

    // Update is called once per frame
    void Update()
    {
        if (!Data.MuteSound)
        {
            if (!MusicManager.Instance.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                MusicManager.Instance.gameObject.GetComponent<AudioSource>().Play();
            }
        }
        else
        {
            if (MusicManager.Instance.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                MusicManager.Instance.gameObject.GetComponent<AudioSource>().Stop();
            }
        }
    }
}
