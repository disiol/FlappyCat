using System;
using UnityEngine;

namespace FalapyCat.Scripts.Game.Obstacle
{
    public class CollisionWithObstacle : MonoBehaviour
    {
        private GameObject _gameControllerObject;

        private void Awake()
        {
            this._gameControllerObject = GameObject.Find("GameController");
        }

         void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log(" CollisionWithObstacle OnCollisionEnter2D name " + col.gameObject.name);
        
            if (col.gameObject.CompareTag("Player"))
            {
                this._gameControllerObject.GetComponent<Hero>().CheckHeroDie();
            }
        }
         
    }
}