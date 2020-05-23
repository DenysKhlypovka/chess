using UnityEngine;

public class ElementOnGrid : MonoBehaviour
{
    private int locationX;
    private int locationY;

    private Color color;

    public int LocationX
    {
        get => locationX;
        set => locationX = value;
    }

    public int LocationY
    {
        get => locationY;
        set => locationY = value;
    }

    public Color Color
    {
        get => color;
        set => color = value;
    }
}