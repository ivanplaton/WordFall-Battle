using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour {

	public GameObject wordPrefab;
	public Transform wordCanvas;
    public Transform enemyCanvas;

    public WordDisplay SpawnRandomWord ()
	{
		Vector3 randomPosition = new Vector3(Random.Range(-2.5f, 2.5f), 5f);

        GameObject wordObj = Instantiate(wordPrefab, randomPosition, Quaternion.identity, wordCanvas);
        wordObj.transform.position = randomPosition;
		WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

		return wordDisplay;
	}

    public WordDisplay SpawnEnemyWord()
    {
        Vector3 randomPosition = new Vector3(8.2f, 4f);

        GameObject wordObj = Instantiate(wordPrefab, randomPosition, Quaternion.identity, enemyCanvas);
        //wordObj.transform.SetParent(enemyCanvas.transform);
        wordObj.transform.position = randomPosition;

        Debug.Log("SpawnEnemyWord Displayed at: " + enemyCanvas + 
             "\n random pos at: " + randomPosition);
        WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

        return wordDisplay;
    }
}
