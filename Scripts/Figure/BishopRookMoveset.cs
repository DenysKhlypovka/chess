using UnityEngine;

namespace Figure
{
    public class BishopRookMoveset : FigureController
    {
        public void CheckAvailableBishopMoves()
        {
            for (int locX = LocationX + 1, locY = LocationY + 1; locX < 8 && locY < 8; locX++, locY++)
            {
                if (HighlightCubes(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX + 1, locY = LocationY - 1; locX < 8 && locY >= 0; locX++, locY--)
            {
                if (HighlightCubes(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX - 1, locY = LocationY + 1; locX >= 0 && locY < 8; locX--, locY++)
            {
                if (HighlightCubes(locX, locY))
                {
                    break;
                }
            }

            for (int locX = LocationX - 1, locY = LocationY - 1; locX >= 0 && locY >= 0; locX--, locY--)
            {
                if (HighlightCubes(locX, locY))
                {
                    break;
                }
            }
        }

        public void CheckAvailableRookMoves()
        {
            for (var i = LocationX + 1; i < 8; i++)
            {
                if (HighlightCubes(i, LocationY))
                {
                    break;
                }
            }

            for (var i = LocationX - 1; i > -1; i--)
            {
                if (HighlightCubes(i, LocationY))
                {
                    break;
                }
            }

            for (var j = LocationY + 1; j < 8; j++)
            {
                if (HighlightCubes(LocationX, j))
                {
                    break;
                }
            }

            for (var j = LocationY - 1; j > -1; j--)
            {
                if (HighlightCubes(LocationX, j))
                {
                    break;
                }
            }
        }
    }
}