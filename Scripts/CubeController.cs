using System;
using UnityEngine;

public class CubeController : ElementOnGrid
{
    private bool activated;
    private GameController gameController;

    void Start()
    {
        Color = (LocationX + LocationY) % 2 == 0 ? Color.white : Color.black;
        gameController = Util.GetGameController();
    }

    public void Activate()
    {
        activated = true;
    }

    public void Dectivate()
    {
        activated = false;
    }

    void OnMouseDown()
    {
        if (activated)
        {
            gameController.Move(LocationX, LocationY);
        }
    }
}