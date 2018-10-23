using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionTester : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(gameObject.name + " collided2D with " + col.gameObject.name);
        if (col.gameObject.name == "Word(Clone)")
        {
            Destroy(col.gameObject);
            if(!Data.isNetwork)
            {
               //SceneManager.LoadScene("GameOver");
            }
        }
    }
}
