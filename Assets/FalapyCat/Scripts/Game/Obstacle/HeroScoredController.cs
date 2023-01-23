using UnityEngine;

namespace FalapyCat.Scripts.Game.Obstacle
{
    public class HeroScoredController : MonoBehaviour 
    {
        void OnTriggerEnter2D(Collider2D other)
        {
            if(other.GetComponent<Hero>() != null)
            {
                //If the bird hits the trigger collider in between the columns then
                //tell the game control that the bird scored.
                GameControl.instance.HeroScored();
            }
        }
    }
}