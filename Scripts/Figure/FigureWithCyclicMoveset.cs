namespace Figure
{
    public abstract class FigureWithCyclicMoveset : FigureController
    {
        public void CheckDiagonalMoves()
        {
            for (int locX = LocationX + 1, locY = LocationY + 1; locX < 8 && locY < 8; locX++, locY++)
            {
                if (HighlightCells(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX + 1, locY = LocationY - 1; locX < 8 && locY > -1; locX++, locY--)
            {
                if (HighlightCells(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX - 1, locY = LocationY + 1; locX > -1 && locY < 8; locX--, locY++)
            {
                if (HighlightCells(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX - 1, locY = LocationY - 1; locX > -1 && locY > -1; locX--, locY--)
            {
                if (HighlightCells(locX, locY))
                {
                    break;
                }
            }
        }

        public void CheckHorizontalVerticalMoves()
        {
            for (var i = LocationX + 1; i < 8; i++)
            {
                if (HighlightCells(i, LocationY))
                {
                    break;
                }
            }

            for (var i = LocationX - 1; i > -1; i--)
            {
                if (HighlightCells(i, LocationY))
                {
                    break;
                }
            }

            for (var j = LocationY + 1; j < 8; j++)
            {
                if (HighlightCells(LocationX, j))
                {
                    break;
                }
            }

            for (var j = LocationY - 1; j > -1; j--)
            {
                if (HighlightCells(LocationX, j))
                {
                    break;
                }
            }
        }
    }
}