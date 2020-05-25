using UnityEngine;

public class CellController : ElementOnGrid
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

    public void Deactivate()
    {
        activated = false;
    }

    void OnMouseDown()
    {
        if (activated)
        {
            gameController.TakeTurn(LocationX, LocationY);
        }
    }
}