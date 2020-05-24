﻿namespace Figure
{
    public class KnightController : FigureController
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            for (var i = LocationX - 2; i < LocationX + 3 && i > -1 && i < 8; i++)
            {
                for (var j = LocationY - 2; j < LocationY + 3 && j > -1 && j < 8; j++)
                {
                    if ((i - LocationX) * (j - LocationY) != 2 && (i - LocationX) * (j - LocationY) != -2) continue;
                    HighlightCubes(i, j);
                }
            }
        }
    }
}