using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWordManager : MonoBehaviour {

    public Word wordEnemy;
    public WordSpawner wordSpawner;


    public void AddWord()
    {
        Debug.Log("Add word enemy:" + Data.EnemyWords);
        Word word = new Word(Data.EnemyWords, wordSpawner.SpawnWord());
    }
}
