using UnityEngine;

namespace FalapyCat.Scripts.Game
{
    public class Hero : MonoBehaviour
    {
        [Header("Player"), SerializeField] private GameObject player;

        public float upForce; //Upward force of the "flap".
        private bool isDead = false; //Has the player collided with a wall?

        private Animator anim; //Reference to the Animator component.
        private Rigidbody2D rb2d; //Holds a reference to the Rigidbody2D component of the bird.
        private GameControl _gameControl;
        
        private static readonly int Flap = Animator.StringToHash("Flap");
        private static readonly int Die = Animator.StringToHash("Die");

        void Start()
        {
            _gameControl = GameControl.instance;

            //Get reference to the Animator component attached to this GameObject.
            anim = player.GetComponent<Animator>();
            //Get and store a reference to the Rigidbody2D attached to this GameObject.
            rb2d =  player.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            //Don't allow control if the bird has died.
            if (isDead == false)
            {
                //Look for input to trigger a "flap".
                if (Input.GetMouseButtonDown(0))
                {
                    //...tell the animator about it and then...
                    anim.SetTrigger(Flap);
                    //...zero out the birds current y velocity before...
                    rb2d.velocity = Vector2.zero;
                    //    new Vector2(rb2d.velocity.x, 0);
                    //..giving the bird some upward force.
                    rb2d.AddForce(new Vector2(0, upForce));
                }
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            // Zero out the bird's velocity
            rb2d.velocity = Vector2.zero;
            // If the bird collides with something set it to dead...
            Debug.Log(" Hero OnCollisionEnter2D name " + other.gameObject.name);

            if (other.gameObject.CompareTag("Wall"))
            {
                Debug.Log(" Hero OnCollisionEnter2D other.gameObject.CompareTag = Wall");

                CheckHeroDie();
            }
        }

        public void CheckHeroDie()
        {
            _gameControl.live--;
            if (_gameControl.live == 0)
            {
                isDead = true;
                //...tell the Animator about it...
                anim.SetTrigger(Die);
                //...and tell the game control about it.
                _gameControl.HeroDie();
            }
            else if (_gameControl.live > 0)
            {
                //TODO  pause how window ads
            }
        }
    }
}