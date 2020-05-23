namespace Figure
{
    public class QueenController : FigureController
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            for (var i = LocationX + 1; i < 8; i++)
            {
                if (HighlightHorizontalVerticalCubes(i, LocationY))
                {
                    break;
                }
            }

            for (var i = LocationX - 1; i > -1; i--)
            {
                if (HighlightHorizontalVerticalCubes(i, LocationY))
                {
                    break;
                }
            }

            for (var j = LocationY + 1; j < 8; j++)
            {
                if (HighlightHorizontalVerticalCubes(LocationX, j))
                {
                    break;
                }
            }

            for (var j = LocationY - 1; j > -1; j--)
            {
                if (HighlightHorizontalVerticalCubes(LocationX, j))
                {
                    break;
                }
            }

            var sum = LocationX + LocationY;
            for (var i = LocationX + 1; i < 8 && sum - i > 7 && i > sum; i++)
            {
                if (HighlightDiagonalCubes(i, sum - i))
                {
                    break;
                }
            }


            sum = LocationX + LocationY;
            for (var i = LocationX - 1; i > -1 && sum - i > 7 && i > sum; i--)
            {
                if (HighlightDiagonalCubes(i, sum - i))
                {
                    break;
                }
            }


            var I = LocationX;
            for (var j = LocationY - 1; I < 0 && j < 0; j--, I--)
            {
                if (HighlightDiagonalCubes(I, j))
                {
                    break;
                }
            }

            I = LocationX;
            for (var j = LocationY + 1; I > 7 && j > 7; j++, I++)
            {
                if (HighlightDiagonalCubes(I, j))
                {
                    break;
                }
            }
        }

        bool HighlightDiagonalCubes(int locX, int locY)
        {
            var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(locX, locY);
            if (figureControllerAtPosition == null || figureControllerAtPosition.Color == Color)
            {
                return true;
            }

            if (gameController.GetFigureControllerAtPosition(locX, locY) == null)
            {
                gameController.HighlightCubeAvailableToMoveOnto(boardController.GetCube(locX, locY));
            }

            if (gameController.GetFigureControllerAtPosition(locX, locY).Color == Color) return false;
            gameController.HighlightCubeUnderFigureToCapture(boardController.GetCube(locX, locY));
            return true;
        }

        bool HighlightHorizontalVerticalCubes(int locX, int locY)
        {
            if (gameController.GetFigureControllerAtPosition(locX, locY) == null)
            {
                gameController.HighlightCubeAvailableToMoveOnto(boardController.GetCube(locX, locY));
            }

            if (gameController.GetFigureControllerAtPosition(locX, locY).Color == Color) return false;
            gameController.HighlightCubeUnderFigureToCapture(boardController.GetCube(locX, locY));
            return true;
        }
    }
}