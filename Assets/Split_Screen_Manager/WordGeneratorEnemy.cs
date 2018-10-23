using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordGeneratorEnemy : MonoBehaviour {

    private static string[] EnemyWordArray;
    private static List<string> EnemyWordList;


    public void AddWordToEnemyList(string word)
    {
        Debug.Log("Word to be added: " + word);
        EnemyWordList.Add(word);
        Debug.Log("List count- " + EnemyWordList.Count);
    }

    public static string GetWord()
    {
        string lastWord = "";
        if (EnemyWordList.Count > 1)
            lastWord = EnemyWordList[EnemyWordList.Count - 1];

        return lastWord;

    }
}
