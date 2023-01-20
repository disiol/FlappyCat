using UnityEngine;

namespace FalapyCat.Scripts.Game.Background
{
    public class BackgroundSize : MonoBehaviour
    {
        private float backgroundLocalScaleY;

        // Start is called before the first frame update
        void Start()
        {
            if (Camera.main != null)
            {
                var height = Camera.main.orthographicSize * 2f;
                var width = height * Screen.width / Screen.height;

                if (gameObject.name == "Background")
                {
                    backgroundLocalScaleY = height -4;
                    transform.localScale = new Vector3(width, backgroundLocalScaleY , 1);
                }
               
            }
        }
    }
}