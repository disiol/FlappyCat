using UnityEngine;

namespace FalapyCat.Scripts.Game
{
    public class ColumnPool : MonoBehaviour 
    {
        public GameObject columnPrefab;                                    //The column game object.
        public int columnPoolSize = 5;                                    //How many columns to keep on standby.
        public float spawnRate = 3f;                                    //How quickly columns spawn.
        public float columnMin = -1f;                                    //Minimum y value of the column position.
        public float columnMax = 3.5f;                                    //Maximum y value of the column position.

        private GameObject[] _columns;                                    //Collection of pooled columns.
        private int _currentColumn = 0;                                    //Index of the current column in the collection.

        private Vector2 _objectPoolPosition = new Vector2 (-15,-25);        //A holding position for our unused columns offscreen.
        private float _spawnXPosition = 10f;

        private float _timeSinceLastSpawned;


        void Start()
        {
            _timeSinceLastSpawned = 0f;

            //Initialize the columns collection.
            _columns = new GameObject[columnPoolSize];
            //Loop through the collection... 
            for(int i = 0; i < columnPoolSize; i++)
            {
                //...and create the individual columns.
                _columns[i] = (GameObject)Instantiate(columnPrefab, _objectPoolPosition, Quaternion.identity);
            }
        }


        //This spawns columns as long as the game is not over.
        void Update()
        {
            _timeSinceLastSpawned += Time.deltaTime;

            if (GameControl.instance.isGameOver == false && _timeSinceLastSpawned >= spawnRate) 
            {    
                _timeSinceLastSpawned = 0f;

                //Set a random y position for the column
                float spawnYPosition = Random.Range(columnMin, columnMax);

                //...then set the current column to that position.
                _columns[_currentColumn].transform.position = new Vector2(_spawnXPosition, spawnYPosition);

                //Increase the value of currentColumn. If the new size is too big, set it back to zero
                _currentColumn ++;

                if (_currentColumn >= columnPoolSize) 
                {
                    _currentColumn = 0;
                }
            }
        }
    }
}