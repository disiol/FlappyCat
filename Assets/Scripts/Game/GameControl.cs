using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    public static GameControl instance; //A reference to our game control script so we can access it statically.
    public GameObject scoreText; //A reference to the UI text component that displays the player's score.
    public GameObject gameOverText; //A reference to the UI text component that displays the player's score.
    public GameObject gameOverTextPanel; //A reference to the UI text component that displays the player's score.
    public GameObject scorePanel; //A reference to the object that displays the text which appears when the player dies.

  
    private int score = 0; //The player's score.
    public bool gameOver = false; //Is the game over?
    public float scrollSpeed = -1.5f;
    private string bestScore = "bestScore";
    private int lastrBestScore = 0;


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
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            //...reload the current scene.
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void BirdScored()
    {
        //The bird can't score if the game is over.
        if (!gameOver)
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
        gameOverText.GetComponent<TextMeshProUGUI>().text = String.Format(textGamover, score, lastrBestScore);
        //Activate the game over text.

        gameOver = true;

        //Set the game to be over.
    }

    private void SafeBestPoints()
    {
        if (PlayerPrefs.HasKey(bestScore))
        {
            lastrBestScore = PlayerPrefs.GetInt(bestScore);
            if (lastrBestScore < score)
            {
                PlayerPrefs.SetInt(bestScore, score);
                PlayerPrefs.Save();
            }
        }
        else
        {
            lastrBestScore = score;
            PlayerPrefs.SetInt(bestScore, score);
            PlayerPrefs.Save();
        }
    }
}