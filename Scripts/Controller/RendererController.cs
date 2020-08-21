using UnityEngine;

namespace Controller
{
    public class RendererController : MonoBehaviour
    {
        private static readonly Color LightGreen = new Color(0.1f, 0.9f, 0.1f);
        private static readonly Color DarkGreen = new Color(0, 0.25f, 0f); 
        private static readonly Color LightBlue = new Color(0, 0, 0.8f);
        private static readonly Color DarkBlue = new Color(0, 0, 0.4f);
        
        private static readonly int ColorPropertyName = Shader.PropertyToID("_Color");

        public static void ChangeColor(GameObject gameObjectToChangeColor, Color color)
        {
            gameObjectToChangeColor.GetComponent<Renderer>().material.SetColor(ColorPropertyName, color);
        }

        public static void SetColorToYellow(GameObject gameObjectToChangeColor)
        {
            ChangeColor(gameObjectToChangeColor, Color.yellow);
        }
        
        public static void SetColorToBlue(GameObject gameObjectToChangeColor, bool isDark)
        {
            ChangeColor(gameObjectToChangeColor, isDark ? DarkBlue : LightBlue);
        }

        public static void SetColorToGreen(GameObject gameObjectToChangeColor, bool isDark)
        {
            ChangeColor(gameObjectToChangeColor, isDark ? DarkGreen : LightGreen);
        }

        public static void SetColorToRed(GameObject gameObjectToChangeColor)
        {
            ChangeColor(gameObjectToChangeColor, Color.red);
        }
    }
}