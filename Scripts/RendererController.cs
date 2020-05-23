using UnityEngine;

public class RendererController : MonoBehaviour
{
    public static void ChangeColor(GameObject gameObjectToChangeColor, Color color)
    {
        gameObjectToChangeColor.GetComponent<Renderer>().material.SetColor("_Color", color);
    }
}