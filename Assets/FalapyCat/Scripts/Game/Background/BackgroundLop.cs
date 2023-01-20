using UnityEngine;

namespace FalapyCat.Scripts.Game.Background
{
    public class BackgroundLop : MonoBehaviour
    {
        private float _moveBackgroundSpeed;

        private Vector2 offset = Vector2.zero;

        private Material material;

        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private GameControl gameControl;


        void Start()
        {
            UpdateSpeed();

            BackgroundLopGetTextureOffset();
        }

        public void UpdateSpeed()
        {
            gameControl = GameControl.instance;
            _moveBackgroundSpeed = gameControl.moveBackgroundSpeed;
        }

        private void BackgroundLopGetTextureOffset()
        {
            material = GetComponent<Renderer>().material;
            offset = material.GetTextureOffset(MainTex);
        }

        void Update()
        {
            // if (!gameControl.isGameOver)
            // {
            //     BackgroundLopGetTextureOffset();
            //     offset.x += _moveBackgroundSpeed * Time.deltaTime;
            // }
            // else
            // {
            //     BackgroundLopGetTextureOffset();
            //     offset.x = 0;
            // }
            BackgroundLopGetTextureOffset();
            offset.x += _moveBackgroundSpeed * Time.deltaTime;

            material.SetTextureOffset(MainTex, offset);
        }
    }
}