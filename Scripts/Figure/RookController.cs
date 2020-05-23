using UnityEngine;

namespace Figure
{
    public class RookController : FigureController
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            for (var i = LocationX + 1; i < 8; i++)
            {
                var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(i, LocationY);
                if (HighlightCubes(i, LocationY, true) || figureControllerAtPosition != null && figureControllerAtPosition.Color == Color)
                {
                    break;
                }
            }

            for (var i = LocationX - 1; i > -1; i--)
            {
                var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(i, LocationY);
                if (HighlightCubes(i, LocationY, true) || figureControllerAtPosition != null && figureControllerAtPosition.Color == Color)
                {
                    break;
                }
            }

            for (var j = LocationY + 1; j < 8; j++)
            {
                var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(LocationX, j);
                if (HighlightCubes(LocationX, j, false) || figureControllerAtPosition != null && figureControllerAtPosition.Color == Color)
                {
                    break;
                }
            }

            for (var j = LocationY - 1; j > -1; j--)
            {
                var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(LocationX, j);
                if (HighlightCubes(LocationX, j, false) || figureControllerAtPosition != null && figureControllerAtPosition.Color == Color)
                {
                    break;
                }
            }
        }

        // returns 'true' when break is needed after the method execution 
        bool HighlightCubes(int i, int j, bool withCastling)
        {
            var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(i, j);
            if (figureControllerAtPosition == null)
            {
                if (withCastling && CheckCastling(i))
                {
                    gameController.HighlightCastlingCube(boardController.GetCube(i, j));
                }
                else
                {
                    gameController.HighlightCubeAvailableToMoveOnto(boardController.GetCube(i, j));
                }
            }

            if (figureControllerAtPosition == null || figureControllerAtPosition.Color != Color) return false;
            gameController.HighlightCubeUnderFigureToCapture(boardController.GetCube(i, j));
            return true;
        }

        bool CheckCastling(int locX)
        {
            if (!gameController.IsCastlingAvailable(gameObject)) return false;
            return locX == CalculateCastlingX();
        }
        
        int CalculateCastlingX()
        {
            return LocationX == 0 ? 3 : 5;
        }
    }
}