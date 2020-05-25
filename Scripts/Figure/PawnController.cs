using UnityEngine;

namespace Figure
{
    public class PawnController : FigureController
    {
        void OnMouseDown()
        {
            Activate();
            var locY = LocationY + 1 * GetColorMultiplier();

            if (highlightManager.CheckAndHighlightCellAvailableToMoveOnto(LocationX, locY) && !IsMoved())
            {
                highlightManager.CheckAndHighlightCellAvailableToMoveOnto(LocationX, LocationY + 2 * GetColorMultiplier());
            }
            
            highlightManager.CheckAndHighlightCellUnderFigureToCapture(LocationX - 1, locY, Color);
            highlightManager.CheckAndHighlightCellUnderFigureToCapture(LocationX + 1, locY, Color);
        }

        private int GetColorMultiplier()
        {
            return Color == Color.black ? 1 : -1;
        }
    }
}