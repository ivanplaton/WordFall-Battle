using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTmerEnemy : MonoBehaviour
{

    public WordManager wordManagerEnemy;
    public float wordDelay = 2.0f;
    private float nextWordTime = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Data.EnemyLevel == "Easy")
        {
            wordDelay = 1.5f;
        }
        else if (Data.EnemyLevel == "Hard")
        {
            wordDelay = 1.4f;
        }
        else if (Data.EnemyLevel == "Final")
        {
            wordDelay = 1.2f;
        }

        if (Time.time >= nextWordTime)
        {
            wordManagerEnemy.AddWord();
            nextWordTime = Time.time + wordDelay;
        }
    }
}
