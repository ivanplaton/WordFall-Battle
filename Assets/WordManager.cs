using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WordManager : MonoBehaviour {

	public List<Word> words;

	public WordSpawner wordSpawner;

	private bool hasActiveWord;
	private Word activeWord;

    public Text txtScore = null;
    public Text txtTimer = null;
    public Text txtComboCount = null;
    public Text TxtLevel = null;

    float timer = 120.0f;

    //SCORE VARIABLES
    private int score = 0;
    private int scoreIncrement = 10;

    //PARTICLES
    public ParticleSystem explosion, combo;

    public void AddWord ()
	{
		Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
		words.Add(word);
        Debug.Log("data.enemy word:" + Data.EnemyWords);
        if (explosion.isPlaying)
        {
            explosion.Stop();
        }

        if (combo.isPlaying)
        {
            combo.Stop();
        }

    }

	public void TypeLetter (char letter)
    {
        if (hasActiveWord)
		{
            if (activeWord.GetNextLetter() == letter)
			{
				activeWord.TypeLetter();
			}
            else
            {
                Data.TwoMultiplier = 0;
                Data.Combo = 0;
                txtComboCount.text = "0";
            }
		} else
		{
            foreach (Word word in words)
			{
				if (word.GetNextLetter() == letter)
				{
					activeWord = word;
					hasActiveWord = true;
					word.TypeLetter();
					break;
				}
			}
		}
        if (activeWord.isActiveWordDestroyed())
        {
            hasActiveWord = false;
            words.Remove(activeWord);
            Data.TwoMultiplier = 0;
            Data.Combo = 0;
        }

        if (hasActiveWord && activeWord.WordTyped())
		{
			hasActiveWord = false;
			words.Remove(activeWord);
            setScore(scoreIncrement);
            Data.Score = score;
        }
	}

    void Start()
    {
        MusicManager.Instance.gameObject.GetComponent<AudioSource>().Stop();
        TxtLevel.text = "LEVEL: " + Data.LevelCategory;
    }

    void Update()
    {
        if (Data.isNetwork)
        {
            if (Data.StartGame)
            {
                timer -= Time.deltaTime;
                int dispTime = (int)timer;
                string minutes = Mathf.Floor(timer / 60).ToString("00");
                string seconds = (timer % 60).ToString("00");
                txtTimer.text = minutes + ":" + seconds;

                if (timer <= 0)
                {
                    SceneManager.LoadScene("GameOver");
                }
            }
        }
    }

    private void setScore(int _score)
    {
        if (Data.TwoMultiplier < 5)
        {
            score += _score;
            Data.TwoMultiplier += 1;
        }
        else if (Data.TwoMultiplier == 5)
        {

            if (Data.Combo == 3)
            {
                score += (score / 2) + 1000;

                combo.Play();
                Data.Combo = 0;
                txtComboCount.text = "0";
            } 
            else
            {
                score += (score / 2);

                Data.Combo += 1;
                txtComboCount.text = Data.Combo.ToString();
                explosion.Play();
            }

            Data.TwoMultiplier = 0;

        }
        else
        {
            score += _score;
        }

        txtScore.text = "Score: " + score.ToString();
    }

    public int getScore()
    {
        return score;
    }

    public void setScoreIncrement(int value)
    {
        scoreIncrement = value;
    }

}
