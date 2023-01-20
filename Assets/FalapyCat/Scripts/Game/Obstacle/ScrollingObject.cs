using UnityEngine;

namespace FalapyCat.Scripts.Game.Obstacle
{
    public class ScrollingObject : MonoBehaviour 
    {
        private float _scrollSpeed ;
        private Rigidbody2D _rb2d;
        private GameControl _gameControl;

        // Use this for initialization
        void Start () 
        {
            UpdateSpeed();

            //Get and store a reference to the Rigidbody2D attached to this GameObject.
            _rb2d = GetComponent<Rigidbody2D>();

            //Start the object moving.
            _rb2d.velocity = new Vector2 (_scrollSpeed, 0);
        }

        public void UpdateSpeed()
        {
            _gameControl = GameControl.instance;

            _scrollSpeed = _gameControl.scrollingColumScrollSpeed;
        }

        void Update()
        {
            // If the game is over, stop scrolling.
            if(_gameControl.isGameOver)
            {
                _rb2d.velocity = Vector2.zero;
            }
        }
    }
}