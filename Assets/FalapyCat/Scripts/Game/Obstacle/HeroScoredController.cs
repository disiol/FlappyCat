using System;
using UnityEngine;

namespace FalapyCat.Scripts.Game.Obstacle
{
    public class HeroScoredController : MonoBehaviour
    {
        private GameObject _saunds;
        private GameObject _saundCoin;

        private void Awake()
        {
           _saunds = GameObject.Find("Sounds");
           _saundCoin = _saunds.transform.Find("Coin").gameObject;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log(" HeroScoredController OnTriggerEnter2D name " + col.gameObject.name);
            if (col.tag.Equals("Player"))
            {
                _saundCoin.SetActive(true);
                //If the bird hits the trigger collider in between the columns then
                //tell the game control that the bird scored.
                GameControl.instance.HeroScored();
            }
        }
    }
}