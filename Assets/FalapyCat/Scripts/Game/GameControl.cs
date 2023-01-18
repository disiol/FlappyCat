using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance;
   
    [Header("Sore"),SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject scorePanel;

    [Header("GameOver"),SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject gameOverTextPanel;
    public bool isGameOver = false; //Is the game over?
    [Header("ScrollingObject")]
    public float scrollSpeed = -1.5f;

    [Header("Background")]
    public float moveBackground;


    private int score = 0; //The player's score.
    private const string BestScore = "bestScore";
    private int lastBestScore = 0;


    void Awake()
    {
        //If we don't currently have a game control...
        if (instance == null)
            //...set this one to be it...
            instance = this;
        //...otherwise...
        else if (instance != this)
            //...destroy this one because it is a duplicate.
            Destroy(gameObject);
    }

    void Update()
    {
        //If the game is over and the player has pressed some input...
        if (isGameOver && Input.GetMouseButtonDown(0))
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScored()
    {
        //The bird can't score if the game is over.
        if (!isGameOver)
        {
            score++;
            //...and adjust the score text.
            scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
        }

        //If the game is not over, increase the score...
    }

    public void BirdDied()
    {
        SafeBestPoints();
        scorePanel.SetActive(false);

        gameOverTextPanel.SetActive(true);
        string textGamover = gameOverText.GetComponent<TextMeshProUGUI>().text;
        Debug.Log("textGamover = " + textGamover);
        gameOverText.GetComponent<TextMeshProUGUI>().text = String.Format(textGamover, score, lastBestScore);
        //Activate the game over text.

        isGameOver = true;

        //Set the game to be over.
    }

    private void SafeBestPoints()
    {
        if (PlayerPrefs.HasKey(BestScore))
        {
            lastBestScore = PlayerPrefs.GetInt(BestScore);
            if (lastBestScore < score)
            {
                PlayerPrefs.SetInt(BestScore, score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            lastBestScore = score;
            PlayerPrefs.SetInt(BestScore, score);
            PlayerPrefs.Save();
        }
    }
}