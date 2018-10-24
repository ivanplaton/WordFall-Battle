using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordSpawner : MonoBehaviour {

	public GameObject wordPrefab;
	public Transform wordCanvas;
    public Transform enemyCanvas;

    public WordDisplay SpawnRandomWord ()
	{
		Vector3 randomPosition = new Vector3(Random.Range(-4f, 4f), 4.5f);

        GameObject wordObj = Instantiate(wordPrefab, randomPosition, Quaternion.identity, wordCanvas);
        wordObj.transform.position = randomPosition;
		WordDisplay wordDisplay = wordObj.GetComponent<WordDisplay>();

		return wordDisplay;
	}
}
