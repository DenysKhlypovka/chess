using UnityEngine;

namespace GameObjectScript
{
    public class CellController : ElementOnGrid
    {
        void Start()
        {
            Color = (Coordinate.X + Coordinate.Y) % 2 == 0 ? Color.white : Color.black;
            gameController = Util.Util.GetGameController();
        }

        void OnMouseDown()
        {
            if (IsActivated)
            {
                gameController.TakeTurn(Coordinate);
            }
            else
            {
                gameController.TryActivateFigure(Coordinate);
            }
        }
    }
}