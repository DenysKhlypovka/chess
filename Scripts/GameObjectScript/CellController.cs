using UnityEngine;
using Util;

namespace GameObjectScript
{
    public class CellController : ElementOnGrid
    {
        void Start()
        {
            Color = (Coordinate.X + Coordinate.Y) % 2 == 0 ? Color.white : Color.black;
            gameController = ComponentsUtil.GetGameController();
        }

        void OnMouseDown()
        {
            if (IsActivated)
            {
                gameController.TakeTurn(Coordinate);
            }
            else
            {
                gameController.TryActivatePiece(Coordinate);
            }
        }
    }
}