using UnityEngine;

namespace FalapyCat.Scripts.Game.Background
{
    public class BackgroundLop : MonoBehaviour
    {
        private float _moveBackgroundSpeed;

        private Vector2 _offset = Vector2.zero;

        private Material _material;

        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private GameControl _gameControl;


        void Start()
        {
            UpdateSpeed();

            BackgroundLopGetTextureOffset();
        }

        public void UpdateSpeed()
        {
            _gameControl = GameControl.instance;
            _moveBackgroundSpeed = _gameControl.moveBackgroundSpeed;
        }

        private void BackgroundLopGetTextureOffset()
        {
            _material = GetComponent<Renderer>().material;
            _offset = _material.GetTextureOffset(MainTex);
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
            _offset.x += _moveBackgroundSpeed * Time.deltaTime;

            _material.SetTextureOffset(MainTex, _offset);
        }
    }
}