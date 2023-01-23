using UnityEngine;

namespace FalapyCat.Scripts.Game.Obstacle
{
    public class CollisionWithObstacle : MonoBehaviour
    {
        private GameObject _gameControllerObject;

        private void Awake()
        {
            this._gameControllerObject = GameObject.Find("GameControllerObject");
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            var collider2D1 = other;
            if (collider2D1.gameObject.CompareTag("Player"))
            {
                 this._gameControllerObject.GetComponent<Hero>().CheckHeroDie();
            }
        }
    }
}