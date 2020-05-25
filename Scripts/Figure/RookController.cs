namespace Figure
{
    public class RookController : FigureWithCyclicMoveset
    {
        void OnMouseDown()
        {
            Activate();
            CheckHorizontalVerticalMoves();
            CheckCastling();
        }

        void CheckCastling()
        {
            if (gameController.IsCastlingAvailable(this))
            {
                highlightManager.HighlightCastlingCell(this);
            }
        }
    }
}