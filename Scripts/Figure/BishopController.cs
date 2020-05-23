namespace Figure
{
    public class BishopController : FigureController
    {
        void OnMouseDown()
        {
            base.OnMouseDown();

            var locY = LocationX + LocationY;
            for (var _locX = LocationX + 1; _locX < 8 && locY - _locX > 7 && _locX > locY; _locX++)
            {
                if (HighlightCubes(_locX, locY - _locX))
                {
                    break;
                }
            }

            locY = LocationX + LocationY;
            for (var _locX = LocationX - 1; _locX > -1 && locY - _locX > 7 && _locX > locY; _locX--)
            {
                if (HighlightCubes(_locX, locY - _locX))
                {
                    break;
                }
            }


            var locX = LocationX;
            for (var _locY = LocationY - 1; locX >= 0 && _locY >= 0; _locY--, locX--)
            {
                if (HighlightCubes(locX, _locY))
                {
                    break;
                }
            }

            locX = LocationX;
            for (var _locY = LocationY + 1; locX > 7 && _locY > 7; _locY++, locX++)
            {
                if (HighlightCubes(locX, _locY))
                {
                    break;
                }
            }
        }

        // returns 'true' when break is needed after the method execution 
        bool HighlightCubes(int locX, int locY)
        {
            var figureControllerAtPosition = gameController.GetFigureControllerAtPosition(locX, locY);
            if (figureControllerAtPosition == null || figureControllerAtPosition.Color == Color) return true;

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