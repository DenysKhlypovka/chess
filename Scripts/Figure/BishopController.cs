namespace Figure
{
    public class BishopController : FigureWithCyclicMoveset
    {
        void OnMouseDown()
        {
            Activate();
            CheckDiagonalMoves();
        }
    }
}