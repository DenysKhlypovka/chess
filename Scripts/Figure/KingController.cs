namespace Figure
{
    public class KingController : FigureController
    {
        void OnMouseDown()
        {
            base.OnMouseDown();
            for (var i = LocationX - 1; i < LocationX + 2 & i > -1 && i < 8; i++)
            {
                for (var j = LocationY - 1; j < LocationY + 2 & j > -1 && j < 8; j++)
                {
                    HighlightCubes(i, j);
                }
            }
        }
    }
}