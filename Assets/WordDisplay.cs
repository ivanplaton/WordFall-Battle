using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordDisplay : MonoBehaviour {

	public Text text;
    public bool isDestroyed = false;
    public float fallSpeed = 1f;
    private float deltaSpeed = 0.02f;

    public void SetWord (string word)
	{
		text.text = word;
        isDestroyed = false;

    }

	public void RemoveLetter ()
	{
        if (gameObject != null)
        {
            text.text = text.text.Remove(0, 1);
            text.color = Color.red;
        }
        else
        {
            isDestroyed = true;
        }
	}

	public void RemoveWord ()
	{
		Destroy(gameObject);
	}

    public void setSpeedIncrease(float _increase)
    {
        deltaSpeed += 1f;
    }

    private void Update()
	{
		transform.Translate(0f, -fallSpeed * deltaSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(gameObject.name + " collided2D with " + col.gameObject.name);
        //Debug.Log("Word " + text.text + " is destroyed");
        if (gameObject.name == "Word(Clone)" || gameObject.name == "WordEnemy(Clone)")
        {
            //Debug.Log("Word " + gameObject.GetComponent<Text>().text + " is destroyed");
            Destroy(gameObject);
            isDestroyed = true;
            if (!Data.isNetwork)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }
}
