using UnityEngine;

namespace Figure
{
    public class PawnController : FigureController
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            var j = LocationY + 1 * GetColorMultiplier();
            if (j > -1 && j < 8)
                if (HighlightCubeAvailableToMoveOnto(j))
                {
                    if (!IsMoved())
                    {
                        HighlightCubeAvailableToMoveOnto(LocationY + 2 * GetColorMultiplier());
                    }
                }

            HighlightCubeUnderFigureToCapture(LocationX + 1, j);
            HighlightCubeUnderFigureToCapture(LocationX - 1, j);
        }

        bool HighlightCubeAvailableToMoveOnto(int j)
        {
            if (gameController.GetFigureControllerAtPosition(LocationX, j) != null) return false;
            gameController.HighlightCubeAvailableToMoveOnto(boardController.GetCube(LocationX, j));
            return true;
        }

        void HighlightCubeUnderFigureToCapture(int i, int j)
        {
            if (i <= -1 || i >= 8 || j <= -1 || j >= 8) return;
            var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(i, j);
            if (figureControllerAtPosition == null) return;
            if (figureControllerAtPosition.Color != Color)
            {
                gameController.HighlightCubeUnderFigureToCapture(boardController.GetCube(i, j));
            }
        }

        private int GetColorMultiplier()
        {
            return Color == Color.black ? 1 : -1;
        }
    }
}