namespace Figure
{
    public class QueenController : FigureWithCyclicMoveset
    {
        void OnMouseDown()
        {
            Activate();
            CheckHorizontalVerticalMoves();
            CheckDiagonalMoves();
        }
    }
}