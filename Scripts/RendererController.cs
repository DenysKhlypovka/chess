using UnityEngine;

public class RendererController : MonoBehaviour
{
    private static readonly int ColorPropertyName = Shader.PropertyToID("_Color");

    public static void ChangeColor(GameObject gameObjectToChangeColor, Color color)
    {
        gameObjectToChangeColor.GetComponent<Renderer>().material.SetColor(ColorPropertyName, color);
    }

    public static void SetColorToYellow(GameObject gameObjectToChangeColor)
    {
        ChangeColor(gameObjectToChangeColor, Color.yellow);
    }

    public static void SetColorToBlue(GameObject gameObjectToChangeColor)
    {
        ChangeColor(gameObjectToChangeColor, Color.blue);
    }

    public static void SetColorToGreen(GameObject gameObjectToChangeColor)
    {
        ChangeColor(gameObjectToChangeColor, Color.green);
    }

    public static void SetColorToRed(GameObject gameObjectToChangeColor)
    {
        ChangeColor(gameObjectToChangeColor, Color.red);
    }
}