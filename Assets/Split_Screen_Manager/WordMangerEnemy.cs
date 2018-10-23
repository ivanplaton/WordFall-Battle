using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMangerEnemy : MonoBehaviour {

    public List<Word> words;

    public WordSpawner wordSpawner;

    public void AddWord()
    {
        Word word = new Word(WordGeneratorEnemy.GetWord(), wordSpawner.SpawnWord());
        words.Add(word);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
