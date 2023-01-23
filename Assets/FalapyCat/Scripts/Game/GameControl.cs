using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace FalapyCat.Scripts.Game
{
    public class GameControl : MonoBehaviour
    {
        public static GameControl instance;
       

        [Header("Sore"), SerializeField] private GameObject scoreText;
        [SerializeField] private GameObject scorePanel;

        [Header("Live"), SerializeField] public int live;

        [Header("GameOver"), SerializeField] private GameObject gameOverText;
        [SerializeField] private GameObject gameOverTextPanel;
        public bool isGameOver = false; //Is the game over?

        [Header("Background")] public float moveBackgroundSpeed;


        private int _score = 0; //The player's score.
        private const string BestScore = "bestScore";
        private int _lastBestScore = 0;
       
        [FormerlySerializedAs("scrollingObjectScrollSpeed")] public float scrollingColumScrollSpeed;
       
        private GameObject _saunds;
        private GameObject _saundDead;


        void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
            
            _saunds = GameObject.Find("Sounds");
            _saundDead = _saunds.transform.Find("Dead").gameObject;
        }

        void Update()
        {
            IsGameOver();
        }

        private void IsGameOver()
        {
            IfTheGameIsOverAndThePlayerHasPressedSomeInput();
        }

        private void IfTheGameIsOverAndThePlayerHasPressedSomeInput()
        {
            if (isGameOver && Input.GetMouseButtonDown(0))
            {
                ReloadTheCurrentScene();
            }
        }

        private static void ReloadTheCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void HeroScored()
        {
            //The bird can't score if the game is over.
            if (!isGameOver)
            {
                _score++;
                //...and adjust the score text.
                scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + _score;
            }

            //If the game is not over, increase the score...
        }

        public void HeroDie()
        {
            SafeBestPoints();
            scorePanel.SetActive(false);

            gameOverTextPanel.SetActive(true);
            string textGamover = gameOverText.GetComponent<TextMeshProUGUI>().text;
            Debug.Log("textGamover = " + textGamover);
            gameOverText.GetComponent<TextMeshProUGUI>().text = String.Format(textGamover, _score, _lastBestScore);
            //Activate the game over text.

            isGameOver = true;
            _saundDead.SetActive(true);

            //Set the game to be over.
        }

        private void SafeBestPoints()
        {
            if (PlayerPrefs.HasKey(BestScore))
            {
                _lastBestScore = PlayerPrefs.GetInt(BestScore);
                if (_lastBestScore < _score)
                {
                    PlayerPrefs.SetInt(BestScore, _score);
                    PlayerPrefs.Save();
                }
            }
            else
            {
                _lastBestScore = _score;
                PlayerPrefs.SetInt(BestScore, _score);
                PlayerPrefs.Save();
            }
        }
    }
}